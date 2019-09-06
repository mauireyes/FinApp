using System;
using SQLite;
namespace AwesomeApp.Model
{
    public class Transaction
    {
        public Transaction()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }


        public override string ToString()
        {
           
            return string.Format($"{Category} ({Amount})");
        }
    }
}
