using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class Yazar
    {
        public int YazarID { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string OlumTarihi { get; set; }
    }
}