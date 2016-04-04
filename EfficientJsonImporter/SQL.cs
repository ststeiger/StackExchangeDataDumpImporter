
namespace EfficientJsonImporter
{


    internal class SQL
    {

        public static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with Instance field having
                // the sole instance
                System.Reflection.FieldInfo field = type.GetField("Instance"
                    , System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                    //return field.GetValue(null) as DbProviderFactory;
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))

            throw new System.Exception("DataProvider is missing!");
            //throw new System.Configuration.ConfigurationException("DataProvider is missing!");
        } // End Function GetFactory


        public static System.Data.Common.DbProviderFactory InitializeFactory()
        {
            // return GetFactory(typeof(Npgsql.NpgsqlFactory));
            return GetFactory(typeof(System.Data.SqlClient.SqlClientFactory));
        }


        public static System.Data.Common.DbProviderFactory m_fact = InitializeFactory();



        public static bool Log(System.Exception ex)
        {
            return Log(ex, null);
        } // End Function Log 


        public static bool Log(System.Exception ex, System.Data.IDbCommand cmd)
        {
            return Log(null, ex, cmd);
        } // End Function Log 


        public static bool Log(string location, System.Exception ex, System.Data.IDbCommand cmd)
        {
            if (location != null)
                Notify(location);

            Notify(ex.Message);

            if (cmd != null)
                Notify(cmd.CommandText);

            return true;
        } // End Function Log 


        public static void Notify(object obj)
        {
            string text = "NULL";
            if (obj != null)
                text = obj.ToString();
            
            string caption = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            // System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            System.Console.WriteLine(text);
        } // End Function Notify 


        public static string GetMsConnectionString()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.IntegratedSecurity = true;

            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ArtImportWebServices";
                csb.Password = "TOP_SECRET";
            }

            csb.DataSource = System.Environment.MachineName;
            csb.InitialCatalog = "StackExchange";

            return csb.ConnectionString;
        } // End Function GetConnectionString 


        public static string GetPgConnectionString()
        {
            string strTODO = @"CREATE ROLE stackexchangeimporter LOGIN PASSWORD '123' 
SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;
";
            System.Console.WriteLine(strTODO);

            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
            csb.UserName = "stackexchangeimporter";
            csb.Password = "123";
            csb.Port = 5432;
            csb.Database = "startups";
            csb.Host = "127.0.0.1";

            return csb.ToString();
        } // End Function GetConnectionString



        public static string GetConnectionString()
        {
            if (SQL.m_fact is System.Data.SqlClient.SqlClientFactory)
                return GetMsConnectionString();

            return GetPgConnectionString();
        }


        public static System.Data.Common.DbConnection GetConnection()
        {
            System.Data.Common.DbConnection dbConnection = m_fact.CreateConnection();
            dbConnection.ConnectionString = GetConnectionString();

            return dbConnection;
        } // End Function GetConnection 


        public static System.Data.Common.DbCommand CreateCommand()
        {
            return CreateCommand(null);
        } // End Function CreateCommand


        public static System.Data.Common.DbCommand CreateCommand(string SQL)
        {
            return CreateCommand(SQL, 30);
        } // End Function CreateCommand


        private static byte[] ReadAllBytesShareRead(string fileName)
        {
            byte[] file;

            using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {

                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                } // End Using reader

            } // End Using stream 

            return file;
        }


        public static System.Data.Common.DbCommand CreateCommand(string SQL, int timeout)
        {

            System.Data.Common.DbCommand cmd = m_fact.CreateCommand();
            cmd.CommandText = SQL;
            cmd.CommandTimeout = timeout;

            return cmd;
        } // End Function CreateCommand


        public static void SaveFile(string fileName, string SQL)
        {
            SaveFile(fileName, SQL, null);
        } // End Sub SaveFile


        // https://stackoverflow.com/questions/2579373/saving-any-file-to-in-the-database-just-convert-it-to-a-byte-array
        public static void SaveFile(string fileName, string SQL, string paramName)
        {
            byte[] file = ReadAllBytesShareRead(fileName);
            SaveFile(file, SQL, paramName);
        } // End Sub SaveFile



        public static void SaveFile(byte[] file, string SQL)
        {
            SaveFile(file, SQL, null);
        } // End Sub SaveFile


        public static void SaveFile(byte[] file, string SQL, string paramName)
        {
            using (System.Data.Common.DbCommand cmd = CreateCommand(SQL))
            {
                SaveFile(file, cmd, paramName);
            } // End Using cmd 

        } // End Sub SaveFile



        public static void SaveFile(string fileName, System.Data.IDbCommand cmd)
        {
            SaveFile(fileName, cmd, null);
        }


        public static void SaveFile(string fileName, System.Data.IDbCommand cmd, string paramName)
        {
            byte[] file = ReadAllBytesShareRead(fileName);
            SaveFile(file, cmd, paramName);
        } // End Sub SaveFile


        public static void SaveFile(byte[] file, System.Data.IDbCommand cmd)
        {
            SaveFile(file, cmd, null);
        }


        public static void SaveFile(byte[] file, System.Data.IDbCommand cmd, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
                paramName = "__file";

            if (!paramName.StartsWith("@"))
                paramName = "@" + paramName;

            if (!cmd.Parameters.Contains(paramName))
                AddParameter(cmd, paramName, file);

            ExecuteNonQuery(cmd);
        } // End Sub SaveFile




        // http://stackoverflow.com/questions/2885335/clr-sql-assembly-get-the-bytestream
        // http://stackoverflow.com/questions/891617/how-to-read-a-image-by-idatareader
        // http://stackoverflow.com/questions/4103406/extracting-a-net-assembly-from-sql-server-2005
        public static void RetrieveFile(string sql, string path)
        {
            RetrieveFile(sql, path, "data");
        } // End Sub RetrieveFile 


        public static void RetrieveFile(string sql, string path, string columnName)
        {

            using (System.Data.IDbCommand cmd = CreateCommand(sql, 0))
            {
                RetrieveFile(cmd, columnName, path);
            } // End Using cmd 

        } // End Sub RetrieveFile 


        public static void RetrieveFile(System.Data.IDbCommand cmd, string path)
        {
            RetrieveFile(cmd, null, path);
        } // End Sub RetrieveFile 


        // http://stackoverflow.com/questions/2885335/clr-sql-assembly-get-the-bytestream
        // http://stackoverflow.com/questions/891617/how-to-read-a-image-by-idatareader
        // http://stackoverflow.com/questions/4103406/extracting-a-net-assembly-from-sql-server-2005
        public static void RetrieveFile(System.Data.IDbCommand cmd, string columnName, string path)
        {
            using (System.Data.IDataReader reader = ExecuteReader(cmd, System.Data.CommandBehavior.SequentialAccess | System.Data.CommandBehavior.CloseConnection))
            {
                bool hasRows = reader.Read();
                if (hasRows)
                {
                    const int BUFFER_SIZE = 1024 * 1024 * 10; // 10 MB
                    byte[] buffer = new byte[BUFFER_SIZE];

                    int col = string.IsNullOrEmpty(columnName) ? 0 : reader.GetOrdinal(columnName);
                    int bytesRead = 0;
                    int offset = 0;

                    // Write the byte stream out to disk
                    using (System.IO.FileStream bytestream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
                    {
                        while ((bytesRead = (int)reader.GetBytes(col, offset, buffer, 0, BUFFER_SIZE)) > 0)
                        {
                            bytestream.Write(buffer, 0, bytesRead);
                            offset += bytesRead;
                        } // Whend

                        bytestream.Close();
                    } // End Using bytestream 

                } // End if (!hasRows)

                reader.Close();
            } // End Using reader

        } // End Function RetrieveFile


        public static int ExecuteNonQuery(string SQL)
        {
            int iAffected = 0;
            using (System.Data.Common.DbCommand cmd = CreateCommand(SQL))
            {
                iAffected = ExecuteNonQuery(cmd);
            } // End Using cmd 

            return iAffected;
        } // End Function ExecuteNonQuery 


        public static int ExecuteNonQuery(System.Data.IDbCommand cmd)
        {
            int iAffected = -1;

            try
            {

                using (System.Data.IDbConnection idbConn = GetConnection())
                {

                    lock (idbConn)
                    {

                        lock (cmd)
                        {

                            cmd.Connection = idbConn;

                            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                                cmd.Connection.Open();

                            using (System.Data.IDbTransaction idbtTrans = idbConn.BeginTransaction())
                            {

                                try
                                {
                                    cmd.Transaction = idbtTrans;

                                    iAffected = cmd.ExecuteNonQuery();
                                    idbtTrans.Commit();
                                } // End Try
                                catch (System.Data.Common.DbException ex)
                                {
                                    if (idbtTrans != null)
                                        idbtTrans.Rollback();

                                    iAffected = -2;

                                    if (Log(ex))
                                        throw;
                                } // End catch
                                finally
                                {
                                    if (cmd.Connection.State != System.Data.ConnectionState.Closed)
                                        cmd.Connection.Close();
                                } // End Finally

                            } // End Using idbtTrans

                        } // End lock cmd

                    } // End lock idbConn

                } // End Using idbConn

            } // End Try
            catch (System.Exception ex)
            {
                iAffected = -3;
                if (Log(ex, cmd))
                    throw;
            }
            finally
            {

            }

            return iAffected;
        } // End Function ExecuteNonQuery 


        public static System.Data.IDataReader ExecuteReader(System.Data.IDbCommand cmd)
        {
            return ExecuteReader(cmd, System.Data.CommandBehavior.CloseConnection);
        }


        public static System.Data.IDataReader ExecuteReader(System.Data.IDbCommand cmd, System.Data.CommandBehavior behav)
        {
            System.Data.IDataReader idr = null;

            lock (cmd)
            {
                System.Data.IDbConnection idbc = GetConnection();
                cmd.Connection = idbc;

                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();

                try
                {
                    idr = cmd.ExecuteReader(behav);
                }
                catch (System.Exception ex)
                {
                    if (Log(ex, cmd))
                        throw;
                }
            } // End Lock cmd

            return idr;
        } // End Function ExecuteReader


        public virtual System.Data.DataTable GetDataTable(string strSQL)
        {
            System.Data.DataTable dt = null;

            using (System.Data.IDbCommand cmd = CreateCommand(strSQL))
            {
                dt = GetDataTable(cmd);
            } // End Using cmd

            return dt;
        } // End Function GetDataTable


        public virtual System.Data.DataTable GetDataTable(System.Data.IDbCommand cmd)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbConnection idbc = GetConnection())
            {

                lock (idbc)
                {

                    lock (cmd)
                    {

                        try
                        {
                            cmd.Connection = idbc;

                            using (System.Data.Common.DbDataAdapter daQueryTable = m_fact.CreateDataAdapter())
                            {
                                daQueryTable.SelectCommand = (System.Data.Common.DbCommand)cmd;
                                daQueryTable.Fill(dt);
                            } // End Using daQueryTable

                        } // End Try
                        catch (System.Data.Common.DbException ex)
                        {
                            if (Log("SQL.GetDataTable(System.Data.IDbCommand cmd)", ex, cmd))
                                throw;
                        }// End Catch
                        finally
                        {
                            if (idbc.State != System.Data.ConnectionState.Closed)
                                idbc.Close();
                        } // End Finally

                    } // End lock cmd

                } // End lock idbc

            } // End Using idbc

            return dt;
        } // End Function GetDataTable


        // From Type to DBType
        protected static System.Data.DbType GetDbType(System.Type type)
        {
            // http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            string strTypeName = type.Name;
            System.Data.DbType DBtype = System.Data.DbType.String; // default value

            try
            {
                if (object.ReferenceEquals(type, typeof(System.DBNull)))
                {
                    return DBtype;
                }

                if (object.ReferenceEquals(type, typeof(System.Byte[])))
                {
                    return System.Data.DbType.Binary;
                }

                DBtype = (System.Data.DbType)System.Enum.Parse(typeof(System.Data.DbType), strTypeName, true);

                // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
                // http://msdn.microsoft.com/en-us/library/bbw6zyha(v=vs.71).aspx
                if (DBtype == System.Data.DbType.UInt64)
                    DBtype = System.Data.DbType.Int64;
            }
            catch (System.Exception)
            {
                // add error handling to suit your taste
            }

            return DBtype;
        } // End Function GetDbType


        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue)
        {
            return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad)
        {
            if (objValue == null)
            {
                //throw new ArgumentNullException("objValue");
                objValue = System.DBNull.Value;
            } // End if (objValue == null)

            System.Type tDataType = objValue.GetType();
            System.Data.DbType dbType = GetDbType(tDataType);

            return AddParameter(command, strParameterName, objValue, pad, dbType);
        } // End Function AddParameter


        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad, System.Data.DbType dbType)
        {
            System.Data.IDbDataParameter parameter = command.CreateParameter();

            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            } // End if (!strParameterName.StartsWith("@"))

            parameter.ParameterName = strParameterName;
            parameter.DbType = dbType;
            parameter.Direction = pad;

            // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
            // No association  DbType UInt64 to a known SqlDbType

            if (objValue == null)
                parameter.Value = System.DBNull.Value;
            else
                parameter.Value = objValue;

            command.Parameters.Add(parameter);
            return parameter;
        } // End Function AddParameter


    } // End Internal Class SQL

}
