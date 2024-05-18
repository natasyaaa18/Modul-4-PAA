using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task4_paa.models;

namespace task4_paa
{
    public partial class Form1 : Form
    {
        private string apiUrl = "https://localhost:7131/products";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.FormClosed += (s, args) => this.Show();
            form2.Show();
            this.Hide();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string productsJson = await response.Content.ReadAsStringAsync();
                        IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsJson);

                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Id", "Id");
                        dataGridView1.Columns.Add("Nama", "Nama");
                        dataGridView1.Columns.Add("Harga", "Harga");
                        dataGridView1.Columns.Add("Stock", "Stock");

                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                        deleteButtonColumn.Name = "Delete";
                        deleteButtonColumn.Text = "Hapus";
                        deleteButtonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(deleteButtonColumn);

                        DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
                        updateButtonColumn.Name = "Update";
                        updateButtonColumn.Text = "Ubah";
                        updateButtonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(updateButtonColumn);

                        foreach (var product in products)
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[rowIndex].Cells["Id"].Value = product.id;
                            dataGridView1.Rows[rowIndex].Cells["Nama"].Value = product.name;
                            dataGridView1.Rows[rowIndex].Cells["Harga"].Value = product.price;
                            dataGridView1.Rows[rowIndex].Cells["Stock"].Value = product.stock;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengambil data produk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int productId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                await DeleteProductAsync(productId);
                await LoadProductsAsync();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Update"].Index && e.RowIndex >= 0)
            {
                int productId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                string productName = (string)dataGridView1.Rows[e.RowIndex].Cells["Nama"].Value;
                int productPrice = (int)dataGridView1.Rows[e.RowIndex].Cells["Harga"].Value;
                int productStock = (int)dataGridView1.Rows[e.RowIndex].Cells["Stock"].Value;

                Form5 form4 = new Form5(productId, productName, productPrice, productStock);
                form4.ShowDialog();
                await LoadProductsAsync();
            }
        }

        private async Task DeleteProductAsync(int productId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Form2.AccessToken);
                    HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{productId}");
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Gagal menghapus produk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
