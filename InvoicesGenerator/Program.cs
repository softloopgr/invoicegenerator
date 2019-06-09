using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using InvoicesGenerator.Classes;
using InvoicesGenerator.Properties;
using InvoicesGenerator;

namespace InvoicesGenerator
{
    static class Program
    {
        public static string connectionString;
        public static string appFullName = Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
        public static string APP_PATH = Path.GetDirectoryName(appFullName) + @"\";
        public static bool postUpdateCompleted = false;
        public static User user;
        public static SealDetails seal;
        [STAThread]
        static void Main()
        {
            General.dropInternetExplorerPrintHeaderFooter();
            Settings.Default.Upgrade();

            SimpleAES aes = new SimpleAES();
            string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\" + aes.DecryptString(Settings.Default.db_dataSource);
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\InvoicesGenerator";

            string fileName = Path.GetFileName(sourcePath);

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            if (!File.Exists(targetPath + @"\" + fileName))
            {
                File.Copy(sourcePath, targetPath + @"\" + fileName);
            }

            connectionString = "Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\InvoicesGenerator\" + fileName + ";" +
                               "Persist Security Info=True;" +
                               "Password=" + aes.DecryptString(Settings.Default.db_pass) + "; ";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            user = new User();

            Application.Run(new FormStart());


           //// SoftloopTools.explicitDatabaseUpdateOnVersion1001();
           // seal = InvoiceDAL.getSeal();

           // if (Trial.isTrial())
           // {
           //     using (FormRegister register = new FormRegister())
           //     {
           //         if (register.ShowDialog() == DialogResult.Cancel)
           //         {
           //             Application.Exit();
           //             return;
           //         }
           //     }
           // }
           // if (seal == null || seal.id == null || seal.id < 0)
           // {
           //     using (FormSeal frm = new FormSeal())
           //     {
           //         DialogResult result = frm.ShowDialog();
           //         if (result == DialogResult.Cancel)
           //         {
           //             Application.Exit();
           //             return;
           //         }
           //     }
           // }
           // using (FormLogin frm = new FormLogin())
           // {
           //     if (frm.ShowDialog() == DialogResult.Cancel)
           //     {
           //         Application.Exit();
           //     }
           //     else
           //     {
           //         Application.Run(new FormMain());
           //     }
           // }
        }
    }
}
