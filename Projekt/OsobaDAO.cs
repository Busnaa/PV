using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class OsobaDAO : IDAO<Osoba>
    {
        public void Delete(Osoba osoba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Osoby WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", osoba.ID));
                command.ExecuteNonQuery();
                osoba.ID = 0;
            }
        }

        public IEnumerable<Osoba> GetAll()
        { 
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Osoby", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Osoba osoba = new Osoba(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString()
                    );
                    yield return osoba;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Osoba? GetByID(int id)
        {
            Osoba? osoba = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Osoby WHERE id = @Id", connection))
            {
                // 2. define parameters used in command 
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                // 3. add new parameter to command object
                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    osoba = new Osoba(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString()
                        );
                }
                reader.Close();
            }
                
            return osoba;

        }

        public void Save(Osoba osoba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (osoba.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Osoby (jmeno, prijmeni) VALUES (@jmeno, @prijmeni)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@jmeno", osoba.Jmeno));
                    command.Parameters.Add(new SqlParameter("@prijmeni", osoba.Prijmeni));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    osoba.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Osoby SET jmeno = @jmeno, prijmeni = @prijmeni " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", osoba.ID));
                    command.Parameters.Add(new SqlParameter("@jmeno", osoba.Jmeno));
                    command.Parameters.Add(new SqlParameter("@prijmeni", osoba.Prijmeni));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Osoby", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
