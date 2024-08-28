using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.OrderStatus.Queries.GetAll
{
    public class GetAllOrderStatusQueryResponse
    {
        public List<StatusResponse> Status { get; set; }

    }

    public sealed class StatusResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
