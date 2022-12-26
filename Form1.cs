using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace GoiWebAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void loadData()
        {
            string link = "http://localhost/Manh/api/sanpham";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string masp = txt_MaSP.Text;
            string link = "http://localhost/Manh/api/sanpham?masp=" + masp;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arrSP = data as SanPham[];
            dataGridView1.DataSource = arrSP;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&dvtinh={2}&soluong={3}&dongia={4}", txt_MaSP.Text, txt_TenSP.Text, txt_DVTinh.Text, txt_SoLuong.Text, txt_DonGia.Text);
            string link = "http://localhost/Manh/api/sanpham/" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                loadData();
                MessageBox.Show("Them sp tc");
            }
            else
            {
                MessageBox.Show("themsp tb");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txt_MaSP.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            txt_TenSP.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            txt_DVTinh.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            txt_SoLuong.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
            txt_DonGia.Text = dataGridView1.Rows[d].Cells[4].Value.ToString();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {

            string maSP = txt_MaSP.Text.ToString();
            string deleteString = $"http://localhost/Manh/api/sanpham?id={maSP}";
            HttpWebRequest re = HttpWebRequest.CreateHttp(deleteString);
            re.Method = "DELETE";
            re.ContentType = "aplication/json;charset=UTF-8";
            byte[] bytes = Encoding.UTF8.GetBytes(deleteString);
            re.ContentLength = bytes.Length;
            Stream stream = re.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            var data = js.ReadObject(re.GetResponse().GetResponseStream());
            bool kq = (bool)data;

            if (!kq)
            {
                MessageBox.Show("Thất bại");
            }
            else
            {
                Form1_Load(sender, e);
                MessageBox.Show("Thành công");
            }


        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string putString = string.Format("?ma={0}&ten={1}&dvtinh={2}&soluong={3}&dongia={4}", txt_MaSP.Text, txt_TenSP.Text, txt_DVTinh.Text, txt_SoLuong.Text, txt_DonGia.Text);
            string link = "http://localhost/Manh/api/sanpham/" + putString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "PUT";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteArray = Encoding.UTF8.GetBytes(putString);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                loadData();
                MessageBox.Show("Sua sp tc");
            }
            else
            {
                MessageBox.Show("Sua sp tb");
            }
        }
    }
}
