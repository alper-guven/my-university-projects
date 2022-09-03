-----------------------
!!!! ÖNERİ SİSTEMİ !!!!
-----------------------

&&&& Connection String Oluşturmak

	---Yöntem1--------------------------------------------------------------------
    SqlConnection _connection = new SqlConnection
        	(@"	server=(localdb)\mssqllocaldb;
        		initial catalog=KitapDTS;
        		integrated security=true");
    ---Yöntem2--------------------------------------------------------------------
    string baglantiYolu = @"Data Source=(LocalDB)\v11.0;
    						AttachDbFilename=C:\Users\bil-i7\Documents\Rehber4.mdf;
    						Integrated Security=True;
    						Connect Timeout=30";
    SqlConnection baglanti = new SqlConnection(baglantiYolu);
    ------------------------------------------------------------------------------

&&&& ASP GriedView with ID and Runat Attributes

	<asp:GridView ID="Kitaplar1" runat="server">
	</asp:GridView>

&&&& CodeBehind (ornek.aspx.cs) kısmından GriedView beslemek

        public void CekilenVerileriGoster()
        {
        	// Yeni bir DataSet oluşturulup 
        	// Dataset döndüren bir fonksiyon ile doldurulur
            DataSet kitaplar = _kitapDal.PopulerKitaplarCek();
            // GriedView'e ID'si ile erişip veri kaynağı gösterilir
            Kitaplar1.DataSource = kitaplar.Tables[0];
            // Verileri ilgili GriedView'e yansıtır
            Kitaplar1.DataBind();
        }

------------------------------------------
&&&& Yapılmak İstenen
	/Kullanılan /Sql /Kodları
	^^Fonksiyonun Döndürdüğü Değişken Türü
		public DönütTipi fonksiyon1(){
			// Kodlar
			return DönütTipi;
		};
------------------------------------------

&&&& Ekle
	/INSERT
	^^VOID
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

&&&& Sil
	/DELETE /WHERE
	^^VOID
        public void Sil(int kitapID)
        {
            string commandStr = "DELETE FROM Kitaplar WHERE KitapID=@KitapID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KitapID", kitapID);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

&&&& Güncelle
	/UPDATE /WHERE
	^^VOID
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

&&&& Arama 
	/SELECT /WHERE /LIKE /OR
	^^DataSet
        public DataSet Arama(string arananKelime)
        {
            string komutStr = 
            	@"select YazarID, CONCAT(Isim,' ',Soyisim) as AdSoyad 
            	FROM Yazarlar 
            	WHERE Isim like '%' + @arananKelime + '%' 
            	OR Soyisim = @arananKelime";
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

&&&& Tek Tablodan Select Örneği
	/SELECT /CONCAT /AS /WHERE
	^^DataSet
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

&&&& Tablo Birleştirmeli Select Örneği
	/SELECT /CONCAT /AS /RIGHT JOIN /GROUP BY /HAVING /COUNT /ORDER BY
	^^DataSet
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

&&&& Yüksek Puanlı Kitaplar
	/SELECT /CAST /AVG /AS /DECIMAL /RIGHT JOIN /GROUP BY /HAVING /ORDER BY
	^^DataSet
        public DataSet YuksekPuanliKitaplarCek()
        {
            string komutStr = "SELECT Kitaplar.KitapID, Kitaplar.Ad, "+
            				  		 "CAST(AVG(KitapPuan.Puan) AS DECIMAL(10,2)) as OrtalamaPuan"+
            				  "FROM KitapPuan"+
            				  "RIGHT JOIN Kitaplar on KitapPuan.KitapID = Kitaplar.KitapID"+
            				  "GROUP BY Kitaplar.KitapID, Kitaplar.Ad"+
            				  "HAVING CAST(AVG(KitapPuan.Puan) AS DECIMAL(10,2)) > 0"+
            				  "ORDER BY OrtalamaPuan desc";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kitaplar = new DataSet();
            _connection.Open();
            adapter.Fill(kitaplar);
            _connection.Close();
            return kitaplar;
        }

&&&& Giriş Yap
	/SELECT /WHERE
	^^bool
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

&&&& Kullanıcıyı Nesne (Class) Olarak Çek
	/SELECT /WHERE
	^^Nesne
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

-----------------------
!!!! ÖNERİ SİSTEMİ !!!!
-----------------------

&&&& OkunmusKitaplarByID
	/SELECT /WHERE
	^^List
		public List<int> OkunmusKitaplarByID(int kullaniciID)
		        {
		            _connection.Open();
		            string komutStr = @"SELECT KitapID 
		            					FROM KitapOkunma 
		            					WHERE KullaniciID = @KullaniciID";
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

&&&& KullaniciOneri1
	/SELECT /COUNT /AS /RIGHT JOIN /WHERE /AND /GROUP BY /ORDER BY
	^^DataSet
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
		            
		            string komutStr = @"SELECT Kullanicilar.KullaniciAdi, 
		            						   COUNT(KitapID) as OrtakKitapSayisi, 
		            						   Kullanicilar.KullaniciID
					                    FROM KitapOkunma
		                                RIGHT JOIN Kullanicilar 
		                                		   ON KitapOkunma.KullaniciID = Kullanicilar.KullaniciID
					                    WHERE KitapID IN (" + okunanlarIN + ") " +
					                   "AND Kullanicilar.KullaniciID!=@CurrentUserID" +
					                   "GROUP BY Kullanicilar.KullaniciID, Kullanicilar.KullaniciAdi"+
					                   "ORDER BY OrtakKitapSayisi DESC";
		                                
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

&&&& PuanlanmisKitaplarVePuanlariByID
	/SELECT /WHERE
	^^List
		public List<KitapPuan> PuanlanmisKitaplarVePuanlariByID(int kullaniciID)
		        {
		            _connection.Open();

		            string komutStr = @"SELECT KitapID, Puan FROM KitapPuan 
		            					WHERE KullaniciID = @KullaniciID";
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

&&&& KullaniciOneri2
	/SELECT /RIGHT JOIN /WHERE /AND /GROUP BY /
	^^DataSet
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
		WHERE KitapPuan.KitapID=@KitapID AND KitapPuan.Puan=@Puan 
			  AND KitapPuan.KullaniciID!=@CurrentUserID
		GROUP BY Kullanicilar.KullaniciAdi, KitapPuan.KitapID, 
			     KitapPuan.Puan, Kitaplar.Ad, KitapPuan.KullaniciID";

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

&&&& KitapOneri
	/SELECT /TOP /NEWID /RIGHT JOIN /WHERE IN /AND /WHERE NOT IN /GROUP BY /ORDER BY
	^^DataSet
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
