using System;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Text;
 
public class SqlAccess
{
	public static MySqlConnection dbConnection;
	//如果只是在本地的话，写localhost就可以。
	// static string host = "localhost";  
	//如果是局域网，那么写上本机的局域网IP
	static string host = "127.0.0.1";
	static string port = "3306";
	static string username = "root";
	static string pwd = "123456";
	static string database = "mygame";
 
	public SqlAccess()
	{
		OpenSql();
	}
 
	/// <summary>
	/// 连接数据库
	/// </summary>
	public static void OpenSql()
	{
		try
		{
			string connectionString = string.Format("server = {0};port={1};database = {2};user = {3};password = {4};pooling = true;", host, port, database, username, pwd);
			Debug.Log(connectionString);
			dbConnection = new MySqlConnection(connectionString);
			dbConnection.Open();
			Debug.Log("建立连接");
		}
		catch (Exception e)
		{
			throw new Exception("服务器连接失败, 请重新检查是否打开MySql服务。" + e.Message.ToString());
		}
	}
 
	/// <summary>
	/// 关闭数据库连接
	/// </summary>
	public void Close()
	{
		if (dbConnection != null)
		{
			dbConnection.Close();
			dbConnection.Dispose();
			dbConnection = null;
		}
	}
 
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="tableName">表名</param>
	/// <param name="items"></param>
	/// <param name="col">字段名</param>
	/// <param name="operation">运算符</param>
	/// <param name="values">字段值</param>
	/// <returns>DataSet</returns>
    public DataSet Select()
    {
        return ExecuteQuery("SELECT password FROM player WHERE phonenum = '16621276517'");
    }

	public string GetItemByNum(string item, string num)
	{
		string sql = "SELECT "+ item + " FROM player WHERE phonenum = '" + num + "'";
		DataSet ds = ExecuteQuery(sql);
		if(ds != null){
			return GetString(ds);
        }
		return null;
	}

	public string GetIdByNum(string num)
    {
		string sql = "SELECT id FROM player WHERE phonenum = '" + num + "'";
		DataSet ds = ExecuteQuery(sql);
		if(ds != null)
        {
			return GetString(ds);
        }
		return null;
    }
	public void InsertUser(string username, string phonenum, string password)
	{
		string sql = "INSERT INTO player(NAME, PASSWORD, phonenum) VALUES('" + username + "', '"+ password + "', '"+ phonenum +"')";
		ExecuteQuery(sql);
	}

	public string GetNumByID(string playerid, string itemid)
    {
		string sql = "SELECT num FROM hold WHERE playerid = " + playerid + " AND itemid = " + itemid;
		DataSet ds = ExecuteQuery(sql);
	    if(ds != null)
        {
			return GetString(ds);
        }
		return null;
	}
	
	public void DeleteItemByID(string itemid)
    {
		string sql = "UPDATE ";
    }
	public string GetString(DataSet _ds)
	{
		DataTable dt = _ds.Tables[0];
		string ps = "";
        foreach(DataRow row in dt.Rows)
        {
            foreach(DataColumn column in dt.Columns)
            {
                ps += row[column];
            }
        }
		if (ps != "")
		{
			return ps;
		}else
		{
			return null;
		}
	}
	public static DataSet ExecuteQuery(string sqlString)
	{
		if (dbConnection.State == ConnectionState.Open)
		{
			DataSet ds = new DataSet();
			try
			{
				MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
				da.Fill(ds);
			}
			catch (Exception ee)
			{
				throw new Exception("SQL:" + sqlString + "/n" + ee.Message.ToString());
			}
			finally
			{
			}
			return ds;
		}
		return null;
	}
}