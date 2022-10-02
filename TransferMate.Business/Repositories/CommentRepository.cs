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
    public class CommentRepository : IRepository<Data.Models.Comment>
    {
        private readonly string _connectionString;
        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int key)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var com = new SqlCommand("DELETE FROM dbo.Comment WHERE Id = @id", connection))
            {
                com.CommandType = CommandType.Text;

                com.Parameters.AddWithValue("@id", key);

                connection.Open();

                com.ExecuteNonQuery();

                return true;
            }
        }

        public Data.Models.Comment Insert(Data.Models.Comment entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO dbo.Comment (CreatedDate, CommentContent, CommentType, ReminderDate," +
                    " Task)" +
                    " VALUES (@CreatedDate,@CommentContent," +
                    " @CommentType, @ReminderDate, @Task); SELECT SCOPE_IDENTITY() ";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
                    command.Parameters.AddWithValue("@CommentContent", entity.CommentContent);
                    command.Parameters.AddWithValue("@CommentType", entity.CommentType);
                    command.Parameters.AddWithValue("@ReminderDate", entity.ReminderDate);
                    command.Parameters.AddWithValue("@Task", entity.Task);

                    connection.Open();

                    var result = command.ExecuteScalar();
                    entity.Id = Convert.ToInt32(result);

                    return entity;
                }
            }
            
        }

        public IEnumerable<Data.Models.Comment> Select(int? key = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM dbo.Comment{(key > 0 ? " WHERE Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var comments = rows.Select(dr => new Data.Models.Comment
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        CommentContent = dr["CommentContent"].ToString() ?? "",
                        CommentType = Convert.ToInt32(dr["CommentType"]),
                        ReminderDate = Convert.ToDateTime(dr["ReminderDate"]),
                        Task = Convert.ToInt32(dr["Task"])
                    }).ToList();

                    return comments;
                }
            }
        }

        public bool Update(Data.Models.Comment entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE dbo.Comment SET CreatedDate = @CreatedDate, CommentContent = @CommentContent" +
                    ", CommentType = @CommentType, ReminderDate = @ReminderDate, Task = @Task WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
                    command.Parameters.AddWithValue("@CommentContent", entity.CommentContent);
                    command.Parameters.AddWithValue("@CommentType", entity.CommentType);
                    command.Parameters.AddWithValue("@ReminderDate", entity.ReminderDate);
                    command.Parameters.AddWithValue("@Task", entity.Task);

                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return true;
                }
            }
        }
    }
}
