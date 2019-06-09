using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.SqlServerCe;
using InvoicesGenerator.Properties;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using System.Transactions;
using System.Data;

namespace InvoicesGenerator
{
    public class SoftloopTools
    {
      
        #region Logging
        public static void ExceptionLog(Exception ex, string functionName, bool showMessage = true)
        {
            string logFolder = string.Empty;
            if (Settings.Default.path_logPath != string.Empty)
            {
                logFolder = Settings.Default.path_logPath;
            }
            else
            {
                logFolder = Program.APP_PATH + @"\Logs\";
            }
            if (!Directory.Exists(logFolder))
            {
                System.IO.Directory.CreateDirectory(logFolder);
            }

            string msg = "[ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " ] - " + functionName + " => " + ex.Message + "\r\n";
            File.AppendAllText(logFolder + "Error.log", msg, Encoding.UTF8);

            if (showMessage)
            {
                MessageBox.Show("Εμφανίστικε σφάλμα κατα την εκτέλεση της εφαρμογής. Αν το προβλημα επιμένει παρακαλώ ενημερώστε άμεσα τον διαχειριστή", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        public static void TraceLog(string message)
        {
            string logFolder = string.Empty;
            if (Settings.Default.path_logPath != string.Empty)
            {
                logFolder = Settings.Default.path_logPath;
            }
            else
            {
                logFolder = Program.APP_PATH + @"\Logs\";
            }
            if (!Directory.Exists(logFolder))
            {
                System.IO.Directory.CreateDirectory(logFolder);
            }

            string msg = "[ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " ] - " + message + "\r\n";
            File.AppendAllText(logFolder + "Trace.log", msg, Encoding.UTF8);
        }
        #endregion

        #region Updating
        public static void updateApplication()
        {
            string updater = Directory.GetCurrentDirectory() + "\\Updater.exe";
            if (!File.Exists(updater))
            {
                General.ErrorMessage("Δεν βρέθηκε ο Updater. Παρακαλώ ενημερώστε τον διαχειριστή");
                return;
            }

            string parameters =
                "\"" + Settings.Default.app_name + "\" " +
                "\"" + Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", "") + "\" " +
                "\"" + Process.GetCurrentProcess().MainModule.FileName + "\" " +
                "\"" + Settings.Default.app_client + "\" " +
                "\"" + "COMPACT" + "\" " +
                "\"" + Program.connectionString +"\"";
            Process.Start(updater, parameters);

            Application.Exit();
        }


        public static void explicitDatabaseUpdateOnVersion1001()
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
                            cmd.CommandText = "select 1 from Invoice_Types where name ='Απόδειξη Λιανικής'";
                            cmd.Parameters.Clear();
                            object result = cmd.ExecuteScalar();
                            if (result == null)
                            {
                                cmd.Parameters.Clear();
                                
                                cmd.CommandText = "alter table invoices_group drop CONSTRAINT fk_invoice_type";
                                cmd.ExecuteNonQuery();
                                
                                cmd.CommandText = "drop table invoice_types";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = @"CREATE TABLE [Invoice_Types] (
                                                                                  [id] int NOT NULL  IDENTITY (4,1)
                                                                                , [name] nvarchar(400) NOT NULL
                                                                                , [current] int NOT NULL
                                                                                , [is_PrePrint] int NULL
                                                                                )";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "ALTER TABLE [Invoice_Types] ADD CONSTRAINT [PK_Invoice_Types] PRIMARY KEY ([id])";
                                cmd.ExecuteNonQuery();
                                
                                cmd.CommandText = "SET IDENTITY_INSERT [Invoice_Types] ON";
                                cmd.ExecuteNonQuery();                           
                                
                                cmd.CommandText = "INSERT INTO [Invoice_Types] ([id],[name],[current],[is_PrePrint]) VALUES (1,N'Απόδειξη Παροχής Υπηρεσιών',2,null)";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT INTO [Invoice_Types] ([id],[name],[current],[is_PrePrint]) VALUES (2,N'Τιμολόγιο Παροχής Υπηρεσιών',1,null)";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT INTO [Invoice_Types] ([id],[name],[current],[is_PrePrint]) VALUES (3,N'Απόδειξη Λιανικής',1,1)";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "SET IDENTITY_INSERT [Invoice_Types] OFF";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "alter table invoices_group add CONSTRAINT fk_invoice_type FOREIGN KEY (invoice_type) REFERENCES Invoice_Types(id)";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
             
                }
                scope.Complete();
            }
        }

        #endregion

    }
}
