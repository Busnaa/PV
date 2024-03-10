using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Zakaznik : IBaseClass
    {

       
            private int id;
            private string jmeno;
            private string prijmeni;
            private string email;
            private DateTime datumRegistrace;

            public int ID { get => id; set => id = value; }
            public string Jmeno { get => jmeno; set => jmeno = value; }
            public string Prijmeni { get => prijmeni; set => prijmeni = value; }
            public string Email { get => email; set => email = value; }
            public DateTime DatumRegistrace { get => datumRegistrace; set => datumRegistrace = value; }

            public Zakaznik(int id, string jmeno, string prijmeni, string email, DateTime datumRegistrace)
            {
                this.ID = id;
                this.Jmeno = jmeno;
                this.Prijmeni = prijmeni;
                this.Email = email;
                this.DatumRegistrace = datumRegistrace;
            }

            public Zakaznik(string jmeno, string prijmeni, string email, DateTime datumRegistrace)
            {
                this.ID = 0;
                this.Jmeno = jmeno;
                this.Prijmeni = prijmeni;
                this.Email = email;
                this.DatumRegistrace = datumRegistrace;
            }

            public override string ToString()
            {
                return $"{ID}. {Jmeno} {Prijmeni} - {Email} ({DatumRegistrace})";
            }
        }

    
}
