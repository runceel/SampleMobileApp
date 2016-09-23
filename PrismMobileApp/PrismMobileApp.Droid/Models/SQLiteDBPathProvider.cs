using System;
using PrismMobileApp.Models;
using System.IO;

namespace PrismMobileApp.Droid.Models
{
    class SQLiteDBPathProvider : ISQLiteDBPathProvider
    {
        public string GetPath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "localstore.db");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            return path;
        }
    }
}