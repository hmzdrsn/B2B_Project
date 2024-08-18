using B2B_Project.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
    }
}
