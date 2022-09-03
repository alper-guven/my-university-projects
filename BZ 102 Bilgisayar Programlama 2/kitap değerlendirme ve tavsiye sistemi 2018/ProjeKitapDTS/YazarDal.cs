using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class YazarDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=KitapDTS;integrated security=true");
        public List<Yazar> GetAll()
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Yazarlar", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Yazar> yazarlar = new List<Yazar>();

            while (reader.Read())
            {
                Yazar yazar = new Yazar
                {
                    YazarID = Convert.ToInt32(reader["YazarID"]),
                    Isim = reader["Isim"].ToString(),
                    Soyisim = reader["Soyisim"].ToString(),
                    DogumTarihi = reader["DogumTarihi"].ToString(),
                    DogumYeri = reader["DogumYeri"].ToString(),
                    OlumTarihi = reader["OlumTarihi"].ToString()
                };
                yazarlar.Add(yazar);
            }

            reader.Close();
            _connection.Close();
            return yazarlar;
        }
        public DataSet GetByID(int yazarID)
        {
            string komutStr = @"select *, CONCAT(Isim,' ',Soyisim) as AdSoyad from Yazarlar where YazarID=@YazarID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@YazarID", yazarID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet yazar = new DataSet();
            _connection.Open();
            adapter.Fill(yazar);
            _connection.Close();
            return yazar;
        }
        public DataSet PopulerYazarlarCek()
        {
            string komutStr = @"SELECT COUNT(Yazarlar.YazarID) as YazarOkunma, 
	   CONCAT(Isim,' ',Soyisim) as AdSoyad,
	   Yazarlar.YazarID
FROM KitapOkunma
RIGHT JOIN Kitaplar on KitapOkunma.KitapID = Kitaplar.KitapID 
RIGHT JOIN Yazarlar on Kitaplar.YazarID = Yazarlar.YazarID
GROUP BY Yazarlar.YazarID, Isim, Soyisim
HAVING COUNT(Yazarlar.YazarID) > 0 
ORDER BY YazarOkunma desc";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet yazarlar = new DataSet();
            _connection.Open();
            adapter.Fill(yazarlar);
            _connection.Close();
            return yazarlar;
        }

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select YazarID, CONCAT(Isim,' ',Soyisim) as AdSoyad from Yazarlar where Isim like '%' + @arananKelime + '%' or Soyisim = @arananKelime";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet yazarlar = new DataSet();
            _connection.Open();
            adapter.Fill(yazarlar);
            _connection.Close();
            return yazarlar;
        }

    }
}