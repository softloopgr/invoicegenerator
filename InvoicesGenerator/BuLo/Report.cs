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
using InvoicesGenerator.Classes;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml;
using Microsoft.Win32;
using System.Diagnostics;

namespace InvoicesGenerator
{
    public static class Report
    {
        private static int MAX_MAJOR_DLL_VERSION = 14;
        private static string TEMP_DOC_SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\InvoicesGenerator";
        private static string DEFAULT_PDF_NAME = "invoice_generator.pdf";
        private static string DEFAULT_DOC_NAME = "invoice_generator.doc";
        private static string _newPdfFilePath;

        public static string GetCentralizedReport()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\centralizedTemplate.tmpl";
            List<KeyValuePair<string, object>> templateData = new List<KeyValuePair<string, object>>();
            StringBuilder centralizedReport = new StringBuilder();
            ReportDAL.GetCentralized(templateData);

            centralizedReport.Append(FillReport(path, templateData, true));

            return centralizedReport.ToString();
        }

        /// <param name="type">monotupo, diplotupo, triplotupo</param>
        public static string GetBlankInvoice(int sealId, string invoiceTypeName, int startingNumber, int type, int pageNumber)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\emptyInvoiceTemplate.tmpl";
            List<KeyValuePair<string, object>> templateData = new List<KeyValuePair<string, object>>();
            templateData.Add(new KeyValuePair<string, object>("invoice_invoiceTypeName", invoiceTypeName));
            StringBuilder fullReport = new StringBuilder();
            ReportDAL.GetSealDetails(sealId, templateData);

            try
            {
                for (int i = 1; i <= type; i++)
                {
                    for (int j = startingNumber; j <= pageNumber; j++)
                    {
                        templateData.Add(new KeyValuePair<string, object>("invoice_currentPrintNumber", j.ToString()));
                        fullReport.Append(FillReport(path, templateData, true));
                        templateData.RemoveAt(templateData.Count - 1);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return fullReport.ToString();
        }

        public static string GetFullReport(int sealId, int invoiceId, bool isPrePrint, string edeiksh = "")
        {
            List<KeyValuePair<string, object>> templateData = new List<KeyValuePair<string, object>>();
            StringBuilder fullReport = new StringBuilder();

            ReportDAL.GetSealDetails(sealId, templateData);
            ReportDAL.GetCustomer(invoiceId, templateData);
            ReportDAL.GetInvoiceDetails(invoiceId, templateData, edeiksh);
            ReportDAL.GetInvoiceItems(invoiceId, templateData);
            string path = string.Empty;

            if (!isPrePrint)
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\reportTemplate.tmpl";
            }
            else
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\fillEmptyInvoiceTemplate.tmpl";
            }
            fullReport.Append(FillReport(path, templateData, true));

            return fullReport.ToString();
        }

        private static string FillReport(string path, List<KeyValuePair<string, object>> templateData, bool isFirst)
        {
            string report = File.ReadAllText(path);

            try
            {
                foreach (KeyValuePair<string, object> data in templateData)
                {
                    if (data.Value.GetType() == typeof(List<KeyValuePair<string, object>>))
                    {
                        //[0] = key gia na psaksoume me regex, [1] = name tou template pou theloume na anoiksei
                        string regexKey = data.Key.Split(':')[0];
                        string fileName = data.Key.Split(':')[1];
                        string itemPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\" + fileName + ".tmpl";
                        string tempreport = FillReport(itemPath, (List<KeyValuePair<string, object>>)data.Value, false);
                        Regex regex = new Regex(@"{" + regexKey + "}", RegexOptions.IgnoreCase);
                        report = regex.Replace(report, tempreport);
                    }
                    else
                    {
                        Regex regex = new Regex(@"{" + data.Key + "}", RegexOptions.IgnoreCase);
                        report = regex.Replace(report, data.Value.ToString());
                    }
                }

                //ean den einai recursion
                if (isFirst)
                {
                    //vazoume tin eikona ean uparxei kai meta...
                    if (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.CompanyLogo))
                    {
                        Regex imageRegex = new Regex(@"{image_src}", RegexOptions.IgnoreCase);
                        report = imageRegex.Replace(report, InvoicesGenerator.Properties.Settings.Default.CompanyLogo);
                    }

                    //...kanoume clear ota ta empty {} tags
                    MatchCollection emptyCases = Regex.Matches(report, @"{.+?}");
                    foreach (var emptyCase in emptyCases)
                    {
                        Regex regex = new Regex(@"" + emptyCase + "", RegexOptions.IgnoreCase);
                        report = regex.Replace(report, string.Empty);
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            List<KeyValuePair<string, string>> reportStyles = new List<KeyValuePair<string, string>>();
            reportStyles.Add(new KeyValuePair<string, string>("imageVisible", (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.CompanyLogo) ? "block" : "none")));
            report = StylingReport(report, reportStyles);
            return report;
        }

        private static string StylingReport(string report, List<KeyValuePair<string, string>> reportStyles)
        {
            string result = report;

            foreach (KeyValuePair<string, string> style in reportStyles)
            {
                Regex regex = new Regex(@"~" + style.Key + "~", RegexOptions.IgnoreCase);
                result = regex.Replace(result, style.Value);
            }
            return result;
        }

        #region New Word Method

        private static Drawing addImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 19050L,
                             TopEdge = 0L,
                             RightEdge = 9525L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1",
                             Description = "tttt"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                         )
                                         {
                                             Embed = relationshipId
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         ) { Preset = A.ShapeTypeValues.Rectangle }))
                             ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U
                     });

            return element;
        }

        public static void GenerateImagePart(OpenXmlPart part, string imageFilePath, ref long imageWidthEMU, ref long imageHeightEMU)
        {
            byte[] imageFileBytes;
            Bitmap imageFile;

            // Open a stream on the image file and read it's contents.
            using (FileStream fsImageFile = File.OpenRead(imageFilePath))
            {
                imageFileBytes = new byte[fsImageFile.Length];
                fsImageFile.Read(imageFileBytes, 0, imageFileBytes.Length);

                imageFile = new Bitmap(fsImageFile);
            }

            // Get the dimensions of the image in English Metric Units (EMU)
            // for use when adding the markup for the image to the document.
            imageWidthEMU =
              (long)(
              (imageFile.Width / imageFile.HorizontalResolution) * 914400L);
            imageHeightEMU =
              (long)(
              (imageFile.Height / imageFile.VerticalResolution) * 914400L);

            // Write the contents of the image to the ImagePart.
            using (BinaryWriter writer = new BinaryWriter(part.GetStream()))
            {
                writer.Write(imageFileBytes);
                writer.Flush();
            }
        }

        private static Drawing AddParts(WordprocessingDocument parent, string imagepath)
        {
            long imageWidthEMU = 0;
            long imageHeightEMU = 0;

            var imagePart = parent.MainDocumentPart.AddNewPart<ImagePart>("image/jpeg", null);

            GenerateImagePart(imagePart, imagepath,
              ref imageWidthEMU, ref imageHeightEMU);

            return addImageToBody(parent, parent.MainDocumentPart.GetIdOfPart(imagePart));
        }

        public static bool SaveToWord(SealDetails seal, Invoice invoice, string fileName, bool save_for_pdf = false, string edeiksh = "")
        {
            try
            {
                // if we save for pdf then the word is saved in AppData temporarily
                if (save_for_pdf)
                {
                    fileName = TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_DOC_NAME;
                }

                // copy file to destination
                string filePath = string.Empty;
                if (invoice.InvoiceOrder != null)
                {
                    // ean einai deltio apostolhs
                    if (invoice.Invoice_type.Value == InvoiceDAL.getInvoiceTypeIdByLabel("deltio"))
                    {
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\softloopInvoiceTemplateWithOrderDeltio.docx";
                    }
                    else
                    {
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\softloopInvoiceTemplateWithOrder.docx";
                    }
                }
                else
                {
                    // ean einai deltio apostolhs
                    if (invoice.Invoice_type.Value == InvoiceDAL.getInvoiceTypeIdByLabel("deltio"))
                    {
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\softloopInvoiceTemplateDeltio.docx";
                    }
                    else
                    {
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Resources\\Templates\\softloopInvoiceTemplate.docx";
                    }
                }
                File.Copy(filePath, fileName, true);

                // get invoice data
                List<KeyValuePair<string, object>> templateData = new List<KeyValuePair<string, object>>();
                ReportDAL.GetSealDetails(seal.id.Value, templateData);
                ReportDAL.GetCustomer(invoice.Id.Value, templateData);
                ReportDAL.GetInvoiceDetails(invoice.Id.Value, templateData, edeiksh);
                ReportDAL.GetInvoiceItems(invoice.Id.Value, templateData);

                // fill the word template with data
                SearchAndReplace(fileName, templateData);

                return true;
            }
            catch (Exception ex)
            {
                SoftloopTools.ExceptionLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
                return false;
            }
        }

        private static void SearchAndReplace(string document, List<KeyValuePair<string, object>> templateData)
        {
            // vazoume thn eikona
            using (var wordDocument = WordprocessingDocument.Open(document, true))
            {
                var paragraphs = wordDocument.MainDocumentPart.Document.Body.Descendants<Paragraph>();
                foreach (Paragraph p in paragraphs)
                {
                    if (p.InnerText.Equals("{{0}}"))
                    {
                        try
                        {
                            Drawing element = AddParts(wordDocument, InvoicesGenerator.Properties.Settings.Default.CompanyLogo);
                            Run run = p.AppendChild(new Run());
                            run.AppendChild(element);
                        }
                        catch (Exception ex)
                        {
                            SoftloopTools.ExceptionLog(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
                        }
                    }
                }
            }

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                // vazoume osa uparxoun sto word
                foreach (KeyValuePair<string, object> data in templateData)
                {
                    docText = docText.Replace(data.Key, data.Value.ToString());
                }

                // clear empty tags
                MatchCollection emptyCases = Regex.Matches(docText, @"{{.+?}}");
                foreach (var emptyCase in emptyCases)
                {
                    docText = docText.Replace(emptyCase.ToString(), string.Empty);
                }

                docText = docText.Replace("<br/>", "<w:br/>");

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
        }

        #endregion

        #region Pdf Methods

        public static void SaveToPdf(int sealId, int invoiceId, string fileName, bool is_print = false)
        {
            string path = Path.GetDirectoryName(fileName);
            string filename_to_save_as = Path.GetFileName(fileName);

            // ελέγχουμε ποιά έκδοση office τρέχει
            string componentPath = GetComponentPath();
            int majorVersion = GetMajorVersion(componentPath);

            // ελέγχουμε εαν υπάρχει έκδοση μεγαλύτερη από τα dll που έχουμε και φέρνουμε το μεγαλύτερο dll που έχουμε
            if (majorVersion > MAX_MAJOR_DLL_VERSION)
            {
                majorVersion = MAX_MAJOR_DLL_VERSION;
            }

            // φορτώνουμε το κατάλληλο dll
            Assembly DLL = Assembly.LoadFile(Program.APP_PATH + @"Resources\\" + majorVersion + ".dll");
            Type type = DLL.GetTypes().Single(t => t.Name == "ApplicationClass");
            dynamic appClass = Activator.CreateInstance(type);
            // Create a new Microsoft Word application object
            var word = appClass.Application();
            // C# doesn't have optional arguments so we'll need a dummy value
            object oMissing = System.Reflection.Missing.Value;
            // Get list of Word files in specified directory
            DirectoryInfo dirInfo = new DirectoryInfo(TEMP_DOC_SAVE_PATH);
            FileInfo[] wordFiles = dirInfo.GetFiles("*.doc");
            word.Visible = false;
            word.ScreenUpdating = false;
            // Cast as Object for word Open method
            Object filename = (Object)wordFiles[0].FullName;
            object objDocuments = word.GetType().InvokeMember("Documents", BindingFlags.GetProperty, null, word, null);
            object[] param = new object[] {  filename,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing };
            object doc = objDocuments.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, objDocuments, param);
            doc.GetType().InvokeMember("Activate", BindingFlags.InvokeMethod, null, doc, null);
            // Use the dummy value as a placeholder for optional arguments
            DEFAULT_PDF_NAME = Guid.NewGuid() + ".pdf";
            object outputFileName = wordFiles[0].FullName.Replace(DEFAULT_DOC_NAME, DEFAULT_PDF_NAME);
            type = DLL.GetExportedTypes().Single(t => t.Name == "WdSaveFormat");
            dynamic wdSaveFormat = Activator.CreateInstance(type);
            object fileFormat = wdSaveFormat.GetType().InvokeMember("wdFormatPDF", BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, wdSaveFormat, null);
            // Save document into PDF Format
            object[] paramSave = new object[] {   outputFileName,
             fileFormat,  oMissing,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing,
             oMissing,  oMissing,  oMissing,  oMissing};
            doc.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, doc, paramSave);
            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.
            type = DLL.GetExportedTypes().Single(t => t.Name == "WdSaveOptions");
            dynamic wdSaveOptions = Activator.CreateInstance(type);
            object saveChanges = wdSaveOptions.GetType().InvokeMember("wdDoNotSaveChanges", BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, wdSaveOptions, null);
            object[] paramClose = new object[] { saveChanges, oMissing, oMissing };
            doc.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, doc, paramClose);
            doc = null;
            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            object[] paramQuit = new object[] { oMissing, oMissing, oMissing };
            word.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, word, paramQuit);
            // word.Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;

            // σβήνουμε το doc που φτιάχτηκε στο temp
            File.Delete(TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_DOC_NAME);

            if (is_print)
            {
                PrintPDFs(TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_PDF_NAME);
            }
            else
            {
                // μεταφέρουμε το pdf στον φάκελο που ορίσαμε
                _newPdfFilePath = path + "\\" + filename_to_save_as;
                File.Copy(TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_PDF_NAME, _newPdfFilePath, true);
            }

            // σβήνουμε το pdf που φτιάχτηκε στο temp
            File.Delete(TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_PDF_NAME);
        }

        public static Boolean PrintPDFs(string pdfFileName)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                if (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.adobe_path) && File.Exists(InvoicesGenerator.Properties.Settings.Default.adobe_path))
                {
                    //proc.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe";
                    proc.StartInfo.FileName = InvoicesGenerator.Properties.Settings.Default.adobe_path;
                    proc.StartInfo.Arguments = String.Format(@"/p /h {0}", pdfFileName);
                }
                else if (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.foxit_path) && File.Exists(InvoicesGenerator.Properties.Settings.Default.foxit_path))
                {
                    //proc.StartInfo.FileName = @"C:\Program Files (x86)\Foxit Software\Foxit Reader\Foxit Reader.exe";
                    proc.StartInfo.FileName = InvoicesGenerator.Properties.Settings.Default.foxit_path;
                    proc.StartInfo.Arguments = String.Format(@"-p {0}", pdfFileName);
                }
                else
                {
                    General.ErrorMessage("Δεν έχει οριστεί διαθέσιμος PDF Reader και η εκτύπωση δεν μπορεί να συνεχιστεί. Παρακαλούμε επικοινωνήστε άμεσα με τον διαχειριστή του προγράμματος.\nΤηλ. επικοινωνίας: 2130267712");
                    return false;
                }

                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();

                if (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.adobe_path) && File.Exists(InvoicesGenerator.Properties.Settings.Default.adobe_path))
                {
                    KillProcess("AcroRd32");
                }
                else if (!string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.foxit_path) && File.Exists(InvoicesGenerator.Properties.Settings.Default.foxit_path))
                {
                    KillProcess("Foxit Reader");
                    KillProcess("FoxitReader");
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //For whatever reason, sometimes adobe likes to be a stage 5 clinger.
        //So here we kill it with fire.
        private static bool KillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }

        private static string GetComponentPath()
        {
            const string regKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
            string toReturn = string.Empty;
            string key = "winword.exe";

            //looks inside CURRENT_USER:
            RegistryKey _mainKey = Registry.CurrentUser;
            try
            {
                _mainKey = _mainKey.OpenSubKey(regKey + "\\" + key, false);
                if (_mainKey != null)
                {
                    toReturn = _mainKey.GetValue(string.Empty).ToString();
                }
            }
            catch
            { }

            //if not found, looks inside LOCAL_MACHINE:
            _mainKey = Registry.LocalMachine;
            if (string.IsNullOrEmpty(toReturn))
            {
                try
                {
                    _mainKey = _mainKey.OpenSubKey(regKey + "\\" + key, false);
                    if (_mainKey != null)
                    {
                        toReturn = _mainKey.GetValue(string.Empty).ToString();
                    }
                }
                catch
                { }
            }

            //closing the handle:
            if (_mainKey != null)
                _mainKey.Close();

            return toReturn;
        }

        private static int GetMajorVersion(string _path)
        {
            int toReturn = 0;
            if (File.Exists(_path))
            {
                try
                {
                    FileVersionInfo _fileVersion = FileVersionInfo.GetVersionInfo(_path);
                    toReturn = _fileVersion.FileMajorPart;
                }
                catch
                { }
            }
            return toReturn;
        }

        #endregion

    }
}
