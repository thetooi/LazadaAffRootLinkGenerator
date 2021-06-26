using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazadaAffRootLinkGenerator
{
    public partial class SetTokenBitly : Form
    {
        public SetTokenBitly()
        {
            InitializeComponent();
            textBoxTOKEN.Text = Properties.Settings.Default.TOKEN_BITLY;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTOKEN.Text) != true && textBoxTOKEN.Text.Length == 40)
            {
                Properties.Settings.Default.TOKEN_BITLY = textBoxTOKEN.Text;
                label2.Visible = true;
            }
        }

        private void textBoxTOKEN_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTOKEN.Text.Length == 40)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            label2.Visible = false;
        }
    }
}
