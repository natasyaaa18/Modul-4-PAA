using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task4_paa.models;

namespace task4_paa
{
    public partial class Form5 : Form
    {
        private int productId;
        private string apiUrl = "https://localhost:7131/products";

        public Form5(int id, string name, int price, int stock)
        {
            InitializeComponent();
            productId = id;
            textBox1.Text = name;
            textBox2.Text = price.ToString();
            textBox3.Text = stock.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string updatedName = textBox1.Text;
            int updatedPrice = int.Parse(textBox2.Text);
            int updatedStock = int.Parse(textBox3.Text);

            var updatedProduct = new Product
            {
                id = productId,
                name = updatedName,
                price = updatedPrice,
                stock = updatedStock
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Form2.AccessToken);
                    string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(updatedProduct);
                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{apiUrl}/{productId}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produk berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengubah produk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
