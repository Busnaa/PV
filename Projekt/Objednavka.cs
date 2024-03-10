using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Objednavka : IBaseClass
    {
        private int id;
        private DateTime datumObjednani;
        private float celkovaCena;
        private string status;
        private PlatbaKartou platbaKartou;
        private PlatbaHotove platbaHotove;
        private Zakaznik zakaznik;
        private Obchod obchod;

        public int ID { get => id; set => id = value; }
        public DateTime DatumObjednani { get => datumObjednani; set => datumObjednani = value; }
        public float CelkovaCena { get => celkovaCena; set => celkovaCena = value; }
        public string Status { get => status; set => status = value; }
        public PlatbaKartou PlatbaKartou { get => platbaKartou; set => platbaKartou = value; }
        public PlatbaHotove PlatbaHotove { get => platbaHotove; set => platbaHotove = value; }
        public Zakaznik Zakaznik { get => zakaznik; set => zakaznik = value; }
        public Obchod Obchod { get => obchod; set => obchod = value; }

        public Objednavka(int id, DateTime datumObjednani, float celkovaCena, string status, PlatbaKartou platbaKartou, PlatbaHotove platbaHotove, Zakaznik zakaznik, Obchod obchod)
        {
            this.ID = id;
            this.DatumObjednani = datumObjednani;
            this.CelkovaCena = celkovaCena;
            this.Status = status;
            this.PlatbaKartou = platbaKartou;
            this.PlatbaHotove = platbaHotove;
            this.Zakaznik = zakaznik;
            this.Obchod = obchod;
        }

        public Objednavka(DateTime datumObjednani, float celkovaCena, string status, PlatbaKartou platbaKartou, PlatbaHotove platbaHotove, Zakaznik zakaznik, Obchod obchod)
        {
            this.ID = 0;
            this.DatumObjednani = datumObjednani;
            this.CelkovaCena = celkovaCena;
            this.Status = status;
            this.PlatbaKartou = platbaKartou;
            this.PlatbaHotove = platbaHotove;
            this.Zakaznik = zakaznik;
            this.Obchod = obchod;
        }

        public override string ToString()
        {
            return $"{ID}. Datum objednání: {DatumObjednani}, Celková cena: {CelkovaCena}, Status: {Status}, Platba kartou: {PlatbaKartou}, Platba hotově: {PlatbaHotove}, Zákazník: {Zakaznik}, Obchod: {Obchod}";
        }
    }

}
