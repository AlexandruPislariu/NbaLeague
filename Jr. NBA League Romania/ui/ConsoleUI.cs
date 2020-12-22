using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using Jr._NBA_League_Romania.service;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Jr._NBA_League_Romania.ui
{
    class ConsoleUI
    {
        MainService serviceApp;
        //const string[] features = [ "scoreMatch", "matchesPeriod" ];
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

        private void ShowCommands()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("playersTeam       -> show all players in a team");
            Console.WriteLine("activePlayersTeam -> show all active players from a team in one match");
            Console.WriteLine("matchesPeriod     -> show all matches between two dates");
            Console.WriteLine("scoreMatch        -> show match score");
        }
        public void Run()
        {

            ShowCommands();
            string command;
            while(true)
            {
                command = Console.ReadLine();

                string commandName = command.Split(' ')[0];
                string[] parameters = command.Split(' ');

                if(commandName.Equals("playersTeam"))
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
                if(commandName.Equals("activePlayersTeam"))
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
                if(commandName.Equals("matchesPeriod"))
                {
                    string startPeriod = parameters[1];
                    string endPeriod = parameters[2];

                    List<String> matchesPeriod = serviceApp.AllMatchesInPeriod(DateTime.Parse(startPeriod), DateTime.Parse(endPeriod));

                    foreach(var match in matchesPeriod)
                        Console.WriteLine(match);
                }
                if(commandName.Equals("scoreMatch"))
                {
                    long idMatch = long.Parse(parameters[1]);
                    Match match = new Match()
                    {
                        ID = idMatch
                    };
                    
                    Console.WriteLine(serviceApp.ScoreMatch(match));
                }
            }
        }
    }
}
