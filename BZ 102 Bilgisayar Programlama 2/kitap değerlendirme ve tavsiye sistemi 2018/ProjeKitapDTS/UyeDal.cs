using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class UyeDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=KitapDTS;integrated security=true");

        public void Ekle(Uye uye)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into Kullanicilar (isim, Soyisim, Cinsiyet, DogumTarihi, KullaniciAdi, Sifre) values (@isim,@Soyisim,@Cinsiyet,@DogumTarihi,@KullaniciAdi,@Sifre)", _connection);
            command.Parameters.AddWithValue("@isim", uye.Isim);
            command.Parameters.AddWithValue("@Soyisim", uye.Soyisim);
            command.Parameters.AddWithValue("@Cinsiyet", uye.Cinsiyet);
            command.Parameters.AddWithValue("@DogumTarihi", uye.DogumTarihi);
            command.Parameters.AddWithValue("@KullaniciAdi", uye.KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", uye.Sifre);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public bool GirisYap(string KullaniciAdi, string Sifre)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "select * from Kullanicilar where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", Sifre);

            SqlDataAdapter adaptor = new SqlDataAdapter(command);
            DataSet sonucDS = new DataSet();
            adaptor.Fill(sonucDS);
            _connection.Close();

            bool sonuc = false;
            if (sonucDS.Tables[0].Rows.Count > 0)
                sonuc = true;

            return sonuc;
        }

        public int GetIDbyKullaniciAdi(string kullaniciAdi)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select KullaniciID from Kullanicilar where KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            SqlDataReader reader = command.ExecuteReader();

            List<Uye> uyeler = new List<Uye>();

            while (reader.Read())
            {
                Uye uye = new Uye
                {
                    KullaniciID = Convert.ToInt32(reader["KullaniciID"])
                };
                uyeler.Add(uye);
            }

            reader.Close();
            _connection.Close();
            return uyeler[0].KullaniciID;
        }

        public Uye GetByKAdi(string kullaniciAdi)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select KullaniciID, isim, Soyisim from Kullanicilar where KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            SqlDataReader reader = command.ExecuteReader();

            List<Uye> uyeler = new List<Uye>();

            while (reader.Read())
            {
                Uye uye = new Uye
                {
                    Isim = reader["isim"].ToString(),
                    Soyisim = reader["Soyisim"].ToString(),
                    KullaniciID = Convert.ToInt32(reader["KullaniciID"])
                };
                uyeler.Add(uye);
            }

            reader.Close();
            _connection.Close();
            return uyeler[0];
        }

        public bool YoneticiGirisYap(string AdminAdi, string Sifre)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "select * from Yoneticiler where AdminAdi=@KullaniciAdi and Sifre=@Sifre", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", AdminAdi);
            command.Parameters.AddWithValue("@Sifre", Sifre);

            SqlDataAdapter adaptor = new SqlDataAdapter(command);
            DataSet sonucDS = new DataSet();
            adaptor.Fill(sonucDS);
            _connection.Close();

            bool sonuc = false;
            if (sonucDS.Tables[0].Rows.Count > 0)
                sonuc = true;

            return sonuc;
        }

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select KullaniciAdi, CONCAT(isim,' ',Soyisim) as AdSoyad from Kullanicilar where KullaniciAdi like '%' + @arananKelime + '%' ";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet uyeler = new DataSet();
            _connection.Open();
            adapter.Fill(uyeler);
            _connection.Close();
            return uyeler;
        }
        // MesajlarCekByKID
        public DataSet GelenMesajlarCek(int GonderenID, int GidenID)
        {
            string komutStr = @"select * from Mesajlar RIGHT JOIN Kullanicilar ON Mesajlar.GonderenID = Kullanicilar.KullaniciID where GonderenID=@GonderenID and GidenID=@GidenID ";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@GonderenID", GonderenID);
            komut.Parameters.AddWithValue("@GidenID", GidenID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet uyeler = new DataSet();
            _connection.Open();
            adapter.Fill(uyeler);
            _connection.Close();
            return uyeler;
        }
        public void MesajGonder(int GonderenID, int GidenID, string Baslik, string Mesaj, DateTime Tarih)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into Mesajlar (GonderenID, GidenID, Baslik, Mesaj, Tarih) values (@GonderenID, @GidenID, @Baslik, @Mesaj, @Tarih)", _connection);
            command.Parameters.AddWithValue("@GonderenID", GonderenID);
            command.Parameters.AddWithValue("@GidenID", GidenID);
            command.Parameters.AddWithValue("@Baslik", Baslik);
            command.Parameters.AddWithValue("@Mesaj", Mesaj);
            command.Parameters.AddWithValue("@Tarih", Tarih);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        public DataSet MesajAtanlarCek(int GidenID)
        {
            string komutStr = @"select GonderenID, Kullanicilar.KullaniciAdi, COUNT(GonderenID) AS MesajSayisi from Mesajlar RIGHT JOIN Kullanicilar ON Mesajlar.GonderenID = Kullanicilar.KullaniciID where GidenID=@GidenID AND GonderenID!=@GidenID group by GonderenID, Kullanicilar.KullaniciAdi";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@GidenID", GidenID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet gonderenler = new DataSet();
            _connection.Open();
            adapter.Fill(gonderenler);
            _connection.Close();
            return gonderenler;
        }
    }
}