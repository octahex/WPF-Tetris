﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Finisar.SQLite;
using System.Diagnostics;
using System.Collections;

namespace Tetris
{
    public class SQLiteDatabase
    {
        protected SQLiteConnection _conn;
        protected string _filename;
        protected bool _connected;
        protected bool _tableReady;
        protected string _savedTable;

        public SQLiteDatabase(string filename)
        {
            _filename = filename;
            _savedTable = "savedGames";
            initDb();
            initTables();
        }

        public SavedInstance getSavedInstanceById(int id)
        {
            SavedInstance si = new SavedInstance();
            string query = "select * from " + _savedTable + " where id=" + id;

            SQLiteDataReader reader = execReader(query);
            while (reader.Read())
            {
                si.Name = (string)reader["name"];
                si.Board = (string)reader["board"];
                si.Score = reader.GetInt32(reader.GetOrdinal("score"));
                si.LinesCleared = reader.GetInt32(reader.GetOrdinal("linescleared"));
            }

            return si;
        }

        public bool saveNewInstance(SavedInstance si)
        {
            string query = "INSERT INTO "+_savedTable+"(`name`, `board`, `score`, `linescleared`)";
            query += " VALUES(\'" + si.Name + "\', \'" + si.Board + "\', \'" + si.Score;
            query += "\', \'" + si.LinesCleared + "\')";
            return execNonQuery(query);
        }

        public SavedInstance[] getAllInstances()
        {
            ArrayList list = new ArrayList();

            string query = "select * from " + _savedTable;

            SQLiteDataReader reader = execReader(query);
            {
                SavedInstance si = new SavedInstance();
                si.Name = (string)reader["name"];
                si.Board = (string)reader["board"];
                si.Score = (int)reader["score"];
                si.LinesCleared = (int)reader["linescleared"];
                list.Add(si);
            }

            return (SavedInstance[]) list.ToArray();
        }

        public bool execNonQuery(string query)
        {
            if (Ready)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return false;
                }
            }
            else
                return false;
        }

        public SQLiteDataReader execReader(string query)
        {
            if (Ready)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    cmd.CommandText = query;
                    SQLiteDataReader datareader = cmd.ExecuteReader();
                    return datareader;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    return null;
                }
            }
            return null;
        }

        public void closeDb()
        {
            _conn.Close();
        }

        private void initDb()
        {
            _conn = new SQLiteConnection("Data Source=" + _filename + ";Version=3;Compress=True;");

            try
            {
                _conn.Open();
                _connected = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                _connected = false;
            }

            if (!_connected)
            {
                _conn = new SQLiteConnection("Data Source=" + _filename + ";New=True;Version=3;Compress=True;");

                try
                {
                    _conn.Open();
                    _connected = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    _connected = false;
                }
            }

        }

        private void initTables()
        {
            if (_connected)
            {
                bool tableExists = false;
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM "+_savedTable;
                try
                {
                    cmd.ExecuteNonQuery();
                    tableExists = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    tableExists = false;
                }

                if (!tableExists)
                {
                    cmd.CommandText = "CREATE TABLE "+_savedTable+" (id integer primary key, name varchar(40),"
                        + "board varchar(255), score integer, linescleared integer)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        _tableReady = true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                        _tableReady = false;
                    }
                }
            }
            else
                _tableReady = false;
        }

        public bool Ready
        {
            get
            {
                return _connected;
            }
        }

        public override string ToString()
        {
            return _conn.ToString();
        }
    }
}
