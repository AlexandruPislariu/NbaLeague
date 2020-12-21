using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Team : Entity<long>
    {
        public Team(long id, string name):base(id)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}
