using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.model
{
    class Student : Entity<long>
    {
        public Student(string name, string school)
        {
            Name = name;
            School = school;
        }

        public string Name { get; set; }
        public string School { get; set; }
    }
}
