using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
/// <summary>
/// 针对SQL Server数据库操作的通用类
/// 作者：周公
/// 日期：2009-01-08
/// Version:1.0
/// </summary>
public class SqlDbHelper
{
    private string connectionString;
    /// <summary>
    /// 设置数据库连接字符串
    /// </summary>
    public string ConnectionString
    {
        set { connectionString = value; }
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    public SqlDbHelper()
    {
        //#warning 注意，如果采用这种方式构建实例，必须在web.config中配置“conn”的数据库连接字符串
        connectionString = ConfigurationManager.ConnectionStrings["WebMemberConn"].ConnectionString;
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    public SqlDbHelper(string connectionString)
    {
        //#error 数据库连接字符串不能为空
        this.connectionString = connectionString;
    }
    /// <summary>
    /// 执行一个查询，并返回结果集
    /// </summary>
    /// <param name="sql">要执行的查询SQL文本命令</param>
    /// <returns>返回查询结果集</returns>
    public DataTable ExecuteDataTable(string sql)
    {
        return ExecuteDataTable(sql, CommandType.Text, null);
    }
    /// <summary>
    /// 执行一个查询,并返回查询结果
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <returns>返回查询结果集</returns>
    public DataTable ExecuteDataTable(string sql, CommandType commandType)
    {
        return ExecuteDataTable(sql, commandType, null);
    }
    /// <summary>
    /// 执行一个查询,并返回查询结果
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
    /// <returns></returns>
    public DataTable ExecuteDataTable(string sql, CommandType commandType, SqlParameter[] parameters)
    {
        DataTable data = new DataTable();//实例化DataTable，用于装载查询结果集
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                //如果同时传入了参数，则添加这些参数
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                //通过包含查询SQL的SqlCommand实例来实例化SqlDataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);//填充DataTable
            }
        }
        return data;
    }
    /// <summary>
    /// 返回一个SqlDataReader对象的实例
    /// </summary>
    /// <param name="sql">要执行的查询SQL文本命令</param>
    /// <returns></returns>
    public SqlDataReader ExecuteReader(string sql)
    {
        return ExecuteReader(sql, CommandType.Text, null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <returns></returns>
    public SqlDataReader ExecuteReader(string sql, CommandType commandType)
    {
        return ExecuteReader(sql, commandType, null);
    }
    /// <summary>
    /// 返回一个SqlDataReader对象的实例
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
    /// <returns></returns>
    public SqlDataReader ExecuteReader(string sql, CommandType commandType, SqlParameter[] parameters)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(sql, connection);
        //如果同时传入了参数，则添加这些参数
        if (parameters != null)
        {
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }
        connection.Open();
        //CommandBehavior.CloseConnection参数指示关闭Reader对象时关闭与其关联的Connection对象
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }
    /// <summary>
    /// 执行一个查询，返回查询结果集的第一行第一列，忽略其它行和列
    /// </summary>
    /// <param name="sql">要执行的查询SQL文本命令</param>
    /// <returns></returns>
    public Object ExecuteScalar(string sql)
    {
        return ExecuteScalar(sql, CommandType.Text, null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <returns></returns>
    public Object ExecuteScalar(string sql, CommandType commandType)
    {
        return ExecuteScalar(sql, commandType, null);
    }
    /// <summary>
    /// 执行一个查询，返回查询结果集的第一行第一列，忽略其它行和列
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
    /// <returns></returns>
    public Object ExecuteScalar(string sql, CommandType commandType, SqlParameter[] parameters)
    {
        object result = null;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                //如果同时传入了参数，则添加这些参数
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();//打开数据库连接
                result = command.ExecuteScalar();
            }
        }
        return result;//返回查询结果的第一行第一列，忽略其它行和列
    }
    /// <summary>
    /// 对数据库执行增删改操作
    /// </summary>
    /// <param name="sql">要执行的查询SQL文本命令</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql)
    {
        return ExecuteNonQuery(sql, CommandType.Text, null);
    }
    /// <summary>
    /// 对数据库执行增删改操作
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql, CommandType commandType)
    {
        return ExecuteNonQuery(sql, commandType, null);
    }
    /// <summary>
    /// 对数据库执行增删改操作
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
    /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql, CommandType commandType, SqlParameter[] parameters)
    {
        int count = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                //如果同时传入了参数，则添加这些参数
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();//打开数据库连接
                count = command.ExecuteNonQuery();
            }
        }
        return count;//返回执行增删改操作之后，数据库中受影响的行数
    }
    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">多条SQL语句</param>		
    public int ExecuteSqlTranArrayList(ArrayList SQLStringList)
    {
        int count = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                connection.Open();//打开数据库连接
                SqlTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    count = 1;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        return count;
    }
    /// <summary>
    /// 返回当前连接的数据库中所有由用户创建的数据库
    /// </summary>
    /// <returns></returns>
    public DataTable GetTables()
    {
        DataTable data = null;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();//打开数据库连接
            data = connection.GetSchema("Tables");
        }
        return data;
    }

}

