using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferMate.Data.ViewModels
{
    public class CommentViewModel
    {
        public Data.Models.Comment Comment { get; set; }
        public Data.Models.CommentType CommentType { get; set; }
    }
}
