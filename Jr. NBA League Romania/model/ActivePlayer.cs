using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    enum Type { Reserve, Participant};
    class ActivePlayer : Entity<Tuple<long, long>>
    {
        public long Points { get; set; }

        public Type TypePlayer { get; set; }
        

    }
}
