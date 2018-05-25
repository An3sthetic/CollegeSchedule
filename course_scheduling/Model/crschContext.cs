using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_scheduling.Model
{
    public class crschContext
    {

            public string ConnectionString { get; set; }

            public crschContext(string connectionString)
            {
                this.ConnectionString = connectionString;
            }

            private MySqlConnection GetConnection()
            {
                return new MySqlConnection(ConnectionString);
            }

            public List<Login> GetAllFilms()
            {
                List<Login> list = new List<Login>();

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM login", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Login()
                            {
                                User = reader.GetString("userid"),
                                Password = reader.GetString("pass"),
                                type = reader.GetString("type"),
                                
                            });
                        }
                    }
                }

                return list;

    }
}
}
