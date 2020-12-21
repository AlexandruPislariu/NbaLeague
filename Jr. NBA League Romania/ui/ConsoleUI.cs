using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using Jr._NBA_League_Romania.service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Jr._NBA_League_Romania.ui
{
    class ConsoleUI
    {
        MainService serviceApp;
        private void setUpService()
        {
            string tableTeams = ConfigurationManager.AppSettings["database.table.Teams"];
            string tablePlayers = ConfigurationManager.AppSettings["database.table.Players"];
            string tableMatches = ConfigurationManager.AppSettings["database.table.Matches"];
            string tableActivity = ConfigurationManager.AppSettings["database.table.Activity"];
            IRepository<long, Team> repoTeam = new TeamDatabaseRepository(tableTeams);
            IRepository<long, Player> repoPlayer = new PlayerDatabaseRepository(tablePlayers);
            IRepository<long, Match> repoMatch = new MatchDatabaseRepository(tableMatches);
            IRepository<Tuple<long, long>, ActivePlayer> repoActivity = new ActivityDatabaseRepository(tableActivity);
            serviceApp = new MainService(repoActivity, repoMatch, repoPlayer, repoTeam);
        }

        public ConsoleUI()
        {
            setUpService();
        }

        public void Run()
        {
            string command;
            while(true)
            {
                command = Console.ReadLine();

                string commandName = command.Split(' ')[0];
                string[] parameters = command.Split(' ');

                if(commandName.Equals("allPlayersTeam"))
                {
                    long teamId = long.Parse(parameters[1]);
                    Team team = new Team()
                    {
                        ID = teamId
                    };
                    List<Player> players = serviceApp.AllPlayersInATeam(team);
                    foreach(var player in players)
                        Console.WriteLine(player);
                }
                if(commandName.Equals("allActivePlayersTeam"))
                {
                    long teamId = long.Parse(parameters[1]);
                    long matchId = long.Parse(parameters[2]);
                    Team team = new Team()
                    {
                        ID = teamId
                    };
                    Match match = new Match()
                    {
                        ID = matchId
                    };
                    List<Player> allActivePlayers = serviceApp.AllActivePlayersInTeamMatch(team, match);

                    foreach(var activePlayer in allActivePlayers)
                        Console.WriteLine(activePlayer);
                }
            }
        }
    }
}
