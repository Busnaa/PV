using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Obchod : IBaseClass
    {
        private int id;
        private string nazev;
        private string adresa;
        private string email;
        private string telefon;

        public int ID { get => id; set => id = value; }
        public string Nazev { get => nazev; set => nazev = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string Email { get => email; set => email = value; }
        public string Telefon { get => telefon; set => telefon = value; }

        public Obchod(int id, string nazev, string adresa, string email, string telefon)
        {
            this.ID = id;
            this.Nazev = nazev;
            this.Adresa = adresa;
            this.Email = email;
            this.Telefon = telefon;
        }

        public Obchod(string nazev, string adresa, string email, string telefon)
        {
            this.ID = 0;
            this.Nazev = nazev;
            this.Adresa = adresa;
            this.Email = email;
            this.Telefon = telefon;
        }

        public override string ToString()
        {
            return $"{ID}. {Nazev} - {Adresa}, {Email}, {Telefon}";
        }
    }

}
