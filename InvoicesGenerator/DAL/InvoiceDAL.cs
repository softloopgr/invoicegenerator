using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using InvoicesGenerator;
using InvoicesGenerator.Classes;
using System.Globalization;

namespace InvoicesGenerator
{
    class InvoiceDAL
    {
        /// <summary>
        /// Ψάχνει για ένα τιμολόγιο με βάση το Όνομα του Πελάτη ή το ΑΦΜ του
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Invoice> search(DateTime dateFrom, DateTime dateTo, string company, string taxNumber, int inv_type)
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT I.*
                        FROM Invoices_Group I
                        WHERE datediff(d,I.date,@dateFrom)>0
                        AND datediff(d,I.date,@dateTo)<0                        
                        OR 
                        (I.customer_name LIKE @company
                        AND I.customer_taxnumber LIKE @taxNumber
                        AND (I.invoice_type = @inv_type OR @inv_type = -1)) 
                        ORDER BY number desc";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@company", "%" + company + "%");
                        cmd.Parameters.AddWithValue("@taxNumber", "%" + taxNumber + "%");
                        cmd.Parameters.AddWithValue("@inv_type", inv_type);

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Invoice invoice = new Invoice();
                                invoice.Id = int.Parse(dr["id"].ToString());
                                invoice.Date = DateTime.Parse(dr["date"].ToString());
                                invoice.Location = dr["location"].ToString();
                                invoice.comments = dr["comments"].ToString();
                                invoice.Location = dr["location"].ToString();
                                invoice.Number = int.Parse(dr["number"].ToString());
                                invoice.Subtotal = decimal.Parse(dr["subtotal"].ToString());
                                invoice.Tax = decimal.Parse(dr["tax"].ToString());
                                invoice.TaxNumeric = decimal.Parse(dr["tax"].ToString()) / 100 * decimal.Parse(dr["subtotal"].ToString());
                                invoice.TotalVF = decimal.Parse(dr["total"].ToString());

                                invoice.Status = int.Parse(dr["status"].ToString());
                                invoice.Invoice_type = int.Parse(dr["invoice_type"].ToString());
                                invoice.Is_credit = int.Parse(dr["is_credit"].ToString());
                                invoice.Enable_hold = int.Parse(dr["enable_hold"].ToString());
                                invoice.hold_value_database = decimal.Parse(dr["hold_value"].ToString());
                                invoice.Hold_percent = decimal.Parse(dr["hold_percent"].ToString());
                                invoice.Is_deleted = int.Parse(dr["is_deleted"].ToString());

                                invoice.Customer_name = dr["customer_name"].ToString();
                                invoice.Customer_description = dr["customer_description"].ToString();
                                invoice.Customer_address = dr["customer_address"].ToString();
                                invoice.Customer_taxnumber = dr["customer_taxnumber"].ToString();
                                invoice.Customer_taxoffice = dr["customer_taxoffice"].ToString();

                                invoice.InvoiceItems = InvoiceDAL.getInvoicesItems((int)invoice.Id, invoice.Tax);

                                invoices.Add(invoice);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return invoices;
        }

        /// <summary>
        /// Επιστρέφει ένα τιμολόγιο ψάχνοντας με το ID του
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static Invoice getById(int InvoiceID)
        {
            Invoice invoice = new Invoice();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT I.*
                        FROM Invoices_Group I
                        where I.id = @InvoiceID ";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID.ToString());

                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                invoice.Id = int.Parse(dr["id"].ToString());
                                invoice.Date = DateTime.Parse(dr["date"].ToString());
                                invoice.comments = dr["comments"].ToString();
                                invoice.Location = dr["location"].ToString();
                                invoice.Number = int.Parse(dr["number"].ToString());
                                invoice.Subtotal = decimal.Parse(dr["subtotal"].ToString());

                                invoice.Tax = decimal.Parse(dr["tax"].ToString().Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                                invoice.TotalVF = decimal.Parse(dr["total"].ToString());
                                invoice.Status = int.Parse(dr["status"].ToString());

                                invoice.Customer_name = dr["customer_name"].ToString();
                                invoice.Customer_description = dr["customer_description"].ToString();
                                invoice.Customer_address = dr["customer_address"].ToString();
                                invoice.Customer_taxnumber = dr["customer_taxnumber"].ToString();
                                invoice.Customer_taxoffice = dr["customer_taxoffice"].ToString();

                                invoice.Invoice_type = int.Parse(dr["invoice_type"].ToString());
                                invoice.Is_credit = int.Parse(dr["is_credit"].ToString());
                                invoice.Enable_hold = int.Parse(dr["enable_hold"].ToString());
                                invoice.hold_value_database = decimal.Parse(dr["hold_value"].ToString());
                                invoice.Hold_percent = decimal.Parse(dr["hold_percent"].ToString());

                                invoice.UserComments = dr["user_comments"].ToString();
                                invoice.InvoiceOrder = dr["invoice_order"] != DBNull.Value ? dr["invoice_order"].ToString() : null;

                                invoice.LoadingPlace = dr["loading_place"] != DBNull.Value ? dr["loading_place"].ToString() : null;
                                invoice.MovingPurpose = dr["moving_purpose"] != DBNull.Value ? dr["moving_purpose"].ToString() : null;
                                invoice.Destination = dr["destination"] != DBNull.Value ? dr["destination"].ToString() : null;
                                invoice.CustomerPhone = dr["customer_phone"] != DBNull.Value ? dr["customer_phone"].ToString() : null;
                                invoice.CustomerCity = dr["customer_city"] != DBNull.Value ? dr["customer_city"].ToString() : null;
                                invoice.CustomerPostalCode = dr["customer_postalcode"] != DBNull.Value ? dr["customer_postalcode"].ToString() : null;

                                invoice.InvoiceItems = InvoiceDAL.getInvoicesItems((int)invoice.Id, invoice.Tax);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return invoice;
        }

        /// <summary>
        /// Αποθηκεύει ένα τιμολόγιο
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static bool insert(Invoice invoice, out int? InvoiceID)
        {
            InvoiceID = null;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
                                    insert into Invoices_Group(
                                        date,
                                        location,
                                        number,
                                        comments,
                                        subtotal,
                                        tax,
                                        total,
                                        status,
                                        customer_name,
                                        customer_description,
                                        customer_address,
                                        customer_taxoffice,
                                        customer_taxnumber,
                                        invoice_type,
                                        is_credit,
                                        enable_hold,
                                        hold_value,
                                        hold_percent,
                                        is_deleted,
                                        user_comments,
                                        invoice_order,
                                        loading_place,
                                        moving_purpose,
                                        destination,
                                        customer_phone,
                                        customer_city,
                                        customer_postalcode
                                       )
                                    values (
                                        @date,
                                        @location,
                                        @number,
                                        @comments,
                                        @subtotal,
                                        @tax,
                                        @total,
                                        @status,
                                        @customer_name,
                                        @customer_description,
                                        @customer_address,
                                        @customer_taxoffice,
                                        @customer_taxnumber,
                                        @invoice_type,
                                        @is_credit,
                                        @enable_hold,
                                        @hold_value,
                                        @hold_percent,
                                        @is_deleted,
                                        @user_comments,
                                        @invoice_order,
                                        @loading_place,
                                        @moving_purpose,
                                        @destination,
                                        @customer_phone,
                                        @customer_city,
                                        @customer_postalcode
                                        );";

                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@date", invoice.Date.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@location", invoice.Location);
                            cmd.Parameters.AddWithValue("@number", invoice.Number);
                            cmd.Parameters.AddWithValue("@comments", invoice.comments);
                            cmd.Parameters.AddWithValue("@subtotal", invoice.Subtotal);
                            cmd.Parameters.AddWithValue("@tax", invoice.Tax);
                            cmd.Parameters.AddWithValue("@total", invoice.totalWithVat);
                            cmd.Parameters.AddWithValue("@status", invoice.Status);
                            cmd.Parameters.AddWithValue("@customer_name", invoice.Customer_name);
                            cmd.Parameters.AddWithValue("@customer_description", invoice.Customer_description);
                            cmd.Parameters.AddWithValue("@customer_address", invoice.Customer_address);
                            cmd.Parameters.AddWithValue("@customer_taxoffice", invoice.Customer_taxoffice);
                            cmd.Parameters.AddWithValue("@customer_taxnumber", invoice.Customer_taxnumber);
                            cmd.Parameters.AddWithValue("@customer_city", invoice.CustomerCity);
                            cmd.Parameters.AddWithValue("@customer_postalcode", invoice.CustomerPostalCode);
                            cmd.Parameters.AddWithValue("@invoice_type", invoice.Invoice_type);

                            cmd.Parameters.AddWithValue("@is_credit", invoice.Is_credit);
                            cmd.Parameters.AddWithValue("@enable_hold", invoice.Enable_hold);
                            cmd.Parameters.AddWithValue("@hold_value", invoice.hold_value);
                            cmd.Parameters.AddWithValue("@hold_percent", invoice.Hold_percent / 100);
                            cmd.Parameters.AddWithValue("@is_deleted", invoice.Is_deleted);

                            cmd.Parameters.AddWithValue("@user_comments", invoice.UserComments);

                            if (Program.seal.HasOrder == 1 || invoice.InvoiceOrder != null)
                            {
                                if (invoice.InvoiceOrder != null)
                                {
                                    cmd.Parameters.AddWithValue("@invoice_order", invoice.InvoiceOrder);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@invoice_order", DBNull.Value);
                                }
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@invoice_order", DBNull.Value);
                            }

                            if (invoice.LoadingPlace != null)
                            {
                                cmd.Parameters.AddWithValue("@loading_place", invoice.LoadingPlace);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@loading_place", DBNull.Value);
                            }

                            if (invoice.MovingPurpose != null)
                            {
                                cmd.Parameters.AddWithValue("@moving_purpose", invoice.MovingPurpose);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@moving_purpose", DBNull.Value);
                            }

                            if (invoice.Destination != null)
                            {
                                cmd.Parameters.AddWithValue("@destination", invoice.Destination);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@destination", DBNull.Value);
                            }

                            if (invoice.CustomerPhone != null)
                            {
                                cmd.Parameters.AddWithValue("@customer_phone", invoice.CustomerPhone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@customer_phone", DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();

                            cmd.CommandText = @"select @@identity from Invoices_Group";
                            cmd.Parameters.Clear();

                            InvoiceID = (int?)int.Parse(cmd.ExecuteScalar().ToString());
                            invoice.Id = InvoiceID;

                            foreach (var item in invoice.InvoiceItems)
                            {
                                item.save((int)invoice.Id, cmd);
                            }
                        }
                        scope.Complete();
                    }
                }

                catch (Exception e)
                {
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Κάνει UPDATE ενός τιμολογίου με βάση το ID
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static bool update(Invoice invoice)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
                                    update Invoices_Group
                                    set
                                        date = @date,
                                        location = @location,
                                        number = @number,
                                        comments = @comments,
                                        subtotal = @subtotal,
                                        tax = @tax,
                                        total = @total,
                                        status = @status,

                                        customer_name=@customer_name,
                                        customer_description=@customer_description,
                                        customer_address=@customer_address,
                                        customer_taxoffice=@customer_taxoffice,
                                        customer_taxnumber=@customer_taxnumber, 

                                        invoice_type = @invoice_type,
                                        is_credit = @is_credit,
                                        enable_hold = @enable_hold,
                                        hold_value = @hold_value,
                                        hold_percent = @hold_percent,

                                        user_comments = @user_comments,
                                        invoice_order = @invoice_order,          

                                        loading_place = @loading_place,
                                        moving_purpose = @moving_purpose,
                                        destination = @destination,
                                        customer_phone = @customer_phone,
                                        customer_city = @customer_city,
                                        customer_postalcode = @customer_postalcode

                                    WHERE
                                        id=@id;";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", invoice.Id);
                            cmd.Parameters.AddWithValue("@date", invoice.Date);
                            cmd.Parameters.AddWithValue("@location", invoice.Location);
                            cmd.Parameters.AddWithValue("@number", invoice.Number);
                            cmd.Parameters.AddWithValue("@comments", invoice.comments);
                            cmd.Parameters.AddWithValue("@subtotal", invoice.Subtotal);
                            cmd.Parameters.AddWithValue("@tax", invoice.Tax);
                            cmd.Parameters.AddWithValue("@total", invoice.totalWithVat);
                            cmd.Parameters.AddWithValue("@status", invoice.Status);
                            cmd.Parameters.AddWithValue("@customer_name", invoice.Customer_name);
                            cmd.Parameters.AddWithValue("@customer_address", invoice.Customer_address);
                            cmd.Parameters.AddWithValue("@customer_taxnumber", invoice.Customer_taxnumber);
                            cmd.Parameters.AddWithValue("@customer_city", invoice.CustomerCity);
                            cmd.Parameters.AddWithValue("@customer_postalcode", invoice.CustomerPostalCode);
                            cmd.Parameters.AddWithValue("@customer_taxoffice", invoice.Customer_taxoffice);
                            cmd.Parameters.AddWithValue("@customer_description", invoice.Customer_description);
                            cmd.Parameters.AddWithValue("@invoice_type", invoice.Invoice_type);

                            cmd.Parameters.AddWithValue("@is_credit", invoice.Is_credit);
                            cmd.Parameters.AddWithValue("@enable_hold", invoice.Enable_hold);
                            cmd.Parameters.AddWithValue("@hold_value", invoice.hold_value);
                            cmd.Parameters.AddWithValue("@hold_percent", invoice.Hold_percent / 100);

                            cmd.Parameters.AddWithValue("@user_comments", invoice.UserComments);

                            if (Program.seal.HasOrder == 1 || invoice.InvoiceOrder != null)
                            {
                                if (invoice.InvoiceOrder != null)
                                {
                                    cmd.Parameters.AddWithValue("@invoice_order", invoice.InvoiceOrder);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@invoice_order", DBNull.Value);
                                }
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@invoice_order", DBNull.Value);
                            }

                            if (invoice.LoadingPlace != null)
                            {
                                cmd.Parameters.AddWithValue("@loading_place", invoice.LoadingPlace);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@loading_place", DBNull.Value);
                            }

                            if (invoice.MovingPurpose != null)
                            {
                                cmd.Parameters.AddWithValue("@moving_purpose", invoice.MovingPurpose);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@moving_purpose", DBNull.Value);
                            }

                            if (invoice.Destination != null)
                            {
                                cmd.Parameters.AddWithValue("@destination", invoice.Destination);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@destination", DBNull.Value);
                            }

                            if (invoice.CustomerPhone != null)
                            {
                                cmd.Parameters.AddWithValue("@customer_phone", invoice.CustomerPhone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@customer_phone", DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();

                            foreach (var item in invoice.InvoiceItems)
                            {
                                item.save(invoice.Id.Value, cmd);
                            }

                        }
                    }

                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                scope.Complete();
            }
            return true;
        }

        /// <summary>
        /// Διαγράφει ένα τιμολόγιο με βάση το ID (και όλες του τις αιτιολογίες)
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static bool deleteByGroup(int InvoiceID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE Invoices SET is_deleted=1 WHERE group_id=@group_id;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@group_id", InvoiceID);
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE Invoices_Group SET is_deleted=1 WHERE id=@id;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", InvoiceID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                scope.Complete();
            }
            return true;
        }

        public static bool cancel()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update Invoices set status=4 WHERE status>2;";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                scope.Complete();
            }
            return true;
        }

        public static List<KeyValuePair<string, string>> getCustomerList()
        {
            List<KeyValuePair<string, string>> custList = new List<KeyValuePair<string, string>>();
            custList.Add(new KeyValuePair<string, string>("", ""));
            List<string> taxNumbers = InvoiceDAL.getTaxNumbers();

            bool found = false;
            foreach (string taxNum in taxNumbers)
            {
                Dictionary<string, string> cust = getCustomer(taxNum, out found);
                custList.Add(new KeyValuePair<string, string>(taxNum, cust["customer_name"]));
            }
            return custList;
        }

        /// <summary>
        /// Κάνει αναζήτηση για υπάρχων Πελάτη με το ΑΦΜ του
        /// </summary>
        /// <param name="CustomerTaxNumber"></param>
        /// <param name="found"></param>
        /// <returns></returns>
        public static Dictionary<string, string> getCustomer(string CustomerTaxNumber, out bool found)
        {
            found = false;
            Dictionary<string, string> customer = new Dictionary<string, string>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT TOP(1) *
                        FROM Invoices_Group
                        WHERE customer_taxnumber = @CustomerTaxNumber
                        ORDER BY number DESC";
                        // CAUTION: mporei to order by na epirease kapou anepithimita, giafto kai kratao to comment

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerTaxNumber", CustomerTaxNumber);

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                customer.Add("customer_name", dr["customer_name"].ToString());
                                customer.Add("customer_address", dr["customer_address"].ToString());
                                customer.Add("customer_taxnumber", dr["customer_taxnumber"].ToString());
                                customer.Add("customer_taxoffice", dr["customer_taxoffice"].ToString());
                                customer.Add("customer_description", dr["customer_description"].ToString());
                                customer.Add("customer_phone", dr["customer_phone"].ToString());
                                customer.Add("customer_city", dr["customer_city"].ToString());
                                customer.Add("customer_postalcode", dr["customer_postalcode"].ToString());
                                found = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return customer;
        }

        /// <summary>
        /// Επιστρέφει όλες τις αγορές μιας απόδειξης με το ID της απόδειξης
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static List<InvoiceItem> getInvoicesItems(int InvoiceID, decimal? tax_percent)
        {
            List<InvoiceItem> invoices = new List<InvoiceItem>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT  *
                        FROM Invoices
                        WHERE group_id = @InvoiceID
                        AND is_deleted = 0";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID.ToString());

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                InvoiceItem invoice = new InvoiceItem();
                                invoice.id = int.Parse(dr["id"].ToString());
                                invoice.amount = decimal.Parse(dr["amount"].ToString());
                                invoice.description = dr["description"].ToString();
                                invoice.total = decimal.Parse(dr["total"].ToString());
                                invoice.vatPrice = invoice.total * (tax_percent / 100);
                                invoice.vatPrice = General.BankersRound.RoundAwayFromZero(invoice.vatPrice.Value, 2);
                                invoice.unit = decimal.Parse(dr["unit"].ToString());
                                invoice.MetricUnit = dr["metric_unit"].ToString();
                                invoice.is_deleted = int.Parse(dr["is_deleted"].ToString());
                                invoices.Add(invoice);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return invoices;
        }

        /// <summary>
        /// Αποθηκεύει μια αιτιολογία (αγορά) μιάς απόδειξης
        /// </summary>
        /// <param name="invoiceItem"></param>
        /// <param name="InvoiceID"></param>
        /// <param name="itemID"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal static bool insert(InvoiceItem invoiceItem, int InvoiceID, out int? itemID, SqlCeCommand cmd)
        {
            itemID = null;

            bool ret = false;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                                    insert into Invoices(
                                        group_id,
                                        description,
                                        unit,
                                        amount,
                                        total,
                                        metric_unit
                                        )
                                        values (
                                        @group_id,
                                        @description,
                                        @unit,
                                        @amount,
                                        @total,
                                        @metric_unit);";


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@group_id", InvoiceID.ToString());
                cmd.Parameters.AddWithValue("@description", invoiceItem.description);
                cmd.Parameters.AddWithValue("@unit", invoiceItem.unit);
                cmd.Parameters.AddWithValue("@amount", invoiceItem.amount);
                cmd.Parameters.AddWithValue("@total", invoiceItem.total);
                cmd.Parameters.AddWithValue("@metric_unit", invoiceItem.MetricUnit);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"select @@identity from Invoices";
                cmd.Parameters.Clear();

                itemID = int.Parse(cmd.ExecuteScalar().ToString());
                invoiceItem.id = itemID;

                ret = true;
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Κάνει UPDATE μιάς αγοράς μιάς απόδειξης
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal static bool update(InvoiceItem item, SqlCeCommand cmd)
        {
            bool ret = false;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                                    UPDATE Invoices
                                    SET
                                        description = @description,
                                        amount = @amount,
                                        unit = @unit,
                                        total = @total,
                                        metric_unit = @metric_unit
                                        
                                    WHERE id=@id;";


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", item.id.ToString());
                cmd.Parameters.AddWithValue("@amount", item.amount);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@unit", item.unit);
                cmd.Parameters.AddWithValue("@total", item.total);
                cmd.Parameters.AddWithValue("@metric_unit", item.MetricUnit);
                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Διαγράφει μια αγορά απο μια απόδειξη
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        internal static bool delete(int item_id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE Invoices SET is_deleted=1 WHERE id=@id;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", item_id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                scope.Complete();
            }
            return true;
        }

        internal static bool cancel(int Invoice_id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                    {
                        cn.Open();
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update Invoices set status=3 WHERE id=@id;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", Invoice_id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return false;
                }
                scope.Complete();
            }
            return true;
        }

        internal static void setStatus(Invoice Invoice, int status)
        {
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                                        UPDATE Invoices_group
                                        SET status=@status
                                        WHERE id=@id";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", Invoice.Id.Value);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Επιστρέφει όλους τους τύπους αποδείξεων
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> getInvoiceTypes(bool isInMain = false)
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();

            if (isInMain)
            {
                result.Add(new KeyValuePair<int, string>(-1, "Όλα"));
            }

            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select id,name from invoice_types where is_active = 1 order by name asc";

                        cmd.Parameters.Clear();

                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(dr.GetOrdinal("id"));
                                string name = dr["name"] != DBNull.Value ? dr.GetString(dr.GetOrdinal("name")) : string.Empty;
                                result.Add(new KeyValuePair<int, string>(id, name));
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        public static List<KeyValuePair<string, string>> getMetricTypes(bool isInMain = false)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select id, short_name, long_name from MetricUnit where is_active = 1 and is_deleted = 0 order by long_name desc";

                        cmd.Parameters.Clear();

                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string short_name = dr["short_name"] != DBNull.Value ? dr.GetString(dr.GetOrdinal("short_name")) : string.Empty;
                                string name = dr["long_name"] != DBNull.Value ? dr.GetString(dr.GetOrdinal("long_name")) : string.Empty;
                                result.Add(new KeyValuePair<string, string>(short_name, name));
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        /// <summary>
        /// Επιστρέφει τον αριθμό της εκάστωτε απόδειξης για να ξέρει απο πού να αριθμήσει για τις εκτυπώσεις
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int getInvoiceNumber(int type)
        {
            int result = -1;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select [current] from invoice_types where id = @type";

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@type", type);

                        object objCureent = cmd.ExecuteScalar();
                        if (objCureent != null)
                        {
                            result = (int)objCureent;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        public static bool updateInvoiceNumber(int type, int newNumber)
        {
            bool result = false;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        if (newNumber == getInvoiceNumber(type))
                        {
                            cmd.CommandText = @"UPDATE invoice_types SET [current] = [current] + 1 where id = @type";
                        }
                        else
                        {
                            cmd.CommandText = @"UPDATE invoice_types SET [current] = @newNum + 1 where id = @type";
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@newNum", newNumber);
                        cmd.ExecuteNonQuery();

                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        /// <summary>
        /// Αποθηκεύει στην βάση τα δεδομένα μιας σφραγίδας
        /// </summary>
        /// <param name="seal"></param>
        /// <returns></returns>
        public static bool insertCompany(SealDetails seal)
        {
            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                cn.Open();
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                                    insert into Seal(
                                        name,
                                        occupation,
                                        address,
                                        taxnumber,
                                        phone,
                                        taxoffice,
                                        location,
                                        vat,
                                        has_order
                                       )
                                    values (
                                        @name,
                                        @occupation,
                                        @address,
                                        @taxnumber,
                                        @phone,
                                        @taxoffice,
                                        @location,
                                        @vat,
                                        @has_order
                                        );";

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@name", seal.Name);
                    cmd.Parameters.AddWithValue("@occupation", seal.Occupation);
                    cmd.Parameters.AddWithValue("@address", seal.Address);
                    cmd.Parameters.AddWithValue("@taxnumber", seal.Taxnumber);
                    cmd.Parameters.AddWithValue("@phone", seal.Phone);
                    cmd.Parameters.AddWithValue("@taxoffice", seal.Taxoffice);
                    cmd.Parameters.AddWithValue("@location", seal.Location);
                    cmd.Parameters.AddWithValue("@vat", seal.Vat);
                    cmd.Parameters.AddWithValue("@has_order", seal.HasOrder);

                    cmd.ExecuteNonQuery();

                    Program.seal = seal;

                    /*User user = new User();
                    user.Name = seal.Name;
                    user.Username = seal.Username;
                    user.Password = seal.Password;
                    user.IsActive = 1;
                    user.UserHierarchy = 200;
                    user.UserLevelId = 4;

                    Program.seal = seal;

                    string error = string.Empty;
                    UserDAL.insert(user, out error);*/
                }
            }
            return true;
        }

        /// <summary>
        /// Κάνει UPDATE για τον πίνακα Seal
        /// </summary>
        /// <param name="seal"></param>
        /// <returns></returns>
        public static bool updateCompany(SealDetails seal)
        {
            bool ret;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                                    UPDATE Seal
                                    SET
                                        name = @name,
                                        occupation = @occupation,
                                        address = @address,
                                        taxnumber = @taxnumber,
                                        phone = @phone,
                                        taxoffice = @taxoffice,
                                        location = @location,
                                        vat = @vat,
                                        has_order = @has_order
                                    WHERE id=@id;";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", seal.id.ToString());
                        cmd.Parameters.AddWithValue("@name", seal.Name);
                        cmd.Parameters.AddWithValue("@occupation", seal.Occupation);
                        cmd.Parameters.AddWithValue("@address", seal.Address);
                        cmd.Parameters.AddWithValue("@taxnumber", seal.Taxnumber);
                        cmd.Parameters.AddWithValue("@phone", seal.Phone);
                        cmd.Parameters.AddWithValue("@taxoffice", seal.Taxoffice);
                        cmd.Parameters.AddWithValue("@location", seal.Location);
                        cmd.Parameters.AddWithValue("@vat", seal.Vat);
                        cmd.Parameters.AddWithValue("@has_order", seal.HasOrder);

                        cmd.ExecuteNonQuery();
                    }
                }
                Program.seal = seal;
                ret = true;
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Ελέγχει αν υπάρχει μια σφραγίδα και την επιστρέφει 
        /// </summary>
        /// <returns></returns>
        public static SealDetails getSeal()
        {
            SealDetails seal = new SealDetails();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT *
                        FROM Seal";

                        cmd.Parameters.Clear();

                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                seal.id = int.Parse(dr["id"].ToString());
                                seal.Name = dr.GetString(dr.GetOrdinal("name"));
                                seal.Occupation = dr.GetString(dr.GetOrdinal("occupation"));
                                seal.Address = dr.GetString(dr.GetOrdinal("address"));
                                seal.Taxnumber = dr.GetString(dr.GetOrdinal("taxnumber"));
                                seal.Phone = dr.GetString(dr.GetOrdinal("phone"));
                                seal.Taxoffice = dr.GetString(dr.GetOrdinal("taxoffice"));
                                seal.Location = dr.GetString(dr.GetOrdinal("location"));
                                seal.Vat = decimal.Parse(dr["vat"].ToString());
                                seal.HasOrder = int.Parse(dr["has_order"].ToString());
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return seal;
        }

        public static AutoCompleteStringCollection getTaxOffices()
        {
            AutoCompleteStringCollection taxOffices = new AutoCompleteStringCollection();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT distinct customer_taxoffice
                        FROM Invoices_group";

                        cmd.Parameters.Clear();

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["customer_taxoffice"] != null)
                                {
                                    taxOffices.Add(dr.GetString(dr.GetOrdinal("customer_taxoffice")));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return taxOffices;
        }

        public static List<string> getTaxNumbers()
        {
            List<string> taxOffices = new List<string>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT distinct customer_taxnumber
                        FROM Invoices_group";

                        cmd.Parameters.Clear();

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["customer_taxnumber"] != null)
                                {
                                    taxOffices.Add(dr.GetString(dr.GetOrdinal("customer_taxnumber")));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return taxOffices;
        }

        public static bool isCompanyInfoRequired(int type)
        {
            bool result = false;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select is_company_info_required from invoice_types where id = @type";

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@type", type);

                        object objCureent = cmd.ExecuteScalar();
                        if (objCureent != null)
                        {
                            int temp_result = (int)objCureent;
                            result = (temp_result == 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        /*public static AutoCompleteStringCollection getTaxNumbers()
        {
            AutoCompleteStringCollection taxOffices = new AutoCompleteStringCollection();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT distinct customer_taxnumber
                        FROM Invoices_group";

                        cmd.Parameters.Clear();

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["customer_taxnumber"] != null)
                                {
                                    taxOffices.Add(dr.GetString(dr.GetOrdinal("customer_taxnumber")));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return taxOffices;
        }*/

        public static string getBlankInvoiceNames(int id)
        {
            string name = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT name
                        FROM invoice_types
                        WHERE is_PrePrint = 1
                        AND id = @id";

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@id", id);

                        object objCureent = cmd.ExecuteScalar();
                        if (objCureent != null)
                        {
                            name = (string)objCureent;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return name;
        }

        public static string getInvoiceTypeLabelById(int invoiceTypeId)
        {
            string result = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select label from invoice_types where id = @id";

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@id", invoiceTypeId);

                        object objCureent = cmd.ExecuteScalar();
                        if (objCureent != null)
                        {
                            result = objCureent.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        public static int getInvoiceTypeIdByLabel(string label)
        {
            int result = -1;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Select id from invoice_types where label = @label";

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@label", label);

                        object objCureent = cmd.ExecuteScalar();
                        if (objCureent != null)
                        {
                            result = (int)objCureent;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

        public static List<ExistingDescriptions> getExistingInvoiceDescriptions()
        {
            List<ExistingDescriptions> existingInvoiceDescriptions = new List<ExistingDescriptions>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT distinct description
                        FROM Invoices";

                        cmd.Parameters.Clear();

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["description"] != null)
                                {
                                    ExistingDescriptions exDesc = new ExistingDescriptions();
                                    exDesc.ExistingDescription = dr.GetString(dr.GetOrdinal("description"));
                                    existingInvoiceDescriptions.Add(exDesc);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return existingInvoiceDescriptions;
        }
    }
}