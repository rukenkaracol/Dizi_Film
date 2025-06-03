using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        // SQL bağlantı dizesi
        return new SqlConnection("Server=.;Database=Dizi_FilmV20;Integrated Security=True;");
    }
}
