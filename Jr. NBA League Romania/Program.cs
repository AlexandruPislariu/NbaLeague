using System;
using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using Npgsql;

namespace Jr._NBA_League_Romania
{
    class Program
    {
     
        static void Main(string[] args)
        {
            IRepository<long, Team> repoTeam = new TeamDatabaseRepository();
            
        }
    }
}
