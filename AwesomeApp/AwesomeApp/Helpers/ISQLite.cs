using System;
using SQLite.Net;
namespace AwesomeApp.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
  
    }
}

