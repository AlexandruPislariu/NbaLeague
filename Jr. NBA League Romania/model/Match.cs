using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Match : Entity<long>
    {
        public Match(Team first, Team second)
        {
            FirstTeam = first;
            SecondTeam = second;
        }

        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public DateTime Date { get; set; }
    }
}
