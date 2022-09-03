using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class KitapPuan
    {
        public int KitapID { get; set; }
        public int Puan { get; set; }
    }
    public class TavsiyeDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=KitapDTS;integrated security=true");
        public List<int> OkunmusKitaplarByID(int kullaniciID)
        {
            _connection.Open();
            string komutStr = @"SELECT KitapID FROM KitapOkunma WHERE KullaniciID = @KullaniciID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataReader reader = komut.ExecuteReader();
            List<Int32> OkunmusKitaplar = new List<Int32>();

            while (reader.Read())
            {
                int kitapID = Convert.ToInt32(reader["KitapID"]);
                OkunmusKitaplar.Add(kitapID);
            }

            reader.Close();
            _connection.Close();
            return OkunmusKitaplar;
        }
        public DataSet KullaniciOneri1(int currentUserID, List<int> CurrentUserOkunanlar)
        {
            string okunanlarIN = "";
            int kitapSayisi = CurrentUserOkunanlar.Count();
            foreach (var kitapID in CurrentUserOkunanlar)
            {
                kitapSayisi--;
                okunanlarIN += kitapID;
                if (kitapSayisi > 0)
                {
                    okunanlarIN += ", ";
                }
            }
            
            string komutStr = @"SELECT Kullanicilar.KullaniciAdi, COUNT(KitapID) as OrtakKitapSayisi, Kullanicilar.KullaniciID
			                    FROM KitapOkunma
                                right join Kullanicilar on KitapOkunma.KullaniciID = Kullanicilar.KullaniciID
			                    WHERE KitapID IN (" + okunanlarIN + ") AND Kullanicilar.KullaniciID!=@CurrentUserID GROUP BY Kullanicilar.KullaniciID, Kullanicilar.KullaniciAdi ORDER BY OrtakKitapSayisi DESC";
                                
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@CurrentUserID", currentUserID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerinlenler = new DataSet();
            _connection.Open();
            adapter.Fill(onerinlenler);
            _connection.Close();
            return onerinlenler;
        }

        public List<KitapPuan> PuanlanmisKitaplarVePuanlariByID(int kullaniciID)
        {
            _connection.Open();

            string komutStr = @"SELECT KitapID, Puan FROM KitapPuan WHERE KullaniciID = @KullaniciID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataReader reader = komut.ExecuteReader();
            List<KitapPuan> KitaplarVePuanlar = new List<KitapPuan>();

            while (reader.Read())
            {
                KitapPuan PairKitapPuan = new KitapPuan
                {
                    KitapID = Convert.ToInt32(reader["KitapID"]),
                    Puan = Convert.ToInt32(reader["Puan"])
                };

                KitaplarVePuanlar.Add(PairKitapPuan);
            }

            reader.Close();
            _connection.Close();
            return KitaplarVePuanlar;
        }
        public DataSet KullaniciOneri2(int currentUserID, List<KitapPuan> KitaplarVePuanlar)
        {
            _connection.Open();
            DataSet onerinlenler = new DataSet();
            foreach (var PairKitapPuan in KitaplarVePuanlar)
            {
                string komutStr = @"
SELECT Kullanicilar.KullaniciAdi,
	   KitapPuan.KitapID, KitapPuan.Puan, Kitaplar.Ad, KitapPuan.KullaniciID
FROM KitapPuan
RIGHT JOIN Kullanicilar on KitapPuan.KullaniciID = Kullanicilar.KullaniciID
RIGHT JOIN Kitaplar on KitapPuan.KitapID = Kitaplar.KitapID
WHERE KitapPuan.KitapID=@KitapID AND KitapPuan.Puan=@Puan AND KitapPuan.KullaniciID!=@CurrentUserID
GROUP BY Kullanicilar.KullaniciAdi, KitapPuan.KitapID, KitapPuan.Puan, Kitaplar.Ad, KitapPuan.KullaniciID";

                SqlCommand komut = new SqlCommand(komutStr, _connection);
                komut.Parameters.AddWithValue("@KitapID", PairKitapPuan.KitapID);
                komut.Parameters.AddWithValue("@Puan", PairKitapPuan.Puan);
                komut.Parameters.AddWithValue("@CurrentUserID", currentUserID);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komut;
                adapter.Fill(onerinlenler);
            }

            _connection.Close();
            return onerinlenler;
        }

        public DataSet KitapOneri(List<int> CurrentUserOkunanlar, List<int> OnerilenKullanicilarIDs)
        {
            string okunanlarNotIN = "";
            int kitapSayisi = CurrentUserOkunanlar.Count();
            foreach (var kitapID in CurrentUserOkunanlar)
            {
                kitapSayisi--;
                okunanlarNotIN += kitapID;
                if (kitapSayisi > 0)
                {
                    okunanlarNotIN += ", ";
                }
            }

            string kullanicilarIN = "";
            int kullaniciSayisi = OnerilenKullanicilarIDs.Count();
            foreach (var xkullaniciID in OnerilenKullanicilarIDs)
            {
                kullaniciSayisi--;
                kullanicilarIN += xkullaniciID;
                if (kullaniciSayisi > 0)
                {
                    kullanicilarIN += ", ";
                }
            }

            string komutStr = @"SELECT TOP(10) NEWID() as RandomID, Kitaplar.Ad, Kitaplar.KitapID
			                    FROM KitapOkunma
                                right join Kitaplar on KitapOkunma.KitapID = Kitaplar.KitapID
			                    WHERE KitapOkunma.KitapID NOT IN (" + okunanlarNotIN + ") AND KitapOkunma.KullaniciID IN (" + kullanicilarIN + ")" +
                                " GROUP BY Kitaplar.Ad, Kitaplar.KitapID ORDER BY RandomID";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerilenKitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(onerilenKitaplar);
            _connection.Close();

            return onerilenKitaplar;
        }

    }
}