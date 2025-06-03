using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Dizi_FilmV20
{
    public partial class frmKullanici : Form
    {
        int girisYapanKullaniciId;

        public frmKullanici(int kullaniciId)
        {
            InitializeComponent();
            girisYapanKullaniciId = kullaniciId;
            this.Load += frmKullanici_Load;
        }

        private void frmKullanici_Load(object sender, EventArgs e)
        {
            // Görsel ayarlar
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbDurum.Items.Clear();
            cmbDurum.Items.AddRange(new string[] { "İzliyor", "Tamamlandı", "Bıraktı" });

            nudPuan.Minimum = 1;
            nudPuan.Maximum = 10;

            VerileriYukle();
            KullaniciKayitlariniYukle();
        }

        private void VerileriYukle(string arama = "")
        {
            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                string query = "SELECT ContentID, Title, Genre, Duration, ReleaseYear FROM Contents";

                if (!string.IsNullOrWhiteSpace(arama))
                {
                    query += " WHERE Title LIKE @search";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrWhiteSpace(arama))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + arama + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                if (dataGridView1.Columns.Contains("ContentID"))
                    dataGridView1.Columns["ContentID"].Visible = false;
            }
        }

        private void KullaniciKayitlariniYukle()
        {
            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"
                    SELECT C.Title, W.WatchStatus, W.Rating
                    FROM WatchRecords W
                    INNER JOIN Contents C ON C.ContentID = W.ContentID
                    WHERE W.UserID = @u";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", girisYapanKullaniciId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || cmbDurum.SelectedItem == null)
            {
                MessageBox.Show("Lütfen içerik ve izleme durumu seçin.");
                return;
            }

            int contentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ContentID"].Value);
            string durum = cmbDurum.SelectedItem.ToString();
            int puan = (int)nudPuan.Value;

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                string kontrol = "SELECT COUNT(*) FROM WatchRecords WHERE UserID = @u AND ContentID = @c";
                SqlCommand cmd = new SqlCommand(kontrol, conn);
                cmd.Parameters.AddWithValue("@u", girisYapanKullaniciId);
                cmd.Parameters.AddWithValue("@c", contentId);

                int kayitVarMi = (int)cmd.ExecuteScalar();

                if (kayitVarMi > 0)
                {
                    SqlCommand update = new SqlCommand("UPDATE WatchRecords SET WatchStatus = @s, Rating = @r WHERE UserID = @u AND ContentID = @c", conn);
                    update.Parameters.AddWithValue("@s", durum);
                    update.Parameters.AddWithValue("@r", puan);
                    update.Parameters.AddWithValue("@u", girisYapanKullaniciId);
                    update.Parameters.AddWithValue("@c", contentId);
                    update.ExecuteNonQuery();
                    MessageBox.Show("Kayıt güncellendi.");
                }
                else
                {
                    SqlCommand insert = new SqlCommand("INSERT INTO WatchRecords (UserID, ContentID, WatchStatus, Rating) VALUES (@u, @c, @s, @r)", conn);
                    insert.Parameters.AddWithValue("@u", girisYapanKullaniciId);
                    insert.Parameters.AddWithValue("@c", contentId);
                    insert.Parameters.AddWithValue("@s", durum);
                    insert.Parameters.AddWithValue("@r", puan);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Kayıt eklendi.");
                }

                KullaniciKayitlariniYukle();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            VerileriYukle(txtAra.Text.Trim());
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.");
                return;
            }

            string baslik = dataGridView2.CurrentRow.Cells["Title"].Value.ToString();

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"DELETE FROM WatchRecords 
                                 WHERE UserID = @u 
                                 AND ContentID = (SELECT ContentID FROM Contents WHERE Title = @t)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", girisYapanKullaniciId);
                cmd.Parameters.AddWithValue("@t", baslik);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kayıt silindi.");
            KullaniciKayitlariniYukle();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            new frmGiris().Show();
            this.Close();
        }
    }
}
