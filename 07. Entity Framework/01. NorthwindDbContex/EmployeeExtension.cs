using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.NorthwindDbContex
{
    public partial class Employee
    {
        private EntitySet<Territory> territory;

        public EntitySet<Territory> Territory
        {
            get
            {
                this.territory = new EntitySet<Territory>();
                var territories = this.Territories;
                this.territory.AddRange(territories);
                return this.territory;
            }
        }
        
    }
}
