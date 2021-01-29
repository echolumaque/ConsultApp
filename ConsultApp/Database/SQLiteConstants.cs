using System.IO;

namespace ConsultApp.Database
{
    public static class SQLiteConstants
    {
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache ;

        public static string DatabasePath
        {
            get => Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "PendingConsultations.db3");
        }
    }
}
