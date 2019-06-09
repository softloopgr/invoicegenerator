using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoicesGenerator
{
    public static class GreekVerbalMoney
    {
        private static string[] mF = { "", "ΜΙΑ ", "", "ΤΡΕΙΣ ", "ΤΕΣΣΕΡΙΣ " };
        private static string[] m = { "", "ΕΝΑ ", "ΔΥΟ ", "ΤΡΙΑ ", "ΤΕΣΣΕΡΑ ", "ΠΕΝΤΕ ", "ΕΞΙ ", "ΕΠΤΑ ", "ΟΚΤΩ ", "ΕΝΝΕΑ " };
        private static string[] d1 = { "ΔΕΚΑ ", "ΕΝΤΕΚΑ ", "ΔΩΔΕΚΑ " };        //Διαφοροποίησεις των 11,12 ως προς τα 13, 14, 15, ... 
        private static string[] d = { "", "ΔΕΚΑ", "ΕΙΚΟΣΙ ", "ΤΡΙΑΝΤΑ ", "ΣΑΡΑΝΤΑ ", "ΠΕΝΗΝΤΑ ", "ΕΞΗΝΤΑ ", "ΕΒΔΟΜΗΝΤΑ ", "ΟΓΔΟΝΤΑ ", "ΕΝΕΝΗΝΤΑ " };
        private static string[] e = { "", "ΕΚΑΤΟ", "ΔΙΑΚΟΣΙ", "ΤΡΙΑΚΟΣΙ", "ΤΕΤΡΑΚΟΣΙ", "ΠΕΝΤΑΚΟΣΙ", "ΕΞΑΚΟΣΙ", "ΕΠΤΑΚΟΣΙ", "ΟΚΤΑΚΟΣΙ", "ΕΝΝΙΑΚΟΣΙ" };
        private static string[] idx = { "ΛΕΠΤΑ", "ΕΥΡΩ ", "ΧΙΛΙΑΔΕΣ ", "ΕΚΑΤΟΜΜΥΡΙ", "ΔΙΣ", "ΤΡΙΣ", "ΤΕΤΡΑΚΙΣ ", "ΠΕΝΤΑΚΙΣ " };

        public static double Round(double value)
        {
            return Round(value, 0);
        }

        public static double Round(double value, short precision)
        {
            return (double)Math.Round((decimal)value, precision);
        }

        public static string GetVerbal(double money)
        {
            return GetVerbal(money, true);
        }

        public static string GetVerbal(double money, bool showZero)
        {
            return GetVerbal(money, showZero, true);
        }

        public static string GetVerbal(double money, bool showZero, bool showCurrency)
        {
            string tmpStr;
            string retStr;
            string str;
            short index = 0;
            bool isZero = true;
            bool isNegative = false;

            retStr = tmpStr = str = "";

            if (money < 0)
            {
                money = -money;
                isNegative = true;
            }

            if (money != (long)money)
            {
                short value = (short)Round(100 * money - 100 * Math.Floor(money), 0);
                if (value >= 100)
                {
                    value -= 100;
                    money += 1.0;
                }

                money = (long)money;
                if (value > 0)
                {
                    isZero = false;

                    tmpStr = GetValue(value, index, showCurrency);
                    if (money >= 1 && value > 0)
                    {
                        str += "ΚΑΙ ";
                    }
                    str += tmpStr;
                }
            }

            while (money >= 1)
            {
                isZero = false;
                money /= 1000;
                index += 1;
                short value = (short)(Round(money - (long)money, 3) * 1000);
                tmpStr = GetValue(value, index, showCurrency);
                money = (long)money;
                tmpStr += str;
                str = tmpStr;
            }

            if (isZero)
            {
                if (showZero)
                {
                    str = "ΜΗΔΕΝ ";
                    if (showCurrency)
                    {
                        str += idx[1];
                    }
                }
            }
            else
            {
                if (isNegative)
                    retStr = "MEION ";
            }

            retStr += str;
            return retStr;
        }

        static string GetValue(short money, short index, bool showCurrency)
        {
            if (index == 2 && money == 1)
            {
                return "ΧΙΛΙΑ ";
            }

            string str = "";
            int ekatontades = (int)(money / 100);
            int dekmon = money - ekatontades * 100;
            int dekades = (int)(dekmon / 10);
            int monades = dekmon - dekades * 10;

            //EKATONTADES
            if (ekatontades == 1)
            {
                if (dekmon == 0)
                {
                    str = e[1] + " ";
                }
                else
                {
                    str = e[1] + "Ν ";
                }
            }
            else if (ekatontades > 1)
            {
                if (index == 2)
                {
                    str = e[ekatontades] + "ΕΣ ";
                }
                else
                {
                    str = e[ekatontades] + "Α ";
                }
            }

            //DEKADES
            switch (dekmon)
            {
                case 10:
                    str += d1[monades];    //"ΔΕΚΑ " με κενό στο τέλος
                    break;
                case 11:
                    str += d1[monades];
                    monades = 0;
                    break;
                case 12:
                    str += d1[monades];
                    monades = 0;
                    break;
                default:
                    str += d[dekades];
                    break;
            }

            //MONADES
            if (index == 2 && (monades == 1 || monades == 3 || monades == 4))
            {
                str += mF[monades];
            }
            else
            {
                if (dekmon < 10 || dekmon > 12)
                {
                    str += m[monades];
                }
            }

            if (str.Length > 0 || index == 1)
            {
                if (index == 0 && money == 1/*monades == 1 && dekades == 0 && ekatontades == 0*/)
                {
                    if (showCurrency)
                    {
                        str += "ΛΕΠΤΟ";
                    }
                }
                else
                {
                    if (index > 1 || showCurrency)
                    {
                        str += idx[index];
                        if (index > 2)
                        {
                            if (index > 3)
                            {
                                str += idx[3];
                            }
                            if (money > 1)
                            {
                                str += "Α ";
                            }
                            else
                            {
                                str += "Ο ";
                            }
                        }
                    }
                }
            }

            return str;
        }
    }
}
