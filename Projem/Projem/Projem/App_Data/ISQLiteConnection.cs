using SQLite.Net;
namespace Projem.App_Data
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
