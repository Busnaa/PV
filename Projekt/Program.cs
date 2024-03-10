using System.Configuration;
using System.Data.SqlClient;
using System.Formats.Asn1;
using System.Globalization;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVImporter importer = new CSVImporter();
            string csvFilePath = "objednavky.csv"; 


            while (true)
            {
                Console.WriteLine("1. Vložit novou objednávku");
                Console.WriteLine("2. Upravit existující objednávku");
                Console.WriteLine("3. Smazat objednávku");
                Console.WriteLine("4. Importovat data z CSV");
                Console.WriteLine("5. Konfigurace programu");
                Console.WriteLine("6. Konec");

                Console.Write("Vyberte možnost: ");
                string volba = Console.ReadLine();

                switch (volba)
                {
                    case "1":
                        VlozitObjednavku();
                        break;
                    case "2":
                        UpravitObjednavku();
                        break;
                    case "3":
                        SmazatObjednavku();
                        break;
                    case "4":
                         importer.ImportovatDataZCSV(csvFilePath);
                        break;
                    case "5":
                        KonfiguraceProgramu();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Neplatná volba.");
                        break;
                }
            }
        }

        static void VlozitObjednavku()
        {
            Console.WriteLine("Vkládání nové objednávky");

            // Získání potřebných informací od uživatele
            Console.Write("Datum objednávky (YYYY-MM-DD): ");
            string datumObjednavkyStr = Console.ReadLine();
            DateTime datumObjednavky;
            if (!DateTime.TryParse(datumObjednavkyStr, out datumObjednavky))
            {
                Console.WriteLine("Neplatné datum. Objednávka nebyla vložena.");
                return;
            }

            Console.Write("Celková cena: ");
            float celkovaCena;
            if (!float.TryParse(Console.ReadLine(), out celkovaCena))
            {
                Console.WriteLine("Neplatná cena. Objednávka nebyla vložena.");
                return;
            }

            Console.Write("Status: ");
            string status = Console.ReadLine();

            // Vytvoření objednávky
            Objednavka novaObjednavka = new Objednavka(0, datumObjednavky, celkovaCena, status, null, null, null, null);

            // Uložení objednávky do databáze
            ObjednavkaDAO objednavkaDAO = new ObjednavkaDAO();
            objednavkaDAO.Save(novaObjednavka);

            Console.WriteLine("Objednávka byla úspěšně vložena.");
        }


        static void UpravitObjednavku()
        {
            Console.WriteLine("Úprava existující objednávky");

            // Získání ID objednávky od uživatele
            Console.Write("Zadejte ID objednávky k úpravě: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Neplatné ID. Zadejte platné celé číslo.");
                return;
            }

            // Načtení objednávky podle ID
            ObjednavkaDAO objednavkaDAO = new ObjednavkaDAO();
            Objednavka existujiciObjednavka = objednavkaDAO.GetByID(id);

            if (existujiciObjednavka == null)
            {
                Console.WriteLine("Objednávka s daným ID nebyla nalezena.");
                return;
            }

            // Zobrazení informací o stávající objednávce
            Console.WriteLine($"Stávající informace o objednávce:");
            Console.WriteLine($"Datum objednávky: {existujiciObjednavka.DatumObjednani}");
            Console.WriteLine($"Celková cena: {existujiciObjednavka.CelkovaCena}");
            Console.WriteLine($"Status: {existujiciObjednavka.Status}");

            // Zadání nových informací od uživatele
            Console.Write("Nové datum objednávky (YYYY-MM-DD): ");
            string datumObjednavkyStr = Console.ReadLine();
            DateTime datumObjednavky;
            if (!DateTime.TryParse(datumObjednavkyStr, out datumObjednavky))
            {
                Console.WriteLine("Neplatné datum. Úprava objednávky nebyla provedena.");
                return;
            }

            Console.Write("Nová celková cena: ");
            float novaCelkovaCena;
            if (!float.TryParse(Console.ReadLine(), out novaCelkovaCena))
            {
                Console.WriteLine("Neplatná cena. Úprava objednávky nebyla provedena.");
                return;
            }

            Console.Write("Nový status: ");
            string novyStatus = Console.ReadLine();

            // Aktualizace objednávky
            existujiciObjednavka.DatumObjednani = datumObjednavky;
            existujiciObjednavka.CelkovaCena = novaCelkovaCena;
            existujiciObjednavka.Status = novyStatus;

            // Uložení aktualizované objednávky do databáze
            objednavkaDAO.Save(existujiciObjednavka);

            Console.WriteLine("Objednávka byla úspěšně aktualizována.");
        }



        static void SmazatObjednavku()
        {
            Console.WriteLine("Mazání objednávky");

            // Získání ID objednávky od uživatele
            Console.Write("Zadejte ID objednávky k smazání: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Neplatné ID. Zadejte platné celé číslo.");
                return;
            }

            // Načtení objednávky podle ID
            ObjednavkaDAO objednavkaDAO = new ObjednavkaDAO();
            Objednavka objednavka = objednavkaDAO.GetByID(id);

            if (objednavka == null)
            {
                Console.WriteLine("Objednávka s daným ID nebyla nalezena.");
                return;
            }

            // Potvrzení od uživatele
            Console.WriteLine($"Opravdu chcete smazat objednávku s ID {objednavka.ID}? (ano/ne)");
            string potvrzeni = Console.ReadLine();

            if (potvrzeni.ToLower() == "ano")
            {
                // Smazání objednávky z databáze
                objednavkaDAO.Delete(objednavka);
                Console.WriteLine("Objednávka byla úspěšně smazána.");
            }
            else
            {
                Console.WriteLine("Mazání objednávky zrušeno.");
            }
        }


       
        static void KonfiguraceProgramu()
        {
           
            Console.WriteLine("Zadejte novou hodnotu klíče konfigurace:");
            string key = Console.ReadLine();
            Console.WriteLine("Zadejte hodnotu:");
            string value = Console.ReadLine();

            try
            {
                WriteSetting(key, value);
                Console.WriteLine("Konfigurace byla úspěšně aktualizována.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při aktualizaci konfigurace: {ex.Message}");
            }
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? throw new Exception($"Key '{key}' not found in the configuration file.");
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                throw new Exception("Error reading the configuration file.");
            }
        }

        static void WriteSetting(string key, string value)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings[key].Value = value;
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                throw new Exception("Error writing to the configuration file.");
            }
        }
    }

}
