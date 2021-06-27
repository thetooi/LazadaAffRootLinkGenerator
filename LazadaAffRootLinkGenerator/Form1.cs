using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace LazadaAffRootLinkGenerator
{
    public partial class Form1 : Form
    {
        private static readonly string API_KEY = Properties.Settings.Default.TOKEN_BITLY;
        private static readonly string API_URL = "https://api-ssl.bit.ly/v4";
        public Form1()
        {
            InitializeComponent();
            _ = textBoxPRODLINK.Focus();
            textBoxROOTLINK.Text = Properties.Settings.Default.ROOTLINK;
        }

        private void btnSetToken_Click(object sender, EventArgs e)
        {
            SetTokenBitly setTokenBitly = new SetTokenBitly();

            _ = setTokenBitly.ShowDialog();
        }

        private static string Shorten(string longUrl)
        {
            string res;
            if (string.IsNullOrEmpty(Properties.Settings.Default.TOKEN_BITLY) != true)
            {
                RestClient client = new RestClient(API_URL);
                RestRequest request = new RestRequest("shorten");
                _ = request.AddHeader("Authorization", $"Bearer {API_KEY}");
                Dictionary<string, string> param = new Dictionary<string, string> { { "long_url", longUrl } };
                _ = request.AddJsonBody(param);
                IRestResponse response = client.Post(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = response.Content;
                    JObject d = JObject.Parse(content);
                    string result = (string)d["id"];
                    res = "https://" + result;
                }
                else
                {
                    res = "Error: couldn't process the response from bit.ly";
                }
            }
            else
            {
                res = "Error: your bit.ly access token was not found";
            }
            
            return res;
        }

        private void GenLink()
        {
            if (string.IsNullOrEmpty(textBoxROOTLINK.Text) != true && string.IsNullOrEmpty(textBoxPRODLINK.Text) != true)
            {
                try
                {
                    string data = textBoxROOTLINK.Text
                        + "?url=" + Uri.EscapeDataString(textBoxPRODLINK.Text);
                    if (string.IsNullOrEmpty(textBoxSUBID.Text) != true)
                    {
                        data += "&sub_aff_id=" + Uri.EscapeDataString(textBoxSUBID.Text);
                    }
                    textBoxAFFLINK.Text = data;
                    textBoxBITLY.Text = Shorten(textBoxAFFLINK.Text);
                }
                catch (Exception e)
                {
                    _ = MessageBox.Show(e.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenLink();
        }

        private void textBoxAFFLINK_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxAFFLINK.Focus();
            textBoxAFFLINK.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBITLY.Text) != true)
            {
                Clipboard.SetText(textBoxBITLY.Text);
                textBoxBITLY.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAFFLINK.Text) != true)
            {
                Clipboard.SetText(textBoxAFFLINK.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxAFFLINK.Clear();
            textBoxBITLY.Clear();
            textBoxPRODLINK.Clear();
            // textBoxROOTLINK.Clear();
            _ = textBoxPRODLINK.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ROOTLINK = textBoxROOTLINK.Text;
            Properties.Settings.Default.Save();
        }
    }
}
