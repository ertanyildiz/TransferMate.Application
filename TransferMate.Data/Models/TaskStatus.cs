using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferMate.Domain.Entity;

namespace TransferMate.Data.Models
{
    public class TaskStatus : BaseEntity<int>
    {
        public string StatusName { get; set; }
    }
}
