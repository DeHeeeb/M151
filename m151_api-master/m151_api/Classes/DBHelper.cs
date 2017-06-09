using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace m151_api.Classes
{
    public class DBHelper
    {
        static DBHelper instance = null;
        static readonly object padlock = new object();

        public SqlConnection connection { get; set; }

        private DBHelper()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"
                                Data Source=DESKTOP-O4TFK6I\SQLEXPRESS;
                                Initial Catalog=ludothek_verwaltung;
                                Integrated Security=True;";
            connection.Open();
        }
        public static DBHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new DBHelper();
                    return instance;
                }

            }
        }
    }
}