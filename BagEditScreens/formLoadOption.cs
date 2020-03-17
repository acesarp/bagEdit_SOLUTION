using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bagging.BagEdit {

    public enum ScreenChoice {
        Edit = 0, Reseed = 1, None = 2

    }
    public partial class FormLoadOption : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormLoadOption));
        public ScreenChoice Choice { get; private set; }
        public FormLoadOption() {
            InitializeComponent();
            Choice = ScreenChoice.None;
            log.Debug("FormLoadOption initialized.");
        }

        private void EditButton_Click(object sender, EventArgs e) {
            Choice = ScreenChoice.Edit;
        }

        private void ReseedButton_Click(object sender, EventArgs e) {
            Choice = ScreenChoice.Reseed;
        }

    }
}
