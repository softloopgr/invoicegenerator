using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using InvoicesGenerator.Properties;

namespace InvoicesGenerator
{
    public static class Trial
    {
        public enum TrialStatus
        {
            OK = 1,
            TRIAL = 0,
            LOCKED = -1,
            EXPIRED = 2
        };
        public static string rsaKey = "<RSAKeyValue><Modulus>mWNOaPwugvILCYnALmg2gfWadDosbj199u7KIc3NGeAFyDI6gXJ06sSgXttkVkSppfkUgrIu9Efw31pZ3Oveb0riYgVJ4j+DpvNp4prYQuliY6cFiiEWe8G/jIf0gQ0CmROQCgiVGTPyk6vKataKVOdHslJl/qWlFokCNdSO0KUl1oI5kRoL9r0BqF++KzOWKK9bKb+2ShbMhwTgWuhC92e0uBjvADEpaHj9bDoTvquI9C76wSkQHt62XOaxzg6UIzdSU5VXZNSQjCJtsw2dtp5KcaEgWxJ6Z5/WQZY+nNW6RNCs5JpRouPaMImnB+4kAZ/jzjOZxvwa9BdYvOw+kCrXsfR2xwroMwBGqm87M/u7TuavElRJTPSL2F2JFJ0gVex6i+HhlRKuNJ58yFnjV/DKEUE8dwXvscUrtTky5EVQrUXBXRIVC1zpbSPc7a6uX4HCsdBjoF3hpUPxJARW/hMyzXpIy+7j0HNFxFU8fQUz85JI0S7iKSj9N3aso7GtYA7Y0XOkNh3Jw+m6Wgv5GqpjDd+5nfnoAn5maU/oW4F1TGMF+5RJYPRLNHM99RXhu3J1IuGO7Y+DpYI1O3IfnyiwK1Z5R2QIHgnSL1TWIQ==</Modulus><Exponent>AQAB</Exponent><P>DUeQTQt/lEXYarOwOWQHrkFyiAmxbueLoof8hbb70XDTIAg1KDqRW3q1RH/AgvxkZOdDjVIfJPz1Pys/aDyv0cNPHm3a93z8HHfqwCSu+0V8bGiMk7qT/UywNlO6NRraFt01pEhKyMTJDNxmBPnghWYErnow272bYiFQRaGG+wXEbLvamTuOlYJATTfd4otx7g87/xBPSHsT+XI1Quu6NQY5lxoqSvcl8liamwPwuu0sHiYKkatHbByYm2vLGkm9o+qJpE/WH9iENRV44oJep9rzanUHWQaQzF8Y+4kZRoJNpB6dZXtDNQ==</P><Q>C4z6BcGOsG431boGJyAWENv62Ut15/IcilSqU+fwONt05mtbsOK4mOj2aHcpBK5ovuyJLZhritWSNs58EfqSuaTRmhPhjkHka4EOurQLazghL7UHvF9YPsf4drhKDDi4aTAIBQZklRIbt/w/laTAZIOwrvzKUX86SSSV/DKw/At4uOV2fSqZWOc2z7RUdL9LdfHWLA597rhciUUYNuPcUh84fh2OYdYa9gMK+8dknYyGE/NqpuNvWRoxrpiDCJJ1qVKnTYf4dK4jnX4kccW6cpwzLsxYccf/VpmzCiAG8hRs0PiTUJJYvQ==</Q><DP>DSZdhX2qYyha6yZujhY46NyUTpPSqTEOe+7PJ5YQfNCNsH2QLfr5L64uS9t5xfuqaqT7pri4cIcxT9FCo7TcogoWkdpU55hTitlQk95/jAC9+hf5hNxQaZcEM3frv2SMq/F3ieuwamk7ccnaGHlcVJy2bBRBx4dVk/HLLHiMUozeepH3IJ/yTKjDhfOZfBl7wzJAxg3fhLWkDPKEPXVyWcmFQ9S8JqngYMBOsMxRGLl9Y4lmE+OZATy7/cPbRnfZG7VHgXcxYS2llW9i2mIg23Ucb8HdedHflxFgisyzQ7gKrEdIs1JBNQ==</DP><DQ>Blovnj7GhEz5M/csLxQ8TmVMXXhcjYTfIUxSp/ZkbEB018uvD2aAcseby/PMgSCRxn47KvIqIp413SJNpW2Sh7oW849/ekbOojjFqvQdW4fw6FW8vWIKG+zJvjC3uGTxNhgOMQVFB55/0/1L0rZagGzUHEg11AAOQ+909M/D+bktWTIIqellkDkIMc5QrCwQCftH9864fzKw4WU4XeeOYTN+VA1ZIoEnLqiZv8H2mZaQniQ+QS3PLMd1DzocxfgV8VrdW4wtna2vNIwM6uz9Ds7Mby4+bcS5rhy1FBps93E4Luylx6fUWQ==</DQ><InverseQ>APgTDSnh26MphXRPmWG/PJRQsZlJQ6CJT4u7zAA2MjXG69OoE4vy6BmZKWyorbFoivjcI2gMm2RM1VzetNxBzy7P33NwgFctLgGFdq/F8CJLRfmUdThYEksLSoDKHD/WLHWTkmWbggJVpuRGbOip40BpWdfvxdtskwHwYqGH8+ajGr8qocwsomKgKNgz08mTQ6TgkXjLMV9OvZYdMQO/SrIwQx1lGFtYfzdRUFSB7DK9ijUUvc31a/z4RSPK+TgTbX1v4QUwM7pd6uQKqqdc7TRpQewBWxHURcl+/Co1aoZliOeVhZYC+A==</InverseQ><D>lF6zvOeakf1Q4dQGOp2BhCovUhfnXGdMzivjB9nLeCjzTtgPMaGmKGUz/y1Wm//k3e/WgCmdM5vXCiWaYREi4yJbYpddBWWMklRH6vL/F5IrItuvhxJEbkdWYrgrsR0Fk06R+LKejndqQJ9euAN4YXIqLvp3QF+4FdlHcwx9bKMiu6DR0GKSigP/c3/RMna+2/HrS7HuwTHWexAjeKvMAwakj6NGpDfppu3JrQNsj/XbDt/WnDt9JW8mlSxtdzUw4/OiRclidN0MSREQba9RDBGHTuydoJp6HnxEwsJzJD+04tW7N5FyKApxpbnFRqqJ60n/GWcyoWYrHZeMMwDtyXcb1lbAUZxO0Wj2glgx9OLA1FJbEaqpuBq94t2V56b1Q0LKwWr6VV5NZ21kJ8lLaiF6+dJzZUyHofyvfBhJv2z5ROaU3x5jph/VtSMi5E3F7D2DLylqABTVtEo07ZXDqXV+gmKLS2xvyDI47Hojt2I7wqF7gkJFsxPOz+y76HUfqLcCyS6eMc50+j2jU5IkmD0EFgh6iaPf6gmKKC/oKElnq241hYNjj4SA+Ubi8FL6oB/WPjjA+QVLFAwuqbr8kxK8LBnG2IOMiEzSFr7oMQ==</D></RSAKeyValue>";


        public static TrialStatus updateTrial()
        {
            TrialStatus result = TrialStatus.LOCKED;
            try
            {
                object initSetting = Trial.getSetting("InitDate");
                object lastSetting = Trial.getSetting("LastOpen");
                object lockSetting = Trial.getSetting("Status"); //-1 -->locked | 0 --> Trial | 1 -->OK 
                DateTime initdate = new DateTime();
                DateTime lastdate = new DateTime();
                if (initSetting != null)
                {
                    initdate = new DateTime(long.Parse(string.IsNullOrEmpty(initSetting.ToString()) ? "1" : initSetting.ToString()));
                    //DateTime.TryParse(initSetting.ToString(),out initdate);
                }

                if (lastSetting != null)
                {
                    lastdate = new DateTime(long.Parse(string.IsNullOrEmpty(initSetting.ToString()) ? "1" : lastSetting.ToString()));
                    // DateTime.TryParse(lastSetting.ToString(),out lastdate);
                }

                if (Convert.ToInt32(lockSetting) == 1)
                {
                    result = TrialStatus.LOCKED;
                }
                else if (initSetting == null || string.IsNullOrEmpty(initSetting.ToString()))
                {
                    Trial.setSetting("InitDate", DateTime.Now.Ticks.ToString());
                    Trial.setSetting("LastOpen", DateTime.Now.Ticks.ToString());
                    result = TrialStatus.TRIAL;
                }
                else if (lastdate.CompareTo(DateTime.Now) == 1)
                {
                    Trial.setSetting("Locked", "-1");
                    result = TrialStatus.LOCKED;
                }
                else if ((DateTime.Now).CompareTo(initdate.AddDays(31)) == -1)
                {
                    result = TrialStatus.TRIAL;
                    Trial.setSetting("LastOpen", DateTime.Now.Ticks.ToString());
                }
                else if ((DateTime.Now).CompareTo(initdate.AddDays(31)) == 1)
                {
                    result = TrialStatus.EXPIRED;
                    Trial.setSetting("LastOpen", DateTime.Now.Ticks.ToString());
                }
                return result;
            }
            catch (Exception)
            {
               
                return result;
            }
        }

        public static int getTrialDays()
        {

            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        object initdateSetting = Trial.getSetting("initDate");
                        DateTime initDate = new DateTime();
                        if (initdateSetting != null)
                        {
                            initDate = new DateTime(long.Parse(string.IsNullOrEmpty(initdateSetting.ToString()) ? "1" : initdateSetting.ToString()));
                            //DateTime.TryParse(initSetting.ToString(),out initdate);
                        }


                        if (initDate == DateTime.MinValue)
                        {
                            return 30;
                        }
                        else
                        {
                            TimeSpan days = DateTime.Now.Subtract(initDate);
                            if (days.Days <= 30)
                            {
                                return 30 - days.Days;
                            }
                            else
                            {
                                return 30;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static void updateLicense(string owner, string clientId)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    SqlCeTransaction tsx = connection.BeginTransaction();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.Transaction = tsx;
                        try
                        {
                            Trial.setSetting("owner", owner);
                            Trial.setSetting("Status", "1");
                            Trial.setSetting("ClientID", clientId);
                            tsx.Commit();
                        }
                        catch (Exception ex)
                        {
                            tsx.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static bool isTrial()
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {

                        object clientSetting = Trial.getSetting("Status").ToString();
                        if (clientSetting == null)
                        {
                            //TODO::Edo prepei na kleinei h efarmogh kai na enhmeronei oti to sygkekrimeno antigrafo einai invalid
                            return true;
                        }
                        else
                        {
                            if (clientSetting.ToString().Equals("0"))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool isFirstTime()
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {

                        command.CommandText = @"
                           select value from settings
                            where name='InitDate'";
                        command.Parameters.Clear();

                        using (SqlCeDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["value"] == DBNull.Value)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        return false;

                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static object getSetting(string settingKey)
        {
            object setting = null;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"select value from settings where name=@name";
                        command.Parameters.AddWithValue("@name", settingKey);

                        setting = command.ExecuteScalar();
                    }
                }
                return setting;
            }
            catch (Exception ex)
            {
                return setting;
            }
        }

        public static object setSetting(string settingKey, string settingValue)
        {
            object setting = null;
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(Program.connectionString))
                {
                    connection.Open();
                    using (SqlCeCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"update settings set value=@value where name=@name";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@name", settingKey);
                        command.Parameters.AddWithValue("@value", settingValue);
                        command.ExecuteNonQuery();
                    }
                }
                return setting;

            }
            catch (Exception ex)
            {
              
                return setting;
            }
        }

    }
}
