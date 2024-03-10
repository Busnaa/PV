using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class PlatbaHotoveDAO : IDAO<PlatbaHotove>
    {
        public void Delete(PlatbaHotove platba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM PlatbyHotove WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", platba.ID);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PlatbaHotove> GetAll()
        {
            List<PlatbaHotove> platby = new List<PlatbaHotove>();

            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM PlatbyHotove", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PlatbaHotove platba = new PlatbaHotove(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToSingle(reader["Castka"]),
                        Convert.ToDateTime(reader["Datum"])
                    );
                    platby.Add(platba);
                }
                reader.Close();
            }

            return platby;
        }

        public PlatbaHotove GetByID(int id)
        {
            PlatbaHotove platba = null;
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM PlatbyHotove WHERE ID = @Id", conn))
            {
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    platba = new PlatbaHotove(
                        Convert.ToInt32(reader["ID"]),
                        Convert.ToSingle(reader["Castka"]),
                        Convert.ToDateTime(reader["Datum"])
                    );
                }
                reader.Close();
            }

            return platba;
        }

        public void Save(PlatbaHotove platba)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlCommand command = null;

            if (platba.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO PlatbyHotove (Castka, Datum) VALUES (@Castka, @Datum)", conn))
                {
                    command.Parameters.AddWithValue("@Castka", platba.Castka);
                    command.Parameters.AddWithValue("@Datum", platba.Datum);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE PlatbyHotove SET Castka = @Castka, Datum = @Datum WHERE ID = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Castka", platba.Castka);
                    command.Parameters.AddWithValue("@Datum", platba.Datum);
                    command.Parameters.AddWithValue("@Id", platba.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM PlatbyHotove", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }

}
