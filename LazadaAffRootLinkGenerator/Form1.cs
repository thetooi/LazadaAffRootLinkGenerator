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
        private static readonly string API_KEY = Properties.Settings.Default.TOKEN_TOUICC;
        private static readonly string API_URL = "https://toui.cc/api/url";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSetToken_Click(object sender, EventArgs e)
        {
            SetTokenBitly setTokenBitly = new SetTokenBitly();

            _ = setTokenBitly.ShowDialog();
        }

        private static string Shorten(string longUrl)
        {
            string res;
            if (string.IsNullOrEmpty(Properties.Settings.Default.TOKEN_TOUICC) != true)
            {
                RestClient client = new RestClient(API_URL);
                RestRequest request = new RestRequest("add");
                _ = request.AddHeader("Authorization", $"Bearer {API_KEY}");
                _ = request.AddHeader("Content-Type", "application/json");
                Dictionary<string, string> param = new Dictionary<string, string> { { "url", longUrl } };
                _ = request.AddJsonBody(param);
                IRestResponse response = client.Post(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = response.Content;
                    JObject d = JObject.Parse(content);
                    string result = (string)d["shorturl"];
                    res = result;
                }
                else
                {
                    res = "Error: couldn't process the response from toui.cc";
                }
            }
            else
            {
                res = "Error: your toui.cc access token was not found";
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = System.Diagnostics.Process.Start("https://lin.ee/fvFN9m0");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ROOTLINK) != true)
            {
                textBoxROOTLINK.Text = Properties.Settings.Default.ROOTLINK;
                _ = textBoxPRODLINK.Focus();
            }
            else
            {
                _ = textBoxROOTLINK.Focus();
            }
        }
    }
}
