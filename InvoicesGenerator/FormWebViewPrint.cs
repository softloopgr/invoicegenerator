using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator.Classes;
using System.Drawing.Printing;

namespace InvoicesGenerator
{
    public partial class FormWebViewPrint : Form
    {
        private int _invoiceId;
        public string SavedLocation;
        private SealDetails _seal;

        //einai gia tin sugentrotikh, pou den xreiazomaste kati
        public FormWebViewPrint()
        {
            InitializeComponent();
            _seal = InvoiceDAL.getSeal();
            WebBrowser.DocumentText = Report.GetCentralizedReport();
        }

        public FormWebViewPrint(string invoiceTypeName, int startingNumber, int type, int pageNumber)
        {
            InitializeComponent();
            HideControls();
            _seal = InvoiceDAL.getSeal();
            if (_seal.id.HasValue)
            {
                WebBrowser.DocumentText = Report.GetBlankInvoice(_seal.id.Value, invoiceTypeName, startingNumber, type, pageNumber);
            }
        }

        private void HideControls()
        {
            ButtonSaveToDoc.Visible = false;
            tsSeperator.Visible = false;
        }

        public FormWebViewPrint(int invoiceId, bool isPreprint)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            _seal = InvoiceDAL.getSeal();
            if (_seal.id.HasValue)
            {
                WebBrowser.DocumentText = Report.GetFullReport(_seal.id.Value, invoiceId, isPreprint);
            }
        }

        private void TsBtnPrintClick(object sender, EventArgs e)
        {
            WebBrowser.ShowPrintPreviewDialog();
        }

        public void Print()
        {
            WebBrowser.Print();
        }

        private static string TEMP_DOC_SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\InvoicesGenerator";
        private static string DEFAULT_DOC_NAME = "invoice_generator.doc";
        public void SaveToDoc(int sealId, int invoiceId, string fileName, bool isFromMain = false, bool save_for_pdf = false)
        {
            tsLblMessage.Visible = false;
            string documentText;
            documentText = isFromMain ? Report.GetFullReport(sealId, invoiceId, false) : WebBrowser.DocumentText;

            string tempSaveMessage = string.Empty;

            try
            {
                string path = string.Empty;

                // if we save for pdf then the word is saved in AppData temporarily
                if (save_for_pdf)
                {
                    path = TEMP_DOC_SAVE_PATH + "\\" + DEFAULT_DOC_NAME;
                }
                else
                {
                    path = fileName;
                }

                SavedLocation = tempSaveMessage = Path.GetFileName(fileName); //to vazoume sto SavedLocation epeidh einai na paei gia email
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(documentText);
                    fs.Write(info, 0, info.Length);
                }

                if (!save_for_pdf)
                {
                    tsLblMessage.Text = "Το αρχείο αποθηκεύτηκε στο: " + path + ".";
                }
            }
            catch
            {
                General.ErrorMessage("Υπήρξε πρόβλημα με το άνοιγμα του αρχείου. Εαν το έχετε ανοιχτό εκτός προγράμματος κλείστε το και δοκιμάστε ξανά.");
            }

            tsLblMessage.Visible = true;

            if (isFromMain)
            {
                SavedLocation = tsLblMessage.Text;
            }
        }

        private void TsBtnSaveToDiskClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Microsoft Word 97 - 2003 Document|*.doc";
            dialog.FileName = _seal.id + ". " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + _seal.Name.ToUpper();
            dialog.ShowDialog();
            string path = Path.GetDirectoryName(dialog.FileName);
            if (!string.IsNullOrEmpty(path))
            {
                string fileName = dialog.FileName;
                SaveToDoc(_seal.id.Value, _invoiceId, fileName, false);
            }
            else
            {
                tsLblMessage.Text = "Δεν επιλέξατε που θα αποθηκεύσετε το αρχείο.";
            }
        }

        private void ButtonSendEmailClick(object sender, EventArgs e)
        {
            //todo: na psaxnei h oxi?
            //DialogResult questionResult = General.QuestionMessage("Υπάρχει παραστατικό προς αποστολή;");
            //if(questionResult == DialogResult.Yes) //an thelei mporei na psaksei kapoio etoimo arxeio
            //{
            //    OpenFileDialog fileDialog = new OpenFileDialog();
            //    DialogResult result = fileDialog.ShowDialog();
            //    if(result == DialogResult.OK)
            //    {
            //        SavedLocation = fileDialog.FileName;
            //    }
            //    else
            //    {
            //        General.InformationMessage("Για να ξεκινήσει η διαδικασία αποστολής, παρακαλώ επιλέξτε κάποιο αρχείο.");
            //        return;
            //    }
            //}
            //else if (questionResult == DialogResult.No) //allios sozei to invoice pou exei anoigmeno kai stelnei afto
            //{
            ButtonSaveToDoc.PerformClick();
            //}

            using (FormShare frm = new FormShare(_seal.Name, SavedLocation))
            {
                frm.ShowDialog();
            }
        }
    }
}
