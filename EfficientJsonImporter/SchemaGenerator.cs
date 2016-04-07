using System;

namespace EfficientJsonImporter
{
    public class SchemaGenerator
    {


        private static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        public static void Test()
        {
            string fn = MapProjectPath("SQL");

            if (SQL.DbType == SQL.DbType_t.MS_SQL)
                fn = System.IO.Path.Combine(fn, "04a_CreateSchema_MS_SQL.sql");
            else if (SQL.DbType == SQL.DbType_t.MySQL)
                fn = System.IO.Path.Combine(fn, "04b_CreateSchema_MySQL.sql");
            else //if (SQL.DbType == SQL.DbType_t.PostgreSQL)
                fn = System.IO.Path.Combine(fn, "04b_CreateSchema_PG_SQL.sql");
            
            string fileContent = System.IO.File.ReadAllText(fn, System.Text.Encoding.UTF8);
            System.Collections.Generic.List<string> scripts = DAL.Scripting.ScriptSplitter.SplitScript(fileContent);
            foreach(string script in scripts)
            {
                System.Console.WriteLine(script);
            }

        }


    }
}

