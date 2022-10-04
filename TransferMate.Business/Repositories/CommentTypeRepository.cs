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
    public class CommentTypeRepository : IRepository<Data.Models.CommentType>
    {
        private readonly string _connectionString;
        public CommentTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Data.Models.CommentType Insert(Data.Models.CommentType entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Models.CommentType> Select(int? key = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM dbo.CommentType{(key > 0 ? " WHERE Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var commentTypes = rows.Select(dr => new Data.Models.CommentType
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TypeName = dr["TypeName"].ToString() ?? ""
                    }).ToList();

                    return commentTypes;
                }
            }
        }

        public bool Update(Data.Models.CommentType entity)
        {
            throw new NotImplementedException();
        }
    }
}
