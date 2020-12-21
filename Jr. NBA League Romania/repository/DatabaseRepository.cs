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
        public abstract string GetTableValues();
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
        }
        public E Delete(ID id)
        {
            connection.Open();
            string queryDelete = "DELETE FROM " + TableName + " WHERE " + GetIdCondition(id);
            NpgsqlCommand command = new NpgsqlCommand(queryDelete, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            connection.Close();

            return null;
        }

        public IEnumerable<E> FindAll()
        {
            connection.Open();
            string querySelect = "SELECT * FROM " + TableName;
            NpgsqlCommand command = new NpgsqlCommand(querySelect, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            HashSet<E> elements = new HashSet<E>();
            while (reader.Read())
                elements.Add(ExtractEntity(reader));

            connection.Close();
            return elements;
        }

        public E FindOne(ID id)
        {
            connection.Open();

            string querySelectOne = "SELECT * FROM " + TableName + " WHERE " + GetIdCondition(id);
            NpgsqlCommand command = new NpgsqlCommand(querySelectOne, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            E foundEntity = null;
            while (reader.Read())
                foundEntity = ExtractEntity(reader);

            connection.Close();
            return foundEntity;
        }

        public E Save(E entity)
        {
            connection.Open();

            string queryInsert = "INSERT INTO " + TableName + GetTableColumns() + " VALUES " + GetTableValues();
            NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection);
            E savedEntity = ExecuteInsert(entity, command);
            connection.Close();
            return savedEntity;
        }

        public E Update(E entity)
        {
            throw new NotImplementedException();
        }
    }
}
