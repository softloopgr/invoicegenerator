using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace InvoicesGenerator
{
    public class InvoiceItem
    {
        public int? id { get; set; }

        private decimal? _amount;
        public decimal? amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private decimal? _unit;
        public decimal? unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private decimal? _total;
        public decimal? total
        {
            get { return _total; }
            set { _total = value; }
        }

        private decimal? _vatPrice;
        public decimal? vatPrice
        {
            get { return _vatPrice; }
            set { _vatPrice = value; }
        }

        private string _metric_unit;
        public string MetricUnit
        {
            get { return _metric_unit; }
            set { _metric_unit = value; }
        }

        private int? _isDeleted;
        public int? is_deleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public InvoiceItem()
        {

        }

        public InvoiceItem(decimal? amount, string description, decimal? unit, decimal? total, string metric_unit)
        {
            _amount = amount;
            _description = description;
            _unit = unit;
            _total = total;
            _metric_unit = metric_unit;
        }

        public void save(int GroupID, SqlCeCommand cmd)
        {
            if (this.id == null || this.id < 0)
            {
                int? id;
                InvoiceDAL.insert(this, GroupID, out id, cmd);
                this.id = id;
            }
            else
            {
                InvoiceDAL.update(this, cmd);
            }
        }
    }
}