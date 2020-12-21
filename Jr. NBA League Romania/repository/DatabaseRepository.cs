using Jr._NBA_League_Romania.Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Jr._NBA_League_Romania.repository
{
    abstract class DatabaseRepository<ID, E> : IRepository<ID, E> where E : Entity<ID>
    {
        private String server = ConfigurationManager.AppSettings["server"];
        private String port = ConfigurationManager.AppSettings["port"];
        private String user = ConfigurationManager.AppSettings["user"];
        private String password = ConfigurationManager.AppSettings["password"];
        private String database = ConfigurationManager.AppSettings["database"];
        private NpgsqlConnection connection;
        public String TableName { get; set; }

        public abstract E ExtractEntity(NpgsqlDataReader reader);
        public abstract string GetIdCondition(ID id);
        public abstract string GetTableColumns();
        public abstract E ExecuteInsert(E entity, NpgsqlCommand command);
        public DatabaseRepository(String tableName)
        {
            TableName = tableName;
            initConnection();
        }

        private void initConnection()
        {
            string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    server, port, user,
                    password, database);

            // Making connection with Npgsql provider
            connection = new NpgsqlConnection(connstring);
            connection.Open();
        }
        public E Delete(ID id)
        {
            string queryDelete = "DELETE FROM " + TableName + " WHERE " + GetIdCondition(id);
            NpgsqlCommand command = new NpgsqlCommand(queryDelete, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                return ExtractEntity(reader);

            return null;
        }

        public IEnumerable<E> FindAll()
        {
            string querySelect = "SELECT * FROM " + TableName;
            NpgsqlCommand command = new NpgsqlCommand(querySelect, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            HashSet<E> elements = new HashSet<E>();
            while (reader.Read())
                elements.Add(ExtractEntity(reader));

            return elements;
        }

        public E FindOne(ID id)
        {
            string querySelectOne = "SELECT * FROM " + TableName + " WHERE " + GetIdCondition(id);
            NpgsqlCommand command = new NpgsqlCommand(querySelectOne, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                return ExtractEntity(reader);

            return null;
        }

        public E Save(E entity)
        {
            string queryInsert = "INSERT INTO " + TableName + " VALUES " + GetTableColumns();
            NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection);
            return ExecuteInsert(entity, command);
        }

        public E Update(E entity)
        {
            throw new NotImplementedException();
        }
    }
}
