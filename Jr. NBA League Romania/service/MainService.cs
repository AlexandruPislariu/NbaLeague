using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jr._NBA_League_Romania.service
{
    class MainService
    {
        IRepository<Tuple<long, long>, ActivePlayer> repoActivity;
        IRepository<long, Match> repoMatch;
        IRepository<long, Player> repoPlayer;
        IRepository<long, Team> repoTeam;

        public MainService(IRepository<Tuple<long, long>, ActivePlayer> repoActivity, IRepository<long, Match> repoMatch, IRepository<long, Player> repoPlayer, IRepository<long, Team> repoTeam)
        {
            this.repoActivity = repoActivity;
            this.repoMatch = repoMatch;
            this.repoPlayer = repoPlayer;
            this.repoTeam = repoTeam;
        }

        public List<Player> AllPlayersInATeam(Team team)
        {
            return repoPlayer.FindAll().ToList()
                        .Where(player => player.TeamId == team.ID)
                        .ToList();
        }

        public List<Player> AllActivePlayersInTeamMatch(Team team, Match match)
        {
            List<ActivePlayer> allPlayersInMatch = repoActivity.FindAll().ToList()
                                                .Where(activePlayer => activePlayer.ID.Item2 == match.ID)
                                                .ToList();

            List<Player> allPlayersFromTeam = allPlayersInMatch
                .Select(activePlayer => repoPlayer.FindOne(activePlayer.ID.Item1))
                .Where(player => player.TeamId == team.ID)
                .ToList();

            return allPlayersFromTeam;
        }

        public List<Match> AllMatchesInPeriod(DateTime startPeriod, DateTime endPeriod)
        {
            List<Match> allMatchesInPeriod = repoMatch.FindAll().ToList()
                                                       .Where(match => match.Date.CompareTo(startPeriod) >= 0 && match.Date.CompareTo(endPeriod) <= 0)
                                                       .ToList();

            return allMatchesInPeriod;
        }

        public string ScoreMatch(Match match)
        {
            long firstTeamId = match.FirstTeam;
            Team firstTeam = repoTeam.FindOne(firstTeamId);
            long secondTeamId = match.SecondTeam;
            Team secondTeam = repoTeam.FindOne(secondTeamId);

            long firstTeamScore = repoActivity.FindAll().ToList()
                                            .Where(activePlayer => activePlayer.ID.Item2 == match.ID)
                                            .Where(activePlayer =>
                                            {
                                                long idPlayer = activePlayer.ID.Item1;
                                                Player player = repoPlayer.FindOne(idPlayer);
                                                return player.TeamId == firstTeamId;
                                            })
                                            .Sum(activePlayer => activePlayer.Points);

            long secondTeamScore = repoActivity.FindAll().ToList()
                                        .Where(activePlayer => activePlayer.ID.Item2 == match.ID)
                                        .Where(activePlayer =>
                                            {
                                                long idPlayer = activePlayer.ID.Item1;
                                                Player player = repoPlayer.FindOne(idPlayer);
                                                return player.TeamId == secondTeamId;
                                            })
                                         .Sum(activePlayer => activePlayer.Points);


            return firstTeam.ToString() + " " + firstTeamScore.ToString() + " - " + secondTeamScore.ToString() + " " + secondTeam.ToString();
        }
    }
}
