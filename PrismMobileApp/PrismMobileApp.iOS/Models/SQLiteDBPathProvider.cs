using PrismMobileApp.Models;

namespace PrismMobileApp.iOS.Models
{
    class SQLiteDBPathProvider : ISQLiteDBPathProvider
    {
        public string GetPath()
        {
            SQLitePCL.Batteries.Init();
            return "localstore.db";
        }
    }
}