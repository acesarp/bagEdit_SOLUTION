using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bagging.BagEdit {
    public partial class FormWait : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormWait));
        Form parent = null;
        public FormWait(Form parent) {
            InitializeComponent();
            if (parent != null) {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(parent.Location.X + parent.Width / 2 - this.Width / 2, parent.Location.Y + parent.Height / 2 - this.Height / 2);
            } else {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            this.parent = parent;
        }
        public void CloseLoadingForm() {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
            if (imageLabel.Image != null) {
                imageLabel.Image.Dispose();
            }
        }

        private void WaitForm_Load(object sender, EventArgs e) {

        }

        private void WaitForm_Shown(object sender, EventArgs e) {
            this.TopLevel = true;
            this.TopMost = true;

        }
    }


    public class WaitFormWrapper {
        public FormWait thisForm;
        private Thread thread;


        public void Show(Form parent) {
            thread = new Thread(new ParameterizedThreadStart(LoadingProcessEx));
            thread.Start(parent);
        }
        public void Close() {
            if (thisForm != null) {
                thisForm.BeginInvoke(new ThreadStart(thisForm.CloseLoadingForm));
            }
        }

        private void LoadingProcessEx(object parent) {
            Form Cparent = parent as Form;
            thisForm = new FormWait(Cparent);
            thisForm.ShowDialog();
        }

    }
    
}

