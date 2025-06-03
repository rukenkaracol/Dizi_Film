using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dizi_FilmV20
{
    public partial class frmGiris : Form
    {
        public frmGiris()
        {
            InitializeComponent();

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Email = @e AND Password = @p";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@p", sifre);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int userId = Convert.ToInt32(dr["UserID"]);
                    bool isAdmin = Convert.ToBoolean(dr["IsAdmin"]);
                    MessageBox.Show("Giriş başarılı!");

                    if (isAdmin)
                    {
                        frmAdmin adminForm = new frmAdmin();
                        adminForm.Show();
                    }
                    else
                    {
                        frmKullanici kullaniciForm = new frmKullanici(userId);
                        kullaniciForm.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("E-posta veya şifre hatalı.");
                }

                dr.Close();
            }
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            frmKayit kayitForm = new frmKayit();
            kayitForm.ShowDialog();
        }
    }
}
