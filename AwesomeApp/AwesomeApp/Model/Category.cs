using System;
using SQLite;

namespace AwesomeApp.Model
{
    public class Category
    {


        [PrimaryKey, AutoIncrement]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public Category()
        {
        }
    }
}
