
using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Projekt;

public class CSVImporter
{
    public void ImportovatDataZCSV(string filePath)
    {
        try
        {
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true })) 
            {
                var records = csv.GetRecords<ObjednavkaCSV>(); 

              
                ObjednavkaDAO objednavkaDAO = new ObjednavkaDAO();
                foreach (var record in records)
                {
                
                    Objednavka novaObjednavka = new Objednavka(0, record.DatumObjednavky, record.CelkovaCena, record.Status, null, null, null, null);

                  
                    objednavkaDAO.Save(novaObjednavka);
                }
            }

            Console.WriteLine("Data byla úspěšně importována z CSV souboru.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při importu dat z CSV souboru: {ex.Message}");
        }
    }
}
