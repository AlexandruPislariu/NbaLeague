using System;
using System.Collections.Generic;
using System.Configuration;
using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using Jr._NBA_League_Romania.service;
using Npgsql;

namespace Jr._NBA_League_Romania
{
    class Program
    {
     
        static void Main(string[] args)
        {
            string tableTeams = ConfigurationManager.AppSettings["database.table.Teams"];
            string tablePlayers = ConfigurationManager.AppSettings["database.table.Players"];
            IRepository<long, Team> repoTeam = new TeamDatabaseRepository(tableTeams);
            IRepository<long, Player> repoPlayer = new PlayerDatabaseRepository(tablePlayers);
            TeamService servTeam = new TeamService(repoTeam);
            PlayerService servPlayer = new PlayerService(repoPlayer);


            Team team = new Team()
            { 
                ID = 1,
                Name = "Houston Rockets"
            };

            List<Player> playersTeam = servPlayer.allPlayersInATeam(team);
            foreach(var player in playersTeam)
                Console.WriteLine(player);

        }
    }
}
