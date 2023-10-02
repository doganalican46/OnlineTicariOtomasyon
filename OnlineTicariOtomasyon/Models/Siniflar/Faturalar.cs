using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
    public class Faturalar
    {
        public int FaturaID { get; set; }
        public char FaturaSeriNo { get; set; }
        public string FaturaSiraNo { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime Saat { get; set; }
        public string TeslimEden { get; set; }
        public string TeslimAlan{ get; set; }



    }
}