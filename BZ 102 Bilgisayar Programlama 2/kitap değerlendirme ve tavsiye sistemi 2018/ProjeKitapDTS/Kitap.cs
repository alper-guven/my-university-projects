using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeKitapDTS
{
    public class Kitap
    {
        public int KitapID { get; set; }
        public int YazarID { get; set; }
        public string Ad { get; set; }
        public string Yayinevi { get; set; }
        public string TanitimBilgisi { get; set; }
        public string ResimURL { get; set; }
    }
}