using System;
using SQLite;

namespace AwesomeApp.Model
{
    public class Person
    {
        public Person()
        {
        }
       [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
       


        public override string ToString()
        {
           

            return string.Format($"{Name} ({TotalBalance})");
        
    }
    }
}
