using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.Domain
{
    class Entity<TID>
    {

        public Entity(TID id)
        {
            ID = id;
        }
        public TID ID { get; set; }
    }
}
