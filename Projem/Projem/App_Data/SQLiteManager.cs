using Projem.Model;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projem.App_Data
{
    public class SQLiteManager
    {
        SQLiteConnection _sqlconnection;

        public SQLiteManager()
        {
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<OyunBilgi>();
        }
        public int Insert(OyunBilgi o)
        {
            return _sqlconnection.Insert(o);
        }
        public int Delete(int ID)
        {
            return _sqlconnection.Delete<OyunBilgi>(ID);
        }
        public IEnumerable<OyunBilgi> GetAll()
        {
            return _sqlconnection.Table<OyunBilgi>();
        }
        public OyunBilgi Get(int skor)
        {
            return _sqlconnection.Table<OyunBilgi>().Where(x => x.Skor == skor).FirstOrDefault();
        }
        public void Dispose()
        {
            _sqlconnection.Close();
        }

    }
   
}
