using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class KitapDal
    {
        static SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=KitapDTS;integrated security=true");

        public List<Kitap> GetAll()
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Kitaplar", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Kitap> kitaplar = new List<Kitap>();

            while (reader.Read())
            {
                Kitap kitap = new Kitap
                {
                    KitapID = Convert.ToInt32(reader["KitapID"]),
                    YazarID = Convert.ToInt32(reader["YazarID"]),
                    Ad = reader["Ad"].ToString(),
                    Yayinevi = reader["Yayinevi"].ToString(),
                    TanitimBilgisi = reader["TanitimBilgisi"].ToString(),
                    ResimURL = reader["ResimURL"].ToString()
                };
                kitaplar.Add(kitap);
            }

            reader.Close();
            _connection.Close();
            return kitaplar;
        }
        public Kitap GetByID(int kitapID)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Kitaplar where KitapID = @KitapID", _connection);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            SqlDataReader reader = command.ExecuteReader();

            List<Kitap> kitaplar = new List<Kitap>();

            while (reader.Read())
            {
                Kitap kitap = new Kitap
                {
                    KitapID = Convert.ToInt32(reader["KitapID"]),
                    YazarID = Convert.ToInt32(reader["YazarID"]),
                    Ad = reader["Ad"].ToString(),
                    Yayinevi = reader["Yayinevi"].ToString(),
                    TanitimBilgisi = reader["TanitimBilgisi"].ToString(),
                    ResimURL = reader["ResimURL"].ToString()
                };
                kitaplar.Add(kitap);
            }

            reader.Close();
            _connection.Close();
            return kitaplar[0];
        }
        public int OrtalamaPuanByKitapID(int KitapID)
        {
            string komutStr = "SELECT AVG(KitapPuan.Puan) as OrtalamaPuan FROM KitapPuan WHERE KitapID = @KitapID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KitapID", KitapID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitap = new DataSet();
            _connection.Open();
            adapter.Fill(kitap);
            _connection.Close();
            int KitapOkunma = 0;

            if (kitap.Tables[0]!=null)
            {
                if (kitap.Tables[0].Rows[0]["OrtalamaPuan"] != DBNull.Value)
                {
                    KitapOkunma = Convert.ToInt32(kitap.Tables[0].Rows[0]["OrtalamaPuan"]);
                }
            }
            return KitapOkunma;
        }
        public int OkunmaSayisiByKitapID(int KitapID)
        {
            string komutStr = "SELECT COUNT(KitapID) as OkunmaSayisi FROM KitapOkunma WHERE KitapID = @KitapID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KitapID", KitapID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitap = new DataSet();
            _connection.Open();
            adapter.Fill(kitap);
            _connection.Close();
            int KitapOrtPuan = Convert.ToInt32(kitap.Tables[0].Rows[0]["OkunmaSayisi"]);
            return KitapOrtPuan;
        }
        public DataSet GetByYazarID(int yazarID)
        {
            SqlCommand komut = new SqlCommand("select * from Kitaplar where YazarID=@YazarID", _connection);
            komut.Parameters.AddWithValue("@YazarID", yazarID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitap = new DataSet();
            _connection.Open();
            adapter.Fill(kitap);
            _connection.Close();
            return kitap;
        }

       
        public DataSet kitaplarCek()
        {
            SqlCommand komut = new SqlCommand("select * from Kitaplar", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(kitaplar);
            _connection.Close();
            return kitaplar;
        }
        public DataSet PopulerKitaplarCek()
        {
            string komutStr = "SELECT Kitaplar.KitapID, Kitaplar.Ad, COUNT(KitapOkunma.KitapID) as OkunmaSayisi FROM KitapOkunma RIGHT JOIN Kitaplar on KitapOkunma.KitapID = Kitaplar.KitapID GROUP BY Kitaplar.KitapID, Kitaplar.Ad HAVING COUNT(KitapOkunma.KitapID) > 0 ORDER BY OkunmaSayisi desc";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(kitaplar);
            _connection.Close();
            return kitaplar;
        }
        public DataSet YuksekPuanliKitaplarCek()
        {
            string komutStr = "SELECT Kitaplar.KitapID, Kitaplar.Ad, CAST(AVG(KitapPuan.Puan) AS DECIMAL(10,2)) as OrtalamaPuan FROM KitapPuan RIGHT JOIN Kitaplar on KitapPuan.KitapID = Kitaplar.KitapID GROUP BY Kitaplar.KitapID, Kitaplar.Ad HAVING CAST(AVG(KitapPuan.Puan) AS DECIMAL(10,2)) > 0 ORDER BY OrtalamaPuan desc";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(kitaplar);
            _connection.Close();
            return kitaplar;
        }

        public void Ekle(Kitap kitap)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into Kitaplar (YazarID, Ad, Yayinevi, TanitimBilgisi, ResimURL) values (@YazarID, @Ad, @Yayinevi, @TanitimBilgisi, @ResimURL)", _connection);
            command.Parameters.AddWithValue("@YazarID", kitap.YazarID);
            command.Parameters.AddWithValue("@Ad", kitap.Ad);
            command.Parameters.AddWithValue("@Yayinevi", kitap.Yayinevi);
            command.Parameters.AddWithValue("@TanitimBilgisi", kitap.TanitimBilgisi);
            command.Parameters.AddWithValue("@ResimURL", kitap.ResimURL);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Sil(int kitapID)
        {
            string commandStr = "DELETE FROM Kitaplar WHERE KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Guncelle(Kitap kitap)
        {
            string commandStr = "UPDATE Kitaplar SET YazarID=@YazarID, Ad=@Ad, Yayinevi=@Yayinevi, TanitimBilgisi=@TanitimBilgisi WHERE KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KitapID", kitap.KitapID);
            command.Parameters.AddWithValue("@YazarID", kitap.YazarID);
            command.Parameters.AddWithValue("@Ad", kitap.Ad);
            command.Parameters.AddWithValue("@Yayinevi", kitap.Yayinevi);
            command.Parameters.AddWithValue("@TanitimBilgisi", kitap.TanitimBilgisi);


            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select KitapID, Ad from Kitaplar where Ad like '%' + @arananKelime + '%' ";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(kitaplar);
            _connection.Close();
            return kitaplar;
        }

        public void PuanVer(int kullaniciID, int kitapID, decimal puan)
        {
            string commandStr = "INSERT INTO KitapPuan(KullaniciID, KitapID, Puan) values(@KullaniciID, @KitapID, @Puan)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);
            command.Parameters.AddWithValue("@Puan", puan);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolPuanVer(int kullaniciID, int kitapID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM KitapPuan WHERE KullaniciID=@KullaniciID AND KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }
            
            return Kontrol;
        }
        public void IncelemeYaz(int kullaniciID, int kitapID, string inceleme)
        {
            string commandStr = "INSERT INTO KitapInceleme(KullaniciID, KitapID, Inceleme) values(@KullaniciID, @KitapID, @Inceleme)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);
            command.Parameters.AddWithValue("@Inceleme", inceleme);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolIncelemeYaz(int kullaniciID, int kitapID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM KitapInceleme WHERE KullaniciID=@KullaniciID AND KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }

        public void AlintiYap(int kullaniciID, int kitapID, int sayfaNo, string cumle)
        {
            string commandStr = "INSERT INTO KitapAlinti(KullaniciID, KitapID, SayfaNo, Cumle) values(@KullaniciID, @KitapID, @SayfaNo, @Cumle)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);
            command.Parameters.AddWithValue("@SayfaNo", sayfaNo);
            command.Parameters.AddWithValue("@Cumle", cumle);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolAlintiYap(int kullaniciID, int kitapID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM KitapAlinti WHERE KullaniciID=@KullaniciID AND KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }
        public void OkunmaKayit(int kullaniciID, int kitapID)
        {
            string commandStr = "INSERT INTO KitapOkunma(KullaniciID, KitapID) values(@KullaniciID, @KitapID)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolOkunmaKayit(int kullaniciID, int kitapID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM KitapOkunma WHERE KullaniciID=@KullaniciID AND KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }
        public DataSet IncelemelerCek(int kitapID)
        {
            SqlCommand komut = new SqlCommand("select * from KitapInceleme RIGHT JOIN Kullanicilar ON KitapInceleme.KullaniciID = Kullanicilar.KullaniciID where KitapID=@KitapID", _connection);
            komut.Parameters.AddWithValue("@KitapID", kitapID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet incelemeler = new DataSet();
            _connection.Open();
            adapter.Fill(incelemeler);
            _connection.Close();
            return incelemeler;
        }
        public DataSet IncelemelerCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from KitapInceleme RIGHT JOIN Kitaplar on KitapInceleme.KitapID = Kitaplar.KitapID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet incelemeler = new DataSet();
            _connection.Open();
            adapter.Fill(incelemeler);
            _connection.Close();
            return incelemeler;
        }

        public DataSet AlintilarCek(int kitapID)
        {
            SqlCommand komut = new SqlCommand("select * from KitapAlinti RIGHT JOIN Kullanicilar ON KitapAlinti.KullaniciID = Kullanicilar.KullaniciID where KitapID=@KitapID", _connection);
            komut.Parameters.AddWithValue("@KitapID", kitapID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet alintilar = new DataSet();
            _connection.Open();
            adapter.Fill(alintilar);
            _connection.Close();
            return alintilar;
        }
        public DataSet AlintilarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from KitapAlinti RIGHT JOIN Kitaplar on KitapAlinti.KitapID = Kitaplar.KitapID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet alintilar = new DataSet();
            _connection.Open();
            adapter.Fill(alintilar);
            _connection.Close();
            return alintilar;
        }
        public DataSet OkunanlarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select Kitaplar.KitapID, Kitaplar.Ad from KitapOkunma RIGHT JOIN Kitaplar on KitapOkunma.KitapID = Kitaplar.KitapID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet okunanlar = new DataSet();
            _connection.Open();
            adapter.Fill(okunanlar);
            _connection.Close();
            return okunanlar;
        }
        public DataSet PuanlarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from KitapPuan RIGHT JOIN Kitaplar on KitapPuan.KitapID = Kitaplar.KitapID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet puanlar = new DataSet();
            _connection.Open();
            adapter.Fill(puanlar);
            _connection.Close();
            return puanlar;
        }

    }
}