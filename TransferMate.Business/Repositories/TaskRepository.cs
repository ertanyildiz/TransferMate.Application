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
            using (var com = new SqlCommand("DELETE FROM dbo.Task WHERE Id = @id", connection))
            {
                com.CommandType = CommandType.Text;

                com.Parameters.AddWithValue("@id", key);

                connection.Open();

                com.ExecuteNonQuery();

                return true;
            }
        }

        public Data.Models.Task Insert(Data.Models.Task entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO dbo.Task (Title, CreatedDate, RequiredByDate, TaskDescription, TaskStatus, TaskType, AssignedTo, NextActionDate)" +
                    " VALUES (@Title, @CreatedDate,@RequiredByDate," +
                    " @TaskDescription, @TaskStatus, @TaskType, @AssignedTo, @NextActionDate); SELECT SCOPE_IDENTITY() ";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", entity.Title);
                    command.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
                    command.Parameters.AddWithValue("@RequiredByDate", entity.RequiredByDate);
                    command.Parameters.AddWithValue("@TaskDescription", entity.TaskDescription);
                    command.Parameters.AddWithValue("@TaskStatus", entity.TaskStatus);
                    command.Parameters.AddWithValue("@TaskType", entity.TaskType);
                    command.Parameters.AddWithValue("@AssignedTo", entity.AssignedTo);
                    command.Parameters.AddWithValue("@NextActionDate", entity.NextActionDate);

                    connection.Open();

                    var result = command.ExecuteScalar();
                    entity.Id = Convert.ToInt32(result);

                    return entity;
                }
            }
            
        }

        public IEnumerable<Data.Models.Task> Select(int? key = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM dbo.Task{(key > 0 ? " WHERE Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var tasks = rows.Select(dr => new Data.Models.Task
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = dr["Title"]?.ToString() ?? "",
                        AssignedTo = Convert.ToInt32(dr["AssignedTo"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        NextActionDate = Convert.ToDateTime(dr["NextActionDate"]),
                        RequiredByDate = Convert.ToDateTime(dr["RequiredByDate"]),
                        TaskDescription = dr["TaskDescription"]?.ToString() ?? "",
                        TaskStatus = Convert.ToInt32(dr["TaskStatus"]),
                        TaskType = Convert.ToInt32(dr["TaskType"])
                    }).ToList();

                    return tasks;
                }
            }
        }

        public bool Update(Data.Models.Task entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE dbo.Task SET Title = @Title, CreatedDate = @CreatedDate, RequiredByDate = @RequiredByDate" +
                    ", TaskDescription = @TaskDescription, TaskStatus = @TaskStatus, TaskType = @TaskType," +
                    " AssignedTo = @AssignedTo, NextActionDate = @NextActionDate WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Title", entity.Title);
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

        public IEnumerable<Data.ViewModels.TaskViewModel> SelectView(int? key = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT dbo.Task.TaskStatus, dbo.Task.AssignedTo, dbo.Task.NextActionDate, dbo.Task.Id AS Id, dbo.Task.Title, dbo.Task.CreatedDate, dbo.Task.RequiredByDate, dbo.Task.TaskDescription, dbo.Task.TaskType, " +
                    $"dbo.TaskStatus.Id as TaskId, dbo.TaskStatus.StatusName, " +
                    $"dbo.TaskType.Id as TypeId, dbo.TaskType.TypeName as TypeName, " +
                    $"dbo.[User].Id as UserId, dbo.[User].Username as UserName FROM dbo.Task " +
                    $"LEFT JOIN dbo.TaskStatus ON dbo.Task.TaskStatus = dbo.TaskStatus.Id " +
                    $"LEFT JOIN dbo.TaskType ON dbo.Task.TaskType = dbo.TaskType.Id " +
                    $"LEFT JOIN dbo.[User] ON dbo.Task.AssignedTo = dbo.[User].Id" +
                    $"{ (key > 0 ? " WHERE dbo.Task.Id = @Id" : "")}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", key);

                    connection.Open();
                    var sqlDataReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(sqlDataReader);
                    IEnumerable<DataRow> rows = dt.AsEnumerable();
                    var tasks = rows.Select(dr => new Data.ViewModels.TaskViewModel
                    {
                        Task = new Data.Models.Task
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Title = dr["Title"]?.ToString() ?? "",
                            AssignedTo = Convert.ToInt32(dr["AssignedTo"]),
                            CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                            NextActionDate = Convert.ToDateTime(dr["NextActionDate"]),
                            RequiredByDate = Convert.ToDateTime(dr["RequiredByDate"]),
                            TaskDescription = dr["TaskDescription"]?.ToString() ?? "",
                            TaskStatus = Convert.ToInt32(dr["TaskStatus"]),
                            TaskType = Convert.ToInt32(dr["TaskType"])
                        },
                        TaskStatus = new Data.Models.TaskStatus
                        {
                            Id = Convert.ToInt32(dr["TaskStatus"]),
                            StatusName = dr["StatusName"]?.ToString() ?? ""
                        },
                        TaskType = new Data.Models.TaskType
                        {
                            Id = Convert.ToInt32(dr["TaskType"]),
                            TypeName = dr["TypeName"]?.ToString() ?? ""
                        },
                        User = new Data.Models.User
                        {
                            Id = Convert.ToInt32(dr["AssignedTo"]),
                            UserName = dr["Username"]?.ToString() ?? ""
                        }
                    }).ToList();

                    return tasks;
                }
            }
        }
    }
}
