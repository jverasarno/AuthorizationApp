using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsDoneByUser { get; set; }

        public DateTime UserUpdatedAt { get; set; }

        public int RequestedByUser { get; set; }

        public bool IsDoneByVPP { get; set; }

        public DateTime VPPUpdatedAt { get; set; }

        public int VPPUser { get; set; }

        public bool IsDoneByCI { get; set; }

        public DateTime CIUpdatedAt { get; set; }

        public int CIUser { get; set; }

        public bool IsDoneByVPTI { get; set; }

        public DateTime VPTIUpdatedAt { get; set; }

        public int VPTIUser { get; set; }

        public Int32 Incident { get; set; }

        public Int32 ChangeOrder { get; set; }

    }
}
