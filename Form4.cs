using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using task4_paa.models;

namespace task4_paa
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var newProduct = new Product
            {
                name = textBox1.Text,
                price = int.Parse(textBox2.Text),
                stock = int.Parse(textBox3.Text)
            };

            var jsonContent = JsonConvert.SerializeObject(newProduct);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Form2.AccessToken);
                    var response = await client.PostAsync("https://localhost:7131/products", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produk berhasil ditambahkan.");
                        this.Close();
                        Form1 form1 = new Form1();
                        form1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menambahkan produk. Status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }
    }
}

