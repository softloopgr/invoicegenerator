using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using InvoicesGenerator.Properties;
using Microsoft.Win32;

namespace InvoicesGenerator
{
    public static class General
    {
        public static bool CheckTaxNumber(string tax_number)
        {
            //return true;
            long remainder, sum;
            int nn, k;

            if (tax_number.Length != 9)
                return false;
            for (nn = 2, k = 7, sum = 0; k >= 0; k--, nn += nn)
                sum += nn * (tax_number[k] - '0');
            remainder = sum % 11;

            return (remainder == 10)
                   ? tax_number[8] == '0'
                    : (tax_number[8] - '0') == remainder;
        }

        public static string CalculateHash(string input, string saltSeed)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltSeed);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            HMACMD5 hmacSHA1 = new HMACMD5(saltBytes);
            byte[] saltedHash = hmacSHA1.ComputeHash(inputBytes);
            return Convert.ToBase64String(saltedHash);
        }

        #region MesageBoxes
        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ExclamationMessage(string message)
        {
            MessageBox.Show(message, "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static void InformationMessage(string message)
        {
            MessageBox.Show(message, "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult QuestionMessage(string message)
        {
            return MessageBox.Show(message, "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion

        #region Compobox Lists
        public static List<KeyValuePair<int, string>> generateSimpleHourList(int from, int to)
        {
            List<KeyValuePair<int, string>> hoursList = new List<KeyValuePair<int, string>>();
            for (int i = @from; i <= to; i++)
            {
                hoursList.Add(new KeyValuePair<int, string>(i, String.Format("{0:00}:00", i)));
            }
            return hoursList;
        }
        internal static List<KeyValuePair<int, string>> generateSimpleWeekDaysList(int from, int to)
        {
            string[] days = new string[7] { "Δευτέρα", "Τρίτη", "Τετάρτη", "Πέμπτη", "Παρασκευή", "Σάββατο", "Κυριακή" };
            List<KeyValuePair<int, string>> daysList = new List<KeyValuePair<int, string>>();
            for (int i = @from; i <= to; i++)
            {
                daysList.Add(new KeyValuePair<int, string>(i, days[i]));
            }
            return daysList;
        }

        #endregion

        public static List<KeyValuePair<int, string>> getBanks()
        {
            List<KeyValuePair<int, string>> banks = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT  *
                        FROM banks
                        ORDER BY name";

                        cmd.Parameters.Clear();

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                KeyValuePair<int, string> bank =
                                    new KeyValuePair<int, string>(
                                        Int32.Parse(dr["id"].ToString()),
                                        dr["name"].ToString());
                                banks.Add(bank);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.Message);
            }
            return banks;
        }

        public static List<KeyValuePair<int, string>> getBranches(int bank_id)
        {

            List<KeyValuePair<int, string>> branches = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        SELECT  *
                        FROM branches
                        WHERE bank_id=@bank_id
                        ORDER BY name";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@bank_id", bank_id);

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                KeyValuePair<int, string> branch =
                                    new KeyValuePair<int, string>(
                                        Int32.Parse(dr["id"].ToString()),
                                        dr["name"].ToString());
                                branches.Add(branch);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.Message);
            }
            return branches;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public sealed class BankersRound
        {

            public static decimal RoundAwayFromZero(decimal value, int digits)
            {

                int sign = Math.Sign(value);

                decimal scale = Convert.ToDecimal(Math.Pow(10.0, digits));

                double round = Math.Floor(Convert.ToDouble(Math.Abs(value) * scale) + 0.5);

                return (sign * Convert.ToDecimal(round) / scale);

            }

        }

        public static string formatAmount(decimal amount)
        {
            //NumberFormatInfo nfi = new NumberFormatInfo()
            //{
            //    NumberGroupSeparator = ".",
            //    NumberDecimalSeparator = ",",
            //    NumberDecimalDigits = 2
            //};
            //string amountText = amount.ToString("n", nfi);

            return String.Format("{0:N}" + " €", amount); //decimal.Parse(amountText);
        }

        public static bool IsConnected()
        {
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
            //     new Uri(Settings.Default.InvoicesGenerator_gr_gsis_www1_RgWsBasStoixN)
            //     );
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                 new Uri("http://ec.europa.eu/taxation_customs/vies/checkVatService.wsdl")
                 );
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    response.Close();
                    request = null;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                request = null;
                return false;
            }
        }
        public static string generateDateText(DateTime date)
        {
            string dateText = dateToWord(date.Day) + " " + monthToWord(date.Month);// +" " + date.Year.ToString();
            return dateText;
        }

        public static string dateToWord(int date)
        {
            string retWord = "";
            object[,] ArrWordList = { 
                { 1, "Πρώτη" },{ 2, "Δεύτερη" },{ 3, "Τρίτη" },{ 4, "Τετάρτη" },{ 5, "Πέμπτη" },{ 6, "Έκτη" },{ 7, "Εβδόμη" },{ 8, "Ογδόη" },{ 9, "Εννάτη" }, 
                { 10, "Δεκάτη" },{ 11, "Ενδεκάτη" },{ 12, "Δωδεκάτη" },{ 13, "Δεκάτη Τρίτη" },{ 14, "Δεκάτη Τετάρτη" }, { 15, "Δεκάτη Πέμπτη" }, { 16, "Δεκάτη Έκτη" }, { 17, "Δεκάτη Εβδόμη" },  { 18, "Δεκάτη Ογδόη" }, { 19, "Δεκάτη Εννάτη" }, 
                { 20, "Εικοστή" },{ 21, "Εικοστή Πρώτη" },{ 22, "Εικοστή Δευτέρα" },{ 23, "Εικοστή Τρίτη" },{ 24, "Εικοστή Τετάρτη" }, { 25, "Εικοστή Πέμπτη" }, { 26, "Εικοστή Έκτη" }, { 27, "Εικοστή Εβδόμη" },  { 28, "Εικοστή Ογδόη" }, { 29, "Εικοστή Εννάτη" }, 
                { 30, "Τριακοστή" },{31, "Τριακοστή Πρώτη" }
            };

            for (int i = 0; i <= ArrWordList.Length; i++)
            {
                if (date.ToString() == ArrWordList[i, 0].ToString())
                {
                    retWord = ArrWordList[i, 1].ToString();
                    break;
                }
            }
            return retWord;
        }

        public static string monthToWord(int month)
        {
            string retWord = "";
            object[,] ArrWordList = { 
                { 1, "Ιανουαρίου" },{ 2, "Φεβρουαρίου" },{ 3, "Μαρτίου" },{ 4, "Απριλίου" },{ 5, "Μαϊου" },{ 6, "Ιουνίου" },
                { 7, "Ιουλίου" },{ 8, "Αυγούστου" },{ 9, "Σεπτεμβρίου" }, { 10, "Οκτωβρίου" },{ 11, "Νοεμβρίου" },{ 12, "Δεκεμβρίου" }
            };

            for (int i = 0; i <= ArrWordList.Length; i++)
            {
                if (month.ToString() == ArrWordList[i, 0].ToString())
                {
                    retWord = ArrWordList[i, 1].ToString();
                    break;
                }
            }
            return retWord;
        }

        public static object[] serializeToObject<T>(T paramObject)
        {
            Type classType = paramObject.GetType();
            FieldInfo[] classProperties = classType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            List<string> propertiesValues = new List<string>();

            foreach (FieldInfo prop in classProperties)
            {
                object propertyValue = prop.GetValue(paramObject);
                if (propertyValue != null)
                {
                    if (typeof(Decimal?).IsAssignableFrom(prop.FieldType) && prop.Name != "_tax")
                    {
                        propertiesValues.Add(formatAmount(((decimal?)propertyValue).Value));
                    }
                    else if (typeof(DateTime?).IsAssignableFrom(prop.FieldType))
                    {
                        propertiesValues.Add(((DateTime)propertyValue).ToShortDateString());
                    }
                    else
                    {
                        propertiesValues.Add(prop.Name == "_tax"
                                                 ? String.Format("{0:N1}" + " %", propertyValue)
                                                 : propertyValue.ToString());
                    }
                }
            }

            return propertiesValues.ToArray();
        }

        public static bool emailCheck(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void dropInternetExplorerPrintHeaderFooter()
        {
            string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.SetValue("footer", "");
                    key.SetValue("header", "");
                    key.SetValue("margin_top", 1.00);
                    key.SetValue("margin_bottom", 1.00);
                    key.SetValue("margin_left", 1.25);
                    key.SetValue("margin_right", 1.25);
                }
            }
        }

    }
}
