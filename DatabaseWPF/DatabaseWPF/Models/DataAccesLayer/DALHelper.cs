using DatabaseWPF.Models.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows;

namespace DatabaseWPF.Models.DataAccesLayer
{
    public static class DALHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Achi"].ConnectionString;

        public static SqlConnection Connection => new SqlConnection(connectionString);

        private static List<T> ReadData<T>(SqlDataReader reader) where T : DatabaseObject, new()
        {
            List<T> result = new();

            PropertyInfo[] properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                T temp = new();

                foreach (var property in properties)
                {
                    ClassSetter.SetProperty(temp, property, reader[property.Name]);
                }

                result.Add(temp);
            }

            return result;
        }

        private static List<DatabaseObject> ReadData(SqlDataReader reader, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();

            List<DatabaseObject> result = new();

            while (reader.Read())
            {
                object temp = Activator.CreateInstance(type);

                foreach (var property in properties)
                {
                    ClassSetter.SetProperty(temp, property, reader[property.Name]);
                }

                result.Add(temp as DatabaseObject);
            }

            return result;
        }

        private static T ReadObject<T>(SqlDataReader reader) where T : DatabaseObject, new()
        {
            return ReadObject(reader, typeof(T)) as T;
        }

        private static object ReadObject(SqlDataReader reader, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            object result = Activator.CreateInstance(type);

            if (reader.Read())
            {
                foreach (var property in properties)
                {
                    ClassSetter.SetProperty(result, property, reader[property.Name]);
                }

                return result;
            }

            return null;
        }

        public static object GetObject(string procedureName, Type type, params (string name, object data)[] parameters)
        {
            using SqlConnection con = Connection;

            con.Open();

            SqlCommand cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var pair in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(pair.name, pair.data));
            }

            SqlDataReader reader = cmd.ExecuteReader();

            return ReadObject(reader, type);
        }

        public static List<T> GetObjects<T>(string procedureName, params (string name, object data)[] parameters)
            where T : DatabaseObject, new()
        {
            using SqlConnection con = Connection;

            con.Open();

            SqlCommand cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var pair in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(pair.name, pair.data));
            }

            SqlDataReader reader = cmd.ExecuteReader();

            return ReadData<T>(reader);
        }

        public static List<DatabaseObject> GetObjects
            (string procedureName, Type type, params (string name, object data)[] parameters)
        {
            using SqlConnection con = Connection;

            con.Open();

            SqlCommand cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var pair in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(pair.name, pair.data));
            }

            SqlDataReader reader = cmd.ExecuteReader();

            return ReadData(reader, type);
        }

        public static T GetObject<T>(string procedureName, params (string name, object data)[] parameters)
            where T : DatabaseObject, new()
        {
            using SqlConnection con = Connection;

            con.Open();

            SqlCommand cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var pair in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(pair.name, pair.data));
            }

            SqlDataReader reader = cmd.ExecuteReader();

            return ReadObject<T>(reader);
        }

        public static void ExecuteNonQuery(string procedureName, params (string name, object data)[] parameters)
        {
            using SqlConnection con = Connection;
            SqlCommand cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(param.name, param.data));
            }

            con.Open();
            cmd.ExecuteNonQuery();
        }

        private static List<object> GetListAux(SqlDataReader reader, string column)
        {
            List<object> result = new();

            while (reader.Read())
            {
                result.Add(reader[column]);
            }

            return result;
        }

        public static List<object> GetList(string procedureName, string column)
        {
            using SqlConnection con = Connection;

            try
            {
                SqlCommand cmd = new SqlCommand(procedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                return GetListAux(reader, column);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }
    }
}
