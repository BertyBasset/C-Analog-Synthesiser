using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI {
    public partial class TextInputBox : Form {
        public TextInputBox() {
            InitializeComponent();

            cmdCancel.Click += (o, e) => { rv = "";  this.Close(); };
            cmdOK.Click += (o, e) => {
                if (txtText.Text.Trim() == "") {
                    txtText.Focus();
                    MessageBox.Show("Please enter a Patch Name", "Save Patch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                    
                rv = txtText.Text.Trim();
                this.Close();   
            };
        }

        private void CmdCancel_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static string rv = "";
        public static string Show(string Title, string Label) {
            var f = new TextInputBox();
            f.lblLabel.Text = Label;
            f.Text = Title;

            f.ShowDialog();

            return rv;  
        }

    }
}
