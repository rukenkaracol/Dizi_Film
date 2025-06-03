using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dizi_FilmV20
{
    public partial class frmKayit : Form
    {
        public frmKayit()
        {
            InitializeComponent();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            if (username == "" || email == "" || sifre == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, Email, Password) VALUES (@u, @e, @p)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@p", sifre);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Kayıt başarılı! Artık giriş yapabilirsin.");
                        this.Close(); // formu kapat
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız oldu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
