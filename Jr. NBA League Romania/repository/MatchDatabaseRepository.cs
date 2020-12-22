using Jr._NBA_League_Romania.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.repository
{
    class MatchDatabaseRepository : DatabaseRepository<long, Match>
    {
        public MatchDatabaseRepository(String tableName) : base(tableName)
        {

        }
        public override Match ExecuteInsert(Match entity, NpgsqlCommand command)
        {
            command.Parameters.Add(new NpgsqlParameter("@first_team", entity.FirstTeam));
            command.Parameters.Add(new NpgsqlParameter("@second_team", entity.SecondTeam));
            command.Parameters.Add(new NpgsqlParameter("@date", entity.Date));
            NpgsqlDataReader reader = command.ExecuteReader();
            return entity;
        }

        public override Match ExtractEntity(NpgsqlDataReader reader)
        {
            long id = reader.GetFieldValue<long>(0);
            long first_team = reader.GetFieldValue<long>(1);
            long second_team = reader.GetFieldValue<long>(2);
            DateTime date = reader.GetFieldValue<DateTime>(3);
            Match match = new Match()
            {
                ID = id,
                FirstTeam = first_team,
                SecondTeam = second_team,
                Date = date
            };
            return match;

        }

        public override string GetIdCondition(long id)
        {
            return "id_match=" + id.ToString();
        }

        public override string GetTableColumns()
        {
            return "(first_team, second_team, date)";
        }

        public override string GetTableValues()
        {
            return "(@first_team, @second_team, @date)";
        }
    }
}
