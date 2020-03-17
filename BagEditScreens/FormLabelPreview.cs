using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace Bagging.BagEdit {
    public partial class FormLabelPreview : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormBagEdit));
        private readonly Image image;
        public ushort Copies { get; private set; } = 0;
        public FormLabelPreview(Bitmap image) {
            this.image = image;
            InitializeComponent();
            log.Debug("FormLabelPreview initialized.");
            Copies = ushort.Parse(this.numberOfCopies.Value.ToString());
        }

        private void LabelPreview_Load(object sender, EventArgs e) {
            pictureBox1.Image = new Bitmap(image, (int)Math.Ceiling(image.Width * .6) , (int)Math.Ceiling(image.Height * .6));
            
        }

        private void OK_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void NumberOfCopies_ValueChanged(object sender, EventArgs e) {
            Copies = ushort.Parse(this.numberOfCopies.Value.ToString());
        }

        private void NumberOfCopies_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = true;
        }
    }
}
