using Jr._NBA_League_Romania.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.repository
{
    class PlayerDatabaseRepository : DatabaseRepository<long, Player>
    {
        public PlayerDatabaseRepository(string tableName):base(tableName)
        {
           
        }

        public override Player ExecuteInsert(Player entity, NpgsqlCommand command)
        {
            command.Parameters.Add(new NpgsqlParameter("@name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("@school", entity.School));
            command.Parameters.Add(new NpgsqlParameter("@team_id", entity.TeamId));
            NpgsqlDataReader reader = command.ExecuteReader();
            return entity;
        }

        public override Player ExtractEntity(NpgsqlDataReader reader)
        {
            long id = reader.GetFieldValue<long>(0);
            string name = reader.GetFieldValue<string>(1);
            string school = reader.GetFieldValue<string>(2);
            long id_team = reader.GetFieldValue<long>(3);
            Player player = new Player(name, school, id_team)
            {
                ID = id,
            };

            return player;
        }

        public override string GetIdCondition(long id)
        {
            return "id_player=" + id.ToString();
        }

        public override string GetTableColumns()
        {
            return "(name, school, team_id)";
        }

        public override string GetTableValues()
        {
            return "(@name, @school, @team_id)";
        }
    }
}
