using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoicesGenerator
{
    public class Invoice
    {
        private int? _id;
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #region Customer Details

        private string _customer_name;
        public string Customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        private string _customer_description;
        public string Customer_description
        {
            get { return _customer_description; }
            set { _customer_description = value; }
        }

        private string _customer_address;
        public string Customer_address
        {
            get { return _customer_address; }
            set { _customer_address = value; }
        }

        private string _customer_taxoffice;
        public string Customer_taxoffice
        {
            get { return _customer_taxoffice; }
            set { _customer_taxoffice = value; }
        }

        private string _customer_taxnumber;
        public string Customer_taxnumber
        {
            get { return _customer_taxnumber; }
            set { _customer_taxnumber = value; }
        }

        #endregion

        #region Extra Details

        private decimal? _subtotal;
        public decimal? Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        private decimal? _tax;
        public decimal? Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        private decimal? _totalVF;
        public decimal? TotalVF
        {
            get { return _totalVF; }
            set { _totalVF = value; }
        }

        private string _invoiceTypename;
        public string InvoiceTypeName
        {
            get { return _invoiceTypename; }
            set { _invoiceTypename = value; }
        }

        private int? _is_credit;
        public int? Is_credit
        {
            get { return _is_credit; }
            set { _is_credit = value; }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private DateTime? _date;
        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private int? _currentPrintNumber;
        public int? CurrentPrintNumber
        {
            get { return _currentPrintNumber; }
            set { _currentPrintNumber = value; }
        }

        private string _comments;
        public string comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        private string _is_creditVF;
        public string Is_creditVF
        {
            get
            {
                _is_creditVF = _is_credit == 1 ? "Με πίστωση" : "Μετρητοίς";
                return _is_creditVF;
            }
            set
            {
                _is_creditVF = value == "1" ? "Με πίστωση" : "Μετρητοίς";
            }
        }

        private decimal? _taxNumeric;
        public decimal? TaxNumeric
        {
            get { return _taxNumeric; }
            set { _taxNumeric = value; }
        }

        #endregion

        private int? _number;
        public int? Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private int? _is_deleted;
        public int? Is_deleted
        {
            get { return _is_deleted; }
            set { _is_deleted = value; }
        }

        public decimal? Total
        {
            get { return InvoiceItems != null ? InvoiceItems.Where(item => item.id != -1000).Sum(item => item.total) : 0; }
        }

        public decimal? totalWithVat
        {
            get
            {
                return Total + ((Total * Tax) / 100);
            }
        }


        public decimal? vatPrice
        {
            get { return InvoiceItems != null ? InvoiceItems.Where(item => item.id != -1000).Sum(item => item.vatPrice) : 0; }
        }

        private int? _status;
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string statusText
        {
            get
            {
                switch (this.Status)
                {
                    case 1: return "ΕΚΤΥΠΩΜΕΝΟ";
                    case 0:
                    default: return "ΜΗ ΕΚΤΥΠΩΜΕΝΟ";
                }
            }
            set { }
        }

        private int? _invoice_type;
        public int? Invoice_type
        {
            get { return _invoice_type; }
            set { _invoice_type = value; }
        }

        private int? _enable_hold;
        public int? Enable_hold
        {
            get { return _enable_hold; }
            set { _enable_hold = value; }
        }

        //epistrefei to total * to poso einai h parakrathsh
        public decimal? hold_value
        {
            get { return Total * hold_percent; }
        }

        public decimal? hold_value_database { get; set; }

        private decimal? hold_percent;
        public decimal? Hold_percent
        {
            get
            {
                return hold_percent * 100;
            }
            set { hold_percent = value; }
        }

        private string _userComments;
        public string UserComments
        {
            get { return _userComments; }
            set { _userComments = value; }
        }

        private string _invoiceOrder;
        public string InvoiceOrder
        {
            get { return _invoiceOrder; }
            set { _invoiceOrder = value; }
        }

        private string _customerCity;
        public string CustomerCity
        {
            get { return _customerCity; }
            set { _customerCity = value; }
        }

        private string _customerPostalCode;
        public string CustomerPostalCode
        {
            get { return _customerPostalCode; }
            set { _customerPostalCode = value; }
        }

        private string _customerPhone;
        public string CustomerPhone
        {
            get { return _customerPhone; }
            set { _customerPhone = value; }
        }

        // gia deltio apostolhs
        private string _loadingPlace;
        public string LoadingPlace
        {
            get { return _loadingPlace; }
            set { _loadingPlace = value; }
        }

        private string _movingPurpose;
        public string MovingPurpose
        {
            get { return _movingPurpose; }
            set { _movingPurpose = value; }
        }

        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        public List<InvoiceItem> InvoiceItems { get; set; }

        public Invoice()
        {

        }

        /// <summary>
        /// Customer Set
        /// </summary>
        /// <param name="customer_name"></param>
        /// <param name="customer_description"></param>
        /// <param name="customer_address"></param>
        /// <param name="customer_taxoffice"></param>
        /// <param name="customer_taxnumber"></param>
        public Invoice(string customer_name, string customer_description, string customer_address,
                       string customer_taxoffice, string customer_taxnumber)
        {
            _customer_name = customer_name;
            _customer_description = customer_description;
            _customer_address = customer_address;
            _customer_taxoffice = customer_taxoffice;
            _customer_taxnumber = customer_taxnumber;
        }

        /// <summary>
        /// Extra δεδομένα
        /// </summary>
        /// <param name="subtotal"></param>
        /// <param name="tax"></param>
        /// <param name="total"></param>
        /// <param name="invoice_type"></param>
        /// <param name="is_credit"></param>
        /// <param name="location"></param>
        /// <param name="date"></param>
        public Invoice(decimal? subtotal, decimal? tax, decimal? total, string invoice_type,
                       int? is_credit, string location, DateTime date, int? currentPrintNumber, string comments, decimal taxNumeric, string userComments, string invoiceOrder,
                        string loadingPlace, string movingPurpose, string destination, string customerPhone, string customerCity, string customerPostalCode)
        {
            _subtotal = subtotal;
            _tax = tax;
            _totalVF = total;
            _invoiceTypename = invoice_type;
            _is_credit = is_credit;
            _location = location;
            _date = date;
            _currentPrintNumber = currentPrintNumber;
            this.comments = comments;
            Is_creditVF = is_credit.ToString();
            _taxNumeric = taxNumeric;
            _userComments = userComments;
            _invoiceOrder = invoiceOrder;
            _loadingPlace = loadingPlace;
            _movingPurpose = movingPurpose;
            _destination = destination;
            // afto mphke edo kai oxi ston customer giati nai men einai tou pelath alla afora to deltio apostolhs
            _customerPhone = customerPhone;
            _customerCity = customerCity;
            _customerPostalCode = customerPostalCode;
        }

        public bool save()
        {
            bool ret;
            if (this.Id == null)
            {
                int? id;
                ret = InvoiceDAL.insert(this, out id);
                this.Id = id;
            }
            else
            {
                ret = InvoiceDAL.update(this);
            }
            return ret;
        }
    }

    public class ExistingDescriptions
    {
        public string ExistingDescription { get; set; }
    }
}
