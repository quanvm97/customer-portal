using SQLite;

namespace Demo.Interfaces.LocalDatabase
{
    public interface IDatabaseConnection
    {
        SQLiteConnection SqliteConnection(string databaseName);
        long GetSize(string databaseName);
        string GetDatabasePath();
        //string SaveFile(byte[] bytes);
    }
}
