using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CWExpert
{
    class DB
    {
        public static DataSet ds;

        private static string app_data_path = "";
        public static string AppDataPath
        {
            set { app_data_path = value; }
        }

        #region Private Member Functions
        // ======================================================
        // Private Member Functions
        // ======================================================

        private static void Create()
        {
        }      

        private static void VerifyTables()
        {
        }

        #endregion

        #region Public Member Functions
        // ======================================================
        // Public Member Functions 
        // ======================================================

        public static void Init()
        {
            ds = new DataSet("Data");

            if (File.Exists(app_data_path + "\\" + "database.xml"))
                ds.ReadXml(app_data_path + "\\" + "database.xml");
            else
            {
                Create();
            }
        }

        public static void Update()
        {
            ds.WriteXml(app_data_path + "\\" + "database.xml", XmlWriteMode.WriteSchema);
        }

        public static void Exit()
        {
            try
            {
                Update();
                ds = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB exit error!" + ex.ToString());
            }
        }

        private static void AddFormTable(string name)
        {
            ds.Tables.Add(name);
            ds.Tables[name].Columns.Add("Key", typeof(string));
            ds.Tables[name].Columns.Add("Value", typeof(string));
        }

        public static void SaveVars(string tableName, ref ArrayList list)
        {
            if (!ds.Tables.Contains(tableName))
                AddFormTable(tableName);

            foreach (string s in list)
            {
                string[] vals = s.Split('/');
                if (vals.Length > 2)
                {
                    for (int i = 2; i < vals.Length; i++)
                        vals[1] += "/" + vals[i];
                }

                DataRow[] rows = ds.Tables[tableName].Select("Key = '" + vals[0] + "'");
                if (rows.Length == 0)	// name is not in list
                {
                    DataRow newRow = ds.Tables[tableName].NewRow();
                    newRow[0] = vals[0];
                    newRow[1] = vals[1];
                    ds.Tables[tableName].Rows.Add(newRow);
                }
                else if (rows.Length == 1)
                {
                    rows[0][1] = vals[1];
                }
            }
        }

        public static ArrayList GetVars(string tableName)
        {
            ArrayList list = new ArrayList();
            if (!ds.Tables.Contains(tableName))
                return list;

            DataTable t = ds.Tables[tableName];

            for (int i = 0; i < t.Rows.Count; i++)
            {
                list.Add(t.Rows[i][0].ToString() + "/" + t.Rows[i][1].ToString());
            }

            return list;
        }

        #endregion
    }
}
