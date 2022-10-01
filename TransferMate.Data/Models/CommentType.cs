using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferMate.Domain.Entity;

namespace TransferMate.Data.Models
{
    public class CommentType : BaseEntity<int>
    {
        public string TypeName { get; set; }
    }
}
