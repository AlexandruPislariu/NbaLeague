using Jr._NBA_League_Romania.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.repository
{
    class TeamDatabaseRepository : DatabaseRepository<long, Team>
    {

        public TeamDatabaseRepository(String tableName) : base(tableName)
        {

        }

        public override Team ExecuteInsert(Team entity, NpgsqlCommand command)
        {
            command.Parameters.Add(new NpgsqlParameter("@name", entity.Name));
            NpgsqlDataReader reader = command.ExecuteReader();
            return entity;
        }

        public override Team ExtractEntity(NpgsqlDataReader reader)
        {
            long ID = reader.GetFieldValue<long>(0);
            string name = reader.GetFieldValue<string>(1);
            Team team = new Team();
            team.ID = ID;
            team.Name = name;
            return team;
        }

        public override string GetIdCondition(long id)
        {
            return "id=" + id.ToString(); 
        }

        public override string GetTableColumns()
        {
            return "(name)";
        }

        public override string GetTableValues()
        {
            return "(@name)";
        }
    }
}
