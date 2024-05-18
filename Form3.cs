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

namespace task4_paa
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var requestBody = new
                    {
                        username = username,
                        password = password
                    };

                    string apiUrl = "https://localhost:7131/register";
                    string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Registrasi berhasil. Silakan masuk dengan akun yang baru dibuat.", "Registrasi Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Registrasi gagal. Silakan coba lagi.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat melakukan registrasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
