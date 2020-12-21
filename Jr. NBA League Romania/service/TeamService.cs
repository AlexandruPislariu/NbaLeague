using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jr._NBA_League_Romania.service
{
    class TeamService
    {
        private IRepository<long, Team> repoTeam;

        public TeamService(IRepository<long, Team> repoTeam)
        {
            this.repoTeam = repoTeam;
        }

        public List<Team> FindAllTeams()
        {
            return this.repoTeam.FindAll().ToList();
        }

        public Team addTeam(String name)
        {
            Team team = new Team
            {
                Name = name
            };
            return this.repoTeam.Save(team);
        }
    }
}
