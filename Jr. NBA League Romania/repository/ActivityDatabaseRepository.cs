using Jr._NBA_League_Romania.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jr._NBA_League_Romania.repository
{
    class ActivityDatabaseRepository : DatabaseRepository<Tuple<long, long>, ActivePlayer>
    {
        public ActivityDatabaseRepository(String tableName) : base(tableName)
        {

        }
        public override ActivePlayer ExecuteInsert(ActivePlayer entity, NpgsqlCommand command)
        {
            command.Parameters.Add(new NpgsqlParameter("@id_player", entity.ID.Item1));
            command.Parameters.Add(new NpgsqlParameter("@id_match", entity.ID.Item2));
            command.Parameters.Add(new NpgsqlParameter("@points", entity.Points));
            string type;
            if (entity.TypePlayer == model.Type.Participant)
                type = "Participant";
            else
                type = "Reserve";
            command.Parameters.Add(new NpgsqlParameter("@type", type));
            NpgsqlDataReader reader = command.ExecuteReader();

            return entity;
        }

        public override ActivePlayer ExtractEntity(NpgsqlDataReader reader)
        {
            long id_player = reader.GetFieldValue<long>(0);
            long id_match = reader.GetFieldValue<long>(1);
            long points = reader.GetFieldValue<long>(2);
            string type = reader.GetFieldValue<string>(3);
            model.Type typePlayer;
            if (type.Equals("Reserve"))
                typePlayer = model.Type.Reserve;
            else
                typePlayer = model.Type.Participant;

            ActivePlayer activePlayer = new ActivePlayer()
            {
                ID = new Tuple<long, long>(id_player, id_match),
                Points = points,
                TypePlayer = typePlayer
            };

            return activePlayer;
        }

        public override string GetIdCondition(Tuple<long, long> id)
        {
            return "id_player=" + id.Item1.ToString() + " id_match=" + id.Item2.ToString();
        }

        public override string GetTableColumns()
        {
            return "(id_player, id_match, points, type)";
        }

        public override string GetTableValues()
        {
            return "(@id_player, @id_match, @points, @type)";
        }
    }
}
