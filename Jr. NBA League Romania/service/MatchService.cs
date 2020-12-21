using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jr._NBA_League_Romania.service
{
    class MatchService
    {
        IRepository<long, Match> repoMatch;

        public MatchService(IRepository<long, Match> repoMatch)
        {
            this.repoMatch = repoMatch;
        }

        public Match addMatch(long firstTeam, long secondTeam, DateTime date)
        {
            Match match = new Match(firstTeam, secondTeam)
            {
                Date = date
            };
            return repoMatch.Save(match);
        }

        public List<Match> allMatchesInPeriod(DateTime startPeriod, DateTime endPeriod)
        {
            List<Match> allMatches = repoMatch.FindAll().ToList();
            return allMatches
                    .Where(match => match.Date.CompareTo(startPeriod) >= 0 && match.Date.CompareTo(endPeriod) <= 0)
                    .ToList();
        }
    }
}
