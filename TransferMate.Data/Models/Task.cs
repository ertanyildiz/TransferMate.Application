using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferMate.Domain.Entity;

namespace TransferMate.Data.Models
{
    public class Task : BaseEntity<int>
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime RequiredByDate { get; set; }
        public string TaskDescription { get; set; }
        public int TaskStatus { get; set; }
        public int TaskType { get; set; }
        public int AssignedTo { get; set; }
        public DateTime NextActionDate { get; set; }
    }
}
