using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    //Dubbelkolla paruppgift Customer.cs
    internal class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";

        //Print Student info
        public override string ToString()
        {
            return $"Förnamn: {FirstName}\n" +
                $"Efternamn: {LastName}\n" +
                $"Stad: {City}\n";
        }
    }
}
