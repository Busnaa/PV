using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class ObchodDAO : IDAO<Obchod>
    {
        public void Delete(Obchod obchod)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Obchody WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", obchod.ID);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Obchod> GetAll()
        {
            List<Obchod> obchody = new List<Obchod>();

            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Obchody", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Obchod obchod = new Obchod(
                        Convert.ToInt32(reader["ID"]),
                        reader["Nazev"].ToString(),
                        reader["Adresa"].ToString(),
                        reader["Email"].ToString(),
                        reader["Telefon"].ToString()
                    );
                    obchody.Add(obchod);
                }
                reader.Close();
            }

            return obchody;
        }

        public Obchod GetByID(int id)
        {
            Obchod obchod = null;
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Obchody WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obchod = new Obchod(
                        Convert.ToInt32(reader["ID"]),
                        reader["Nazev"].ToString(),
                        reader["Adresa"].ToString(),
                        reader["Email"].ToString(),
                        reader["Telefon"].ToString()
                    );
                }
                reader.Close();
            }

            return obchod;
        }

        public void Save(Obchod obchod)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlCommand command = null;

            if (obchod.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Obchody (Nazev, Adresa, Email, Telefon) VALUES (@Nazev, @Adresa, @Email, @Telefon)", conn))
                {
                    command.Parameters.AddWithValue("@Nazev", obchod.Nazev);
                    command.Parameters.AddWithValue("@Adresa", obchod.Adresa);
                    command.Parameters.AddWithValue("@Email", obchod.Email);
                    command.Parameters.AddWithValue("@Telefon", obchod.Telefon);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Obchody SET Nazev = @Nazev, Adresa = @Adresa, Email = @Email, Telefon = @Telefon WHERE ID = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Nazev", obchod.Nazev);
                    command.Parameters.AddWithValue("@Adresa", obchod.Adresa);
                    command.Parameters.AddWithValue("@Email", obchod.Email);
                    command.Parameters.AddWithValue("@Telefon", obchod.Telefon);
                    command.Parameters.AddWithValue("@Id", obchod.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Obchody", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }

}
