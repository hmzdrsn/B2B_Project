using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Common.Models
{
    public class HandlerResponse <T>
    {
        public string? Message { get; set; }
        public string? Status { get; set; }
        public T? Data { get; set; }
    }
}
