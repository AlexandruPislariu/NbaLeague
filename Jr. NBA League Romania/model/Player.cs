using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Player : Student
    {
        public Player(long id, string name, string school, Team team) : base(id, name, school)
        {
            Team = team;
        }
        public Team Team { get; set; }
    }
}
