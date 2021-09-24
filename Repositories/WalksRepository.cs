using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId ,wk.Name as walkerName, d.Name as dogName, o.Name AS ownerName, o.Id AS ownerId
                      FROM Walks w Left JOIN Walker wk  ON w.WalkerId = wk.Id Left Join Dog d ON w.DogId = d.Id Left JOIN Owner o ON o.Id = d.OwnerId
                    ";


                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();

                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("WalkerId")))
                        {
                            walk.Walker = new Walker
                            {
                                Name = reader.GetString(reader.GetOrdinal("walkerName"))
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("DogId")))
                        {
                            walk.Dog = new Dog()
                            {
                                Name = reader.GetString(reader.GetOrdinal("dogName"))
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ownerName")))
                        {
                            walk.Owner = new Owner()
                            {
                                Name = reader.GetString(reader.GetOrdinal("ownerName"))
                            };
                        }

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }
    }
}