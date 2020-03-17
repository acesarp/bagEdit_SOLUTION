using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bagging.BagEdit.CustomControls {

    /// <summary>
    /// Summary description for MyDataGridView.
    /// </summary>
    [ToolboxItem(true)]
    public partial class MyDataGridView : DataGridView {

        public MyDataGridView() : base() {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (msg.WParam.ToInt32() == (int)Keys.Enter) {
                    SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}