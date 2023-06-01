using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Customer
{
    public class CustomerDTO : BaseEntity
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

    }
}
