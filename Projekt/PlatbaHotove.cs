using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class PlatbaHotove : IBaseClass
    {
        private int id;
        private float castka;
        private DateTime datum;

        public int ID { get => id; set => id = value; }
        public float Castka { get => castka; set => castka = value; }
        public DateTime Datum { get => datum; set => datum = value; }

        public PlatbaHotove(int id, float castka, DateTime datum)
        {
            this.ID = id;
            this.Castka = castka;
            this.Datum = datum;
        }

        public PlatbaHotove(float castka, DateTime datum)
        {
            this.ID = 0;
            this.Castka = castka;
            this.Datum = datum;
        }

        public override string ToString()
        {
            return $"{ID}. Částka: {Castka}, Datum: {Datum}";
        }
    }

}
