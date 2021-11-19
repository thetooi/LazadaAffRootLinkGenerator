using System;
using System.Windows.Forms;

namespace LazadaAffRootLinkGenerator
{
    public partial class SetTokenBitly : Form
    {
        public SetTokenBitly()
        {
            InitializeComponent();
            textBoxTOKEN.Text = Properties.Settings.Default.TOKEN_TOUICC;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTOKEN.Text) != true
                && textBoxTOKEN.Text.Length == 12)
            {
                Properties.Settings.Default.TOKEN_TOUICC = textBoxTOKEN.Text;
                label2.Visible = true;
            }
        }

        private void textBoxTOKEN_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBoxTOKEN.Text.Length == 12;
            label2.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://toui.cc/developers");
        }
    }
}
