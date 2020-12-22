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
            Match foundMatch = repoMatch.FindOne(match.ID);
            Team foundTeam = repoTeam.FindOne(team.ID);
            List<ActivePlayer> allPlayersInMatch = repoActivity.FindAll().ToList()
                                                .Where(activePlayer => activePlayer.ID.Item2 == foundMatch.ID)
                                                .ToList();

            List<Player> allPlayersFromTeam = allPlayersInMatch
                .Select(activePlayer => repoPlayer.FindOne(activePlayer.ID.Item1))
                .Where(player => player.TeamId == foundTeam.ID)
                .ToList();

            return allPlayersFromTeam;
        }

        public List<String> AllMatchesInPeriod(DateTime startPeriod, DateTime endPeriod)
        {
            List<String> allMatchesInPeriod = repoMatch.FindAll().ToList()
                                                       .Where(match => match.Date.CompareTo(startPeriod) >= 0 && match.Date.CompareTo(endPeriod) <= 0)
                                                       .Select(match => new { MatchDescription = repoTeam.FindOne(match.FirstTeam).Name + " - " + repoTeam.FindOne(match.SecondTeam).Name})
                                                       .Select(matchDesc => matchDesc.MatchDescription)
                                                       .ToList();

            return allMatchesInPeriod;
        }

        public string ScoreMatch(Match match)
        {
            Match foundMatch = repoMatch.FindOne(match.ID);
            long firstTeamId = foundMatch.FirstTeam;
            Team firstTeam = repoTeam.FindOne(firstTeamId);
            long secondTeamId = foundMatch.SecondTeam;
            Team secondTeam = repoTeam.FindOne(secondTeamId);

            long firstTeamScore = repoActivity.FindAll().ToList()
                                            .Where(activePlayer => activePlayer.ID.Item2 == foundMatch.ID)
                                            .Where(activePlayer =>
                                            {
                                                long idPlayer = activePlayer.ID.Item1;
                                                Player player = repoPlayer.FindOne(idPlayer);
                                                return player.TeamId == firstTeamId;
                                            })
                                            .Sum(activePlayer => activePlayer.Points);

            long secondTeamScore = repoActivity.FindAll().ToList()
                                        .Where(activePlayer => activePlayer.ID.Item2 == foundMatch.ID)
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
