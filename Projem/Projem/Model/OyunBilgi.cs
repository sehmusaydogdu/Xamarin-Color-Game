using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projem.Model
{
    public class OyunBilgi
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public int Skor { get; set; }
    }
}
