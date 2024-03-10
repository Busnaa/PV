using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class PlatbaKartouDAO : IDAO<PlatbaKartou>
    {
        public void Delete(PlatbaKartou platba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM PlatbyKartou WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", platba.ID);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PlatbaKartou> GetAll()
        {
            List<PlatbaKartou> platby = new List<PlatbaKartou>();

            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM PlatbyKartou", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PlatbaKartou platba = new PlatbaKartou(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToSingle(reader["Castka"]),
                        reader["CisloKarty"].ToString(),
                        reader["TypKarty"].ToString(),
                        Convert.ToDateTime(reader["PlatnostKarty"])
                    );
                    platby.Add(platba);
                }
                reader.Close();
            }

            return platby;
        }

        public PlatbaKartou GetByID(int id)
        {
            PlatbaKartou platba = null;
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM PlatbyKartou WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    platba = new PlatbaKartou(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToSingle(reader["Castka"]),
                        reader["CisloKarty"].ToString(),
                        reader["TypKarty"].ToString(),
                        Convert.ToDateTime(reader["PlatnostKarty"])
                    );
                }
                reader.Close();
            }

            return platba;
        }

        public void Save(PlatbaKartou platba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlCommand command = null;

            if (platba.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO PlatbyKartou (Castka, CisloKarty, TypKarty, PlatnostKarty) VALUES (@Castka, @CisloKarty, @TypKarty, @PlatnostKarty)", conn))
                {
                    command.Parameters.AddWithValue("@Castka", platba.Castka);
                    command.Parameters.AddWithValue("@CisloKarty", platba.CisloKarty);
                    command.Parameters.AddWithValue("@TypKarty", platba.TypKarty);
                    command.Parameters.AddWithValue("@PlatnostKarty", platba.PlatnostKarty);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE PlatbyKartou SET Castka = @Castka, CisloKarty = @CisloKarty, TypKarty = @TypKarty, PlatnostKarty = @PlatnostKarty WHERE ID = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Castka", platba.Castka);
                    command.Parameters.AddWithValue("@CisloKarty", platba.CisloKarty);
                    command.Parameters.AddWithValue("@TypKarty", platba.TypKarty);
                    command.Parameters.AddWithValue("@PlatnostKarty", platba.PlatnostKarty);
                    command.Parameters.AddWithValue("@Id", platba.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM PlatbyKartou", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }

}
