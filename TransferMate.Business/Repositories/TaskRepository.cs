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
    public class TaskRepository : IRepository<Data.Models.Task>
    {
        private readonly string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int key)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var com = new SqlCommand("DELETE FROM dbo.TASK WHERE Id = @id", connection))
            {
                com.CommandType = CommandType.Text;

                com.Parameters.AddWithValue("@id", key);

                connection.Open();

                com.ExecuteNonQuery();

                return true;
            }
        }

        public bool Insert(Data.Models.Task entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO dbo.Task (CreatedDate, RequiredByDate, TaskDescription, TaskStatus, TaskType, AssignedTo, NextActionDate)" +
                    " VALUES (@CreatedDate,@RequiredByDate," +
                    " @TaskDescription, @TaskStatus, @TaskType, @AssignedTo, @NextActionDate)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
                    command.Parameters.AddWithValue("@RequiredByDate", entity.RequiredByDate);
                    command.Parameters.AddWithValue("@TaskDescription", entity.TaskDescription);
                    command.Parameters.AddWithValue("@TaskStatus", entity.TaskStatus);
                    command.Parameters.AddWithValue("@TaskType", entity.TaskType);
                    command.Parameters.AddWithValue("@AssignedTo", entity.AssignedTo);
                    command.Parameters.AddWithValue("@NextActionDate", entity.NextActionDate);

                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return true;
                }
            }
            
        }

        public IEnumerable<Data.Models.Task> Select(int? key = 0)
        {
            var list = new List<Data.Models.Task>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM dbo.Task WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var customers = rows.Select(dr => new Data.Models.Task
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        AssignedTo = Convert.ToInt32(dr["AssignedTo"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        NextActionDate = Convert.ToDateTime(dr["NextActionDate"]),
                        RequiredByDate = Convert.ToDateTime(dr["RequiredByDate"]),
                        TaskDescription = dr["TaskDescription"].ToString() ?? "",
                        TaskStatus = Convert.ToInt32(dr["TaskStatus"]),
                        TaskType = Convert.ToInt32(dr["TaskType"])
                    }).ToList();

                    return customers;
                }
            }
        }

        public bool Update(Data.Models.Task entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE dbo.Task SET CreatedDate = @CreatedDate, RequiredByDate = @RequiredByDate" +
                    ", TaskDescription = @TaskDescription, TaskStatus = @TaskStatus, TaskType = @TaskType," +
                    " AssignedTo = @AssignedTo, NextActionDate = @NextActionDate WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
                    command.Parameters.AddWithValue("@RequiredByDate", entity.RequiredByDate);
                    command.Parameters.AddWithValue("@TaskDescription", entity.TaskDescription);
                    command.Parameters.AddWithValue("@TaskStatus", entity.TaskStatus);
                    command.Parameters.AddWithValue("@TaskType", entity.TaskType);
                    command.Parameters.AddWithValue("@AssignedTo", entity.AssignedTo);
                    command.Parameters.AddWithValue("@NextActionDate", entity.NextActionDate);

                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return true;
                }
            }
        }
    }
}
