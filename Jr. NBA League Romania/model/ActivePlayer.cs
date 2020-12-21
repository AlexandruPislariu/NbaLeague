using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    enum Type { Reserve, Participant};
    class ActivePlayer
    {
        public long IdPlayer { get; set; }
        public long IdMatch { get; set; }

        public int Points { get; set; }

        public Type TypePlayer { get; set; }
        

    }
}
