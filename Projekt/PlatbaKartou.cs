using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class PlatbaKartou : IBaseClass
    {
        private int id;
        private float castka;
        private string cisloKarty;
        private string typKarty;
        private DateTime platnostKarty;

        public int ID { get => id; set => id = value; }
        public float Castka { get => castka; set => castka = value; }
        public string CisloKarty { get => cisloKarty; set => cisloKarty = value; }
        public string TypKarty { get => typKarty; set => typKarty = value; }
        public DateTime PlatnostKarty { get => platnostKarty; set => platnostKarty = value; }

        public PlatbaKartou(int id, float castka, string cisloKarty, string typKarty, DateTime platnostKarty)
        {
            this.ID = id;
            this.Castka = castka;
            this.CisloKarty = cisloKarty;
            this.TypKarty = typKarty;
            this.PlatnostKarty = platnostKarty;
        }

        public PlatbaKartou(float castka, string cisloKarty, string typKarty, DateTime platnostKarty)
        {
            this.ID = 0;
            this.Castka = castka;
            this.CisloKarty = cisloKarty;
            this.TypKarty = typKarty;
            this.PlatnostKarty = platnostKarty;
        }

        public override string ToString()
        {
            return $"{ID}. Částka: {Castka}, Číslo karty: {CisloKarty}, Typ karty: {TypKarty}, Platnost karty: {PlatnostKarty}";
        }
    }

}
