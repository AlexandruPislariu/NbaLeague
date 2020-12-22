using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Match : Entity<long>
    {

        public long FirstTeam { get; set; }
        public long SecondTeam { get; set; }
        public DateTime Date { get; set; }

            
    }
}
