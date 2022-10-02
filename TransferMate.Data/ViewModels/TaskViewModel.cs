using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferMate.Data.ViewModels
{
    public class TaskViewModel
    {
        public Data.Models.Task Task { get; set; }
        public Data.Models.TaskStatus TaskStatus { get; set; }
        public Data.Models.TaskType TaskType { get; set; }
        public Data.Models.User User { get; set; }
    }
}
