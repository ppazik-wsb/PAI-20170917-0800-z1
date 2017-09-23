using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{

    public class Student
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        private DateTime dataNarodzin;

        public DateTime DataNarodzin
        {
            get { return dataNarodzin; }
            set {
                if (value < DateTime.Parse("01-01-1900 00:00:00") || value > DateTime.Now)
                {
                    throw new ApplicationException("Data narodzin studenta poza dozwolonym zakresem!");
                }

                dataNarodzin = value;
            }
        }
    }
}