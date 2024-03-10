using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class ObjednavkaDAO : IDAO<Objednavka>
    {
        public void Delete(Objednavka objednavka)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Objednavky WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", objednavka.ID);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Objednavka> GetAll()
        {
            List<Objednavka> objednavky = new List<Objednavka>();

            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Objednavky", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Objednavka objednavka = new Objednavka(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToDateTime(reader["DatumObjednani"]),
                        Convert.ToSingle(reader["CelkovaCena"]),
                        reader["Status"].ToString(),
                        null, // Platba kartou
                        null, // Platba hotove
                        null, // Zakaznik
                        null  // Obchod
                    );
                    objednavky.Add(objednavka);
                }
                reader.Close();
            }

            return objednavky;
        }

        public Objednavka GetByID(int id)
        {
            Objednavka objednavka = null;
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Objednavky WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    objednavka = new Objednavka(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToDateTime(reader["DatumObjednani"]),
                        Convert.ToSingle(reader["CelkovaCena"]),
                        reader["Status"].ToString(),
                        null, // Platba kartou
                        null, // Platba hotove
                        null, // Zakaznik
                        null  // Obchod
                    );
                }
                reader.Close();
            }

            return objednavka;
        }

        public void Save(Objednavka objednavka)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlCommand command = null;

            if (objednavka.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Objednavky (DatumObjednani, CelkovaCena, Status) VALUES (@DatumObjednani, @CelkovaCena, @Status); SELECT SCOPE_IDENTITY()", conn))
                {
                    command.Parameters.AddWithValue("@DatumObjednani", objednavka.DatumObjednani);
                    command.Parameters.AddWithValue("@CelkovaCena", objednavka.CelkovaCena);
                    command.Parameters.AddWithValue("@Status", objednavka.Status);
                    objednavka.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Objednavky SET DatumObjednani = @DatumObjednani, CelkovaCena = @CelkovaCena, Status = @Status WHERE ID = @Id", conn))
                {
                    command.Parameters.AddWithValue("@DatumObjednani", objednavka.DatumObjednani);
                    command.Parameters.AddWithValue("@CelkovaCena", objednavka.CelkovaCena);
                    command.Parameters.AddWithValue("@Status", objednavka.Status);
                    command.Parameters.AddWithValue("@Id", objednavka.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Objednavky", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }

}
