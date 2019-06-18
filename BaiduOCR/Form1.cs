using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace BaiduOCR
{
    public partial class Form1 : Form
    {

        Baidu.Aip.Ocr.Ocr client;
        public Form1()
        {
            InitializeComponent();
            //var result = AccessToken.getAccessToken();
            //client = new Baidu.Aip.Ocr.Ocr("xxxx", "xxxxxxx");
        }
        public void ReconBill()
        {
            string token = AccessToken.GetAccessToken();
            dynamic json = Newtonsoft.Json.Linq.JToken.Parse(token) as dynamic;
            string name = json.access_token;
            string host = "{0}?access_token=" + name;
            if (comboBox1.SelectedItem == null)
                host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v2/advanced_general");
            else
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "增值税发票识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/vat_invoice");
                        break;
                    case "通用物体和场景识别高级版":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v2/advanced_general");
                        break;
                    case "图像主体检测":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v1/object_detect");
                        break;
                    case "车型识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v1/car");
                        break;
                    case "动物识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v1/animal");
                        break;
                    case "植物识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v1/plant");
                        break;
                    case "货币识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v1/currency");
                        break;
                    case "通用票据识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/receipt");
                        break;
                    case "定额发票识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/quota_invoice");
                        break;
                    case "手写文字识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/handwriting");
                        break;
                    case "数字识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers");
                        break;
                    case "身份证识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard");
                        break;
                    case "银行卡识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard");
                        break;
                    case "火车票识别":
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/ocr/v1/train_ticket");
                        break;
                    default:
                        host = string.Format(host, "https://aip.baidubce.com/rest/2.0/image-classify/v2/advanced_general");
                        break;
                }

            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            // 图片的base64编码
            string base64 = getFileBase64(this.textBox1.Text.Trim());
            String str = "image=" + HttpUtility.UrlEncode(base64) + "&top_num=" + 6;
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            richTextBox1.Text = result;

        }

        public static String getFileBase64(String fileName)
        {
            FileStream filestream = new FileStream(fileName, FileMode.Open);
            byte[] arr = new byte[filestream.Length];
            filestream.Read(arr, 0, (int)filestream.Length);
            string baser64 = Convert.ToBase64String(arr);
            filestream.Close();
            return baser64;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("请选择一个图片！");
                return;
            }
            ReconBill();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string fname = openFileDialog.FileName;
            if (fname.Length > 0)
            {
                if (fname.ToLower().EndsWith(".jpg") || fname.ToLower().EndsWith(".png"))
                    this.textBox1.Text = fname;
                else
                {
                    this.textBox1.Text = "";
                    MessageBox.Show("请选择一个图片！");
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.SelectedText = this.comboBox1.SelectedItem.ToString();

        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }
    }
}
