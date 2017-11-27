using SQLite.Net.Attributes;
using System;

namespace Projem.Model
{
    public class OyunBilgi
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public DateTime BaslangicZamani { get; set; }
        public int Skor { get; set; }
    }
}
