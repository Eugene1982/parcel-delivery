using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHost.Domain
{
    public class Department
    {
        public string Name { get; set; }
        public int? WeightMin { get; set; }
        public int? WeightMax { get; set; }
        public int? PriceStart { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
