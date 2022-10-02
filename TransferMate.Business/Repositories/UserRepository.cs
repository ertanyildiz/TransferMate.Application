using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferMate.Data.Repository;

namespace TransferMate.Business.Repositories
{
    public class UserRepository : IRepository<Data.Models.User>
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Data.Models.User Insert(Data.Models.User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Data.Models.User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Models.User> Select(int? key)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM dbo.[User]{(key > 0 ? " WHERE Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var userList = rows.Select(dr => new Data.Models.User
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UserName = dr["Username"]?.ToString() ?? ""
                    }).ToList();

                    return userList;
                }
            }
        }
    }
}
