using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class ZakaznikDAO : IDAO<Zakaznik>
    {
        public void Delete(Zakaznik zakaznik)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Zakaznici WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", zakaznik.ID));
                command.ExecuteNonQuery();
                zakaznik.ID = 0;
            }
        }

        public IEnumerable<Zakaznik> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Zakaznici", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Zakaznik zakaznik = new Zakaznik(
                        Convert.ToInt32(reader["ID"]),
                        reader["Jmeno"].ToString(),
                        reader["Prijmeni"].ToString(),
                        reader["Email"].ToString(),
                        Convert.ToDateTime(reader["DatumRegistrace"])
                    );
                    yield return zakaznik;
                }
                reader.Close();
            }
        }

        public Zakaznik GetByID(int id)
        {
            Zakaznik zakaznik = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Zakaznici WHERE ID = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    zakaznik = new Zakaznik(
                        Convert.ToInt32(reader["ID"]),
                        reader["Jmeno"].ToString(),
                        reader["Prijmeni"].ToString(),
                        reader["Email"].ToString(),
                        Convert.ToDateTime(reader["DatumRegistrace"])
                    );
                }
                reader.Close();
            }

            return zakaznik;
        }

        public void Save(Zakaznik zakaznik)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlCommand command = null;

            if (zakaznik.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Zakaznici (Jmeno, Prijmeni, Email, DatumRegistrace) VALUES (@Jmeno, @Prijmeni, @Email, @DatumRegistrace)", conn))
                {
                    command.Parameters.AddWithValue("@Jmeno", zakaznik.Jmeno);
                    command.Parameters.AddWithValue("@Prijmeni", zakaznik.Prijmeni);
                    command.Parameters.AddWithValue("@Email", zakaznik.Email);
                    command.Parameters.AddWithValue("@DatumRegistrace", zakaznik.DatumRegistrace);
                    command.ExecuteNonQuery();

                    // Získání ID posledně vloženého záznamu
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    zakaznik.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Zakaznici SET Jmeno = @Jmeno, Prijmeni = @Prijmeni, Email = @Email, DatumRegistrace = @DatumRegistrace WHERE ID = @ID", conn))
                {
                    command.Parameters.AddWithValue("@Jmeno", zakaznik.Jmeno);
                    command.Parameters.AddWithValue("@Prijmeni", zakaznik.Prijmeni);
                    command.Parameters.AddWithValue("@Email", zakaznik.Email);
                    command.Parameters.AddWithValue("@DatumRegistrace", zakaznik.DatumRegistrace);
                    command.Parameters.AddWithValue("@ID", zakaznik.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Zakaznici", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }

}
