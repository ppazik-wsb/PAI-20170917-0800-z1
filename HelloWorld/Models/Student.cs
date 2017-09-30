using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{

    public class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Imię")]
        public string Imie { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Nazwisko { get; set; }

        private DateTime dataNarodzin;

        [DisplayName("Data narodzin")]
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