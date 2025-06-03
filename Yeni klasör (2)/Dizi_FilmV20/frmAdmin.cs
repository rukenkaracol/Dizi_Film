using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dizi_FilmV20
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            this.Load += frmAdmin_Load;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            VerileriYenile();
        }

        private void VerileriYenile()
        {
            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Contents", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtTitle.Text = dataGridView1.CurrentRow.Cells["Title"].Value.ToString();
                txtGenre.Text = dataGridView1.CurrentRow.Cells["Genre"].Value.ToString();
                txtDuration.Text = dataGridView1.CurrentRow.Cells["Duration"].Value.ToString();
                txtYear.Text = dataGridView1.CurrentRow.Cells["ReleaseYear"].Value.ToString();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Contents (Title, Genre, Duration, ReleaseYear) VALUES (@t, @g, @d, @y)", conn);
                cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@g", txtGenre.Text.Trim());
                cmd.Parameters.AddWithValue("@d", txtDuration.Text.Trim());
                cmd.Parameters.AddWithValue("@y", Convert.ToInt32(txtYear.Text.Trim()));
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("İçerik eklendi.");
            VerileriYenile();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int contentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ContentID"].Value);

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Contents WHERE ContentID = @id", conn);
                cmd.Parameters.AddWithValue("@id", contentId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("İçerik silindi.");
            VerileriYenile();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int contentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ContentID"].Value);

            using (SqlConnection conn = new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Contents SET Title = @t, Genre = @g, Duration = @d, ReleaseYear = @y WHERE ContentID = @id", conn);
                cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@g", txtGenre.Text.Trim());
                cmd.Parameters.AddWithValue("@d", txtDuration.Text.Trim());
                cmd.Parameters.AddWithValue("@y", Convert.ToInt32(txtYear.Text.Trim()));
                cmd.Parameters.AddWithValue("@id", contentId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("İçerik güncellendi.");
            VerileriYenile();
        }
    }
}
