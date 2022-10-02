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
    public class TypeRepository : IRepository<Data.Models.TaskType>
    {
        private readonly string _connectionString;
        public TypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Data.Models.TaskType Insert(Data.Models.TaskType entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Data.Models.TaskType entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Models.TaskType> Select(int? key)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM dbo.TaskType{(key > 0 ? " WHERE Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var statusList = rows.Select(dr => new Data.Models.TaskType
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TypeName = dr["TypeName"]?.ToString() ?? ""
                    }).ToList();

                    return statusList;
                }
            }
        }
    }
}
