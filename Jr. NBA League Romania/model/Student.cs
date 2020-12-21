using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Student : Entity<long>
    {
        public Student(long id, string name, string school) : base(id)
        {
            Name = name;
            School = school;
        }

        public string Name { get; set; }
        public string School { get; set; }
    }
}
