using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Player : Student
    {
        public Player(string name, string school, long teamId) : base(name, school)
        {
            TeamId = teamId;
        }
        public long TeamId { get; set; }

        public override string ToString()
        {
            return Name + " " + School;
        }
    }
}
