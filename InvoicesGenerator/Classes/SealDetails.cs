using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvoicesGenerator;

namespace InvoicesGenerator.Classes
{
    public class SealDetails
    {
        public int? id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _occupation;
        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _taxnumber;
        public string Taxnumber
        {
            get { return _taxnumber; }
            set { _taxnumber = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _taxoffice;
        public string Taxoffice
        {
            get { return _taxoffice; }
            set { _taxoffice = value; }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private decimal _vat;
        public decimal Vat
        {
            get { return _vat; }
            set { _vat = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        // ean to invoice exei seira
        private int _hasOrder;
        public int HasOrder
        {
            get { return _hasOrder; }
            set { _hasOrder = value; }
        }

        public SealDetails()
        {

        }

        public SealDetails(string name, string occupation, string address,
                            string taxnumber, string phone, string taxoffice,
                            decimal vat, string location, int hasOrder)
        {
            _name = name;
            _occupation = occupation;
            _address = address;
            _taxnumber = taxnumber;
            _phone = phone;
            _taxoffice = taxoffice;
            _vat = vat;
            _location = location;
            _hasOrder = hasOrder;
        }

        public bool save()
        {
            bool ret;
            if (id == null || id < 0)
            {
                ret = InvoiceDAL.insertCompany(this);
            }
            else
            {
                ret = InvoiceDAL.updateCompany(this);
            }
            return ret;
        }
    }
}
