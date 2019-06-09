using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using InvoicesGenerator.Classes;
using InvoicesGenerator.Properties;

namespace InvoicesGenerator
{
    public class ReportDAL
    {

        public static void GetInvoiceItems(int invoiceId, List<KeyValuePair<string, object>> templateData)
        {
            List<KeyValuePair<string, object>> invoiceItems = null;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select 
                                                        amount,
                                                        description,
                                                        unit,
                                                        total,
                                                        metric_unit
                                                        from invoices
                                                        where invoices.group_id = @invoiceID
                                                        and is_deleted=0
                                                        ";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@invoiceID", invoiceId);

                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            int itemCount = 0;
                            int.TryParse(Settings.Default.max_invoice_items, out itemCount);
                            int tempItemCount = 1;
                            int itemsNumber = 5; // starts with 5, then concats 1 and then 1 as in 51_1 alla den to epiane etsi opote to gurisa se 511 klp
                            while (reader.Read())
                            {
                                invoiceItems = new List<KeyValuePair<string, object>>();
                                InvoiceItem invoiceItem = new InvoiceItem(
                                    decimal.Parse(reader["amount"].ToString()),
                                    reader["description"].ToString(),
                                    decimal.Parse(reader["unit"].ToString()),
                                    decimal.Parse(reader["total"].ToString()),
                                    reader["metric_unit"].ToString());

                                decimal tempDecimal = 0;
                                if (tempItemCount <= itemCount)
                                {
                                    // new template
                                    templateData.Add(new KeyValuePair<string, object>("{{" + itemsNumber + "" + tempItemCount + "1}}", invoiceItem.description.Replace("\r\n", "<br/>")));
                                    templateData.Add(new KeyValuePair<string, object>("{{" + itemsNumber + "" + tempItemCount + "2}}", invoiceItem.amount.Value.ToString("0.00")));
                                    tempDecimal = invoiceItem.unit.HasValue ? invoiceItem.unit.Value : 0;
                                    templateData.Add(new KeyValuePair<string, object>("{{" + itemsNumber + "" + tempItemCount + "3}}", General.formatAmount(tempDecimal)));
                                    tempDecimal = invoiceItem.total.HasValue ? invoiceItem.total.Value : 0;
                                    templateData.Add(new KeyValuePair<string, object>("{{" + itemsNumber + "" + tempItemCount + "4}}", General.formatAmount(tempDecimal)));
                                    templateData.Add(new KeyValuePair<string, object>("{{" + itemsNumber + "" + tempItemCount + "5}}", invoiceItem.MetricUnit));

                                    tempItemCount++;
                                }

                                // old template
                                invoiceItems.Add(new KeyValuePair<string, object>("item_description", invoiceItem.description.Replace("\r\n", "<br/>")));
                                invoiceItems.Add(new KeyValuePair<string, object>("item_amount", invoiceItem.amount));
                                tempDecimal = invoiceItem.unit.HasValue ? invoiceItem.unit.Value : 0;
                                invoiceItems.Add(new KeyValuePair<string, object>("item_unit", General.formatAmount(tempDecimal)));
                                tempDecimal = invoiceItem.total.HasValue ? invoiceItem.total.Value : 0;
                                invoiceItems.Add(new KeyValuePair<string, object>("item_total", General.formatAmount(tempDecimal)));

                                templateData.Add(new KeyValuePair<string, object>("invoice_items:itemTemplate", invoiceItems));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                String e = ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="templateData"></param>
        /// <param name="edeiksh">einai apo ta setting</param>
        public static void GetInvoiceDetails(int invoiceId, List<KeyValuePair<string, object>> templateData, string edeiksh = "")
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select
                                                subtotal,
                                                tax,
                                                total,
                                                invoice_types.name as invoice_type,
                                                number as currentPrintNumber,
                                                is_credit,
                                                location,
                                                comments,
                                                date,
                                                user_comments,
                                                invoices_group.invoice_order,
                                                loading_place,
                                                moving_purpose,
                                                destination,
                                                customer_phone,
                                                customer_city,
                                                customer_postalcode
                                                from invoices_group
                                                inner join invoice_types
                                                on invoice_types.id = invoices_group.invoice_type
                                                where invoices_group.id = @invoiceID
                                                and is_deleted=0
                                                ";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@invoiceID", invoiceId);

                        Invoice invoiceDetails = null;
                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoiceDetails = new Invoice(
                                   decimal.Parse(reader["subtotal"].ToString()),
                                   decimal.Parse(reader["tax"].ToString()),
                                   decimal.Parse(reader["total"].ToString()),
                                   reader["invoice_type"].ToString(),
                                   int.Parse(reader["is_credit"].ToString()),
                                   reader["location"].ToString(),
                                   DateTime.Parse(reader["date"].ToString()),
                                   int.Parse(reader["currentPrintNumber"].ToString()),
                                   reader["comments"].ToString(),
                                   decimal.Parse(reader["total"].ToString()) - decimal.Parse(reader["subtotal"].ToString()),
                                   reader["user_comments"].ToString(),
                                   reader["invoice_order"].ToString(),
                                   reader["loading_place"].ToString(),
                                   reader["moving_purpose"].ToString(),
                                   reader["destination"].ToString(),
                                   reader["customer_phone"].ToString(),
                                   reader["customer_city"].ToString(),
                                   reader["customer_postalcode"].ToString());
                            }
                        }

                        if (invoiceDetails != null)
                        {
                            // new template
                            templateData.Add(new KeyValuePair<string, object>("{{1}}", invoiceDetails.InvoiceTypeName));
                            templateData.Add(new KeyValuePair<string, object>("{{2}}", invoiceDetails.Is_creditVF));
                            templateData.Add(new KeyValuePair<string, object>("{{3}}", invoiceDetails.CurrentPrintNumber.ToString()));
                            templateData.Add(new KeyValuePair<string, object>("{{4}}", invoiceDetails.Location));
                            templateData.Add(new KeyValuePair<string, object>("{{5}}", invoiceDetails.Date.Value.ToShortDateString()));
                            templateData.Add(new KeyValuePair<string, object>("{{17}}", invoiceDetails.UserComments.Replace("\r\n", "<br/>")));
                            decimal tempDecimal = invoiceDetails.Subtotal.HasValue ? invoiceDetails.Subtotal.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("{{18}}", General.formatAmount(tempDecimal)));
                            templateData.Add(new KeyValuePair<string, object>("{{19}}", String.Format("{0:N1}" + " %", invoiceDetails.Tax)));
                            tempDecimal = invoiceDetails.TaxNumeric.HasValue ? invoiceDetails.TaxNumeric.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("{{20}}", General.formatAmount(tempDecimal)));
                            tempDecimal = invoiceDetails.TotalVF.HasValue ? invoiceDetails.TotalVF.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("{{21}}", General.formatAmount(tempDecimal)));

                            if (invoiceDetails.InvoiceOrder != null && !string.IsNullOrEmpty(invoiceDetails.InvoiceOrder))
                            {
                                templateData.Add(new KeyValuePair<string, object>("{{22}}", invoiceDetails.InvoiceOrder));
                            }

                            templateData.Add(new KeyValuePair<string, object>("{{23}}", invoiceDetails.LoadingPlace));
                            templateData.Add(new KeyValuePair<string, object>("{{24}}", invoiceDetails.Destination));
                            templateData.Add(new KeyValuePair<string, object>("{{25}}", invoiceDetails.MovingPurpose));
                            templateData.Add(new KeyValuePair<string, object>("{{26}}", invoiceDetails.CustomerPhone));
                            templateData.Add(new KeyValuePair<string, object>("{{27}}", invoiceDetails.CustomerCity));
                            templateData.Add(new KeyValuePair<string, object>("{{28}}", invoiceDetails.CustomerPostalCode));

                            templateData.Add(new KeyValuePair<string, object>("{{29}}", edeiksh));

                            // old template
                            templateData.Add(new KeyValuePair<string, object>("invoice_invoiceTypeName", invoiceDetails.InvoiceTypeName));
                            templateData.Add(new KeyValuePair<string, object>("invoice_isCreditVf", invoiceDetails.Is_creditVF));
                            templateData.Add(new KeyValuePair<string, object>("invoice_currentPrintNumber", invoiceDetails.CurrentPrintNumber.ToString()));
                            templateData.Add(new KeyValuePair<string, object>("invoice_location", invoiceDetails.Location));
                            templateData.Add(new KeyValuePair<string, object>("invoice_date", invoiceDetails.Date.Value.ToShortDateString()));
                            templateData.Add(new KeyValuePair<string, object>("invoice_userComments", invoiceDetails.UserComments.Replace("\r\n", "<br/>")));
                            //decimal tempDecimal = invoiceDetails.Subtotal.HasValue ? invoiceDetails.Subtotal.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("invoice_subtotal", General.formatAmount(tempDecimal)));
                            templateData.Add(new KeyValuePair<string, object>("invoice_tax", String.Format("{0:N1}" + " %", invoiceDetails.Tax)));
                            tempDecimal = invoiceDetails.TaxNumeric.HasValue ? invoiceDetails.TaxNumeric.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("invoice_taxNumeric", General.formatAmount(tempDecimal)));
                            tempDecimal = invoiceDetails.TotalVF.HasValue ? invoiceDetails.TotalVF.Value : 0;
                            templateData.Add(new KeyValuePair<string, object>("invoice_total", General.formatAmount(tempDecimal)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SoftloopTools.ExceptionLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
            }
        }

        public static void GetCustomer(int invoiceId, List<KeyValuePair<string, object>> templateData)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select 
                                                customer_name, 
                                                customer_description,
                                                customer_address,
                                                customer_taxoffice,
                                                customer_taxnumber
                                                from invoices_group
                                                where invoices_group.id = @invoiceID
                                                ";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@invoiceID", invoiceId);

                        Invoice customer = null;
                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Invoice(reader["customer_name"].ToString(),
                                   reader["customer_description"].ToString(),
                                   reader["customer_address"].ToString(),
                                   reader["customer_taxoffice"].ToString(),
                                   reader["customer_taxnumber"].ToString());

                            }
                        }

                        if (customer != null)
                        {
                            // new template
                            templateData.Add(new KeyValuePair<string, object>("{{12}}", customer.Customer_name));
                            templateData.Add(new KeyValuePair<string, object>("{{13}}", customer.Customer_description));
                            templateData.Add(new KeyValuePair<string, object>("{{14}}", customer.Customer_address));
                            templateData.Add(new KeyValuePair<string, object>("{{15}}", customer.Customer_taxnumber));
                            templateData.Add(new KeyValuePair<string, object>("{{16}}", customer.Customer_taxoffice));

                            // old template
                            templateData.Add(new KeyValuePair<string, object>("customer_name", customer.Customer_name));
                            templateData.Add(new KeyValuePair<string, object>("customer_description", customer.Customer_description));
                            templateData.Add(new KeyValuePair<string, object>("customer_address", customer.Customer_address));
                            templateData.Add(new KeyValuePair<string, object>("customer_taxnumber", customer.Customer_taxnumber));
                            templateData.Add(new KeyValuePair<string, object>("customer_taxoffice", customer.Customer_taxoffice));
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public static void GetSealDetails(int sealId, List<KeyValuePair<string, object>> templateData)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select 
                                                name,occupation,address,taxnumber,
                                                phone,taxoffice,vat,location,has_order
                                                from seal
                                                where seal.id = @SealID
                                                ";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@SealID", sealId);

                        SealDetails slDetails = null;
                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                slDetails = new SealDetails(reader["name"].ToString(),
                                    reader["occupation"].ToString(),
                                    reader["address"].ToString(),
                                    reader["taxnumber"].ToString(),
                                    reader["phone"].ToString(),
                                    reader["taxoffice"].ToString(),
                                    decimal.Parse(reader["vat"].ToString()),
                                    reader["location"].ToString(),
                                    int.Parse(reader["has_order"].ToString()));
                            }
                        }

                        if (slDetails != null)
                        {
                            // new template
                            templateData.Add(new KeyValuePair<string, object>("{{6}}", slDetails.Name));
                            templateData.Add(new KeyValuePair<string, object>("{{7}}", slDetails.Occupation));
                            templateData.Add(new KeyValuePair<string, object>("{{8}}", slDetails.Address));
                            templateData.Add(new KeyValuePair<string, object>("{{9}}", slDetails.Phone));
                            templateData.Add(new KeyValuePair<string, object>("{{10}}", "EL " + slDetails.Taxnumber));
                            templateData.Add(new KeyValuePair<string, object>("{{11}}", slDetails.Taxoffice));

                            // old template
                            templateData.Add(new KeyValuePair<string, object>("seal_name", slDetails.Name));
                            templateData.Add(new KeyValuePair<string, object>("seal_occupation", slDetails.Occupation));
                            templateData.Add(new KeyValuePair<string, object>("seal_address", slDetails.Address));
                            templateData.Add(new KeyValuePair<string, object>("seal_phone", slDetails.Phone));
                            templateData.Add(new KeyValuePair<string, object>("seal_taxnumber", slDetails.Taxnumber));
                            templateData.Add(new KeyValuePair<string, object>("seal_taxoffice", slDetails.Taxoffice));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SoftloopTools.ExceptionLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
            }
        }

        public static void GetCentralized(List<KeyValuePair<string, object>> templateData)
        {
            List<KeyValuePair<string, object>> centralizedItems = null;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select customer_name,
                                                customer_taxnumber ,
                                                count(*) invoices_count, 
                                                sum(subtotal) sum_subtotal, 
                                                sum(invoices_group.total) sum_total, 
                                                sum((tax / 100) * subtotal) taxvalue_sum
                                                from invoices_group
                                                where invoices_group.is_deleted = 0
                                                group by customer_name,customer_taxnumber 
                                                ";
                        command.Parameters.Clear();

                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                centralizedItems = new List<KeyValuePair<string, object>>();

                                centralizedItems.Add(new KeyValuePair<string, object>("customer_name", reader["customer_name"].ToString()));
                                centralizedItems.Add(new KeyValuePair<string, object>("customer_taxnumber", reader["customer_taxnumber"].ToString()));
                                centralizedItems.Add(new KeyValuePair<string, object>("invoices_count", int.Parse(reader["invoices_count"].ToString())));
                                centralizedItems.Add(new KeyValuePair<string, object>("sum_subtotal", General.formatAmount(decimal.Parse(reader["sum_subtotal"].ToString()))));
                                centralizedItems.Add(new KeyValuePair<string, object>("taxvalue_sum", General.formatAmount(decimal.Parse(reader["taxvalue_sum"].ToString()))));
                                centralizedItems.Add(new KeyValuePair<string, object>("sum_total", General.formatAmount(decimal.Parse(reader["sum_total"].ToString()))));

                                templateData.Add(new KeyValuePair<string, object>("centralized_items:centralizedItemTemplate", centralizedItems));
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
