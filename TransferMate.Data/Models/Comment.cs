using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferMate.Domain.Entity;

namespace TransferMate.Data.Models
{
    public class Comment : BaseEntity<int>
    {
        public DateTime CreatedDate { get; set; }
        public string CommentContent { get; set; }
        public int CommentType { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
