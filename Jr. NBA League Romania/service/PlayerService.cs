using Jr._NBA_League_Romania.model;
using Jr._NBA_League_Romania.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jr._NBA_League_Romania.service
{
    class PlayerService
    { 
        IRepository<long, Player> repoPlayer;

        public PlayerService(IRepository<long, Player> repoPlayer)
        {
            this.repoPlayer = repoPlayer;
        }

        public Player addPlayer(String name, String school, long teamId)
        {
            Player player = new Player(name, school, teamId);
            return repoPlayer.Save(player);
        }

        public List<Player> allPlayersInATeam(Team team)
        {
            long id = team.ID;
            List<Player> allPlayers = repoPlayer.FindAll().ToList();

            return allPlayers.Where(player => player.TeamId == id).ToList();
        }
    }
}
