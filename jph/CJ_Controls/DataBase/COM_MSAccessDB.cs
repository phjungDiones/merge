using System;
using System.Data;
using System.Collections;
using System.Data.Odbc;
using System.Windows.Forms;
using System.ComponentModel;

namespace CJ_Controls.DataBase
{
	/// <summary>
	/// 2010.07.19 By DH.Choi
	/// 1. Updated that when open, create commands
	/// 2011.07.01 by WH.Choi
	/// 1. Exchange singletone pattern
	/// </summary>
	public class COM_MSAccessDB : Component
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		public COM_MSAccessDB()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string m_strDB_Path = "DB\\DB.mdb";
		[Category("DB_Setting"), Description("DB 파일의 경로를 입력하세요."), DefaultValue(false)]
		public string DB_Path
		{
			get { return m_strDB_Path; }
			set { m_strDB_Path = value; }
		}

		object _LockObj = new object();
		OdbcDataAdapter m_Adapter = new OdbcDataAdapter();
		public OdbcDataAdapter Adapter
		{
			get
			{
				return m_Adapter;
			}
		}
		internal OdbcConnection m_Connection = null;
		public OdbcConnection Connection
		{
			get
			{
				return m_Connection;
			}
		}

		public bool GetTableColumnList(string strTable, string strColumn, ArrayList ColumnList)
		{
			bool bRet = true;
			DataSet ds = new DataSet();
			string strSQL = "SELECT " + strColumn + " FROM " + strTable;

			if (GetRecords(ds, strSQL))
			{
				foreach (DataTable myTable in ds.Tables)
				{
					foreach (DataRow myRow in myTable.Rows)
					{
						ColumnList.Add(myRow[strColumn].ToString());
					}
				}
			}
			else
			{
				bRet = false;
			}
			ds.Dispose();
			return bRet;
		}

		//Cjinnn MDB 최적화 추가..
		//private bool MDB_File_Clean()
		//{
		//	bool bRtn = false;
		//	string strCleanMdb = "DB\\CleanTmp.mdb";
		//	try
		//	{
		//		System.IO.File.Copy(strCleanMdb, m_strDB_Path, true);

		//		//참조 필요 : 참조에서 COM 탭의 "Microsoft Jet and Replication Objects 2.6 Library"
		//		JRO.JetEngineClass jro = new JRO.JetEngineClass();
		//		jro.CompactDatabase("Provider=Microsoft.jet.OLEDB.4.0;Data Source=" + m_strDB_Path,
		//											"Provider=Microsoft.jet.OLEDB.4.0;Data Source=" + strCleanMdb);
		//		bRtn = true;
		//	}
		//	catch (System.Exception ex)
		//	{
		//		LogTextOut("DB 파일 최적화 실패! -> " + ex.Message);
		//	}
		//	finally
		//	{
		//		System.IO.File.Delete(strCleanMdb);
		//	}
		//	return bRtn;
		//}

		public bool Open()
		{
			bool bOpen = true;
			try
			{
				if (m_Connection != null)
				{
					LogTextOut("DB(Open(string strDBName, bool bMdbClean)) (m_Connection != null)");
					return false;
				}

				string strConnection = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + m_strDB_Path + ";Exclusive=1";
				m_Connection = new OdbcConnection(strConnection);
				m_Connection.Open();

				// Create Command
				m_Adapter.InsertCommand = new OdbcCommand("", m_Connection);
				m_Adapter.SelectCommand = new OdbcCommand("", m_Connection);
				m_Adapter.UpdateCommand = new OdbcCommand("", m_Connection);
				m_Adapter.DeleteCommand = new OdbcCommand("", m_Connection);
				LogTextOut("DB Open Success!");
			}
			catch (Exception ex)
			{
				string strEx = "DB Open Error! -> " + ex.Message;
				LogTextOut(strEx);
				bOpen = false;
			}

			return bOpen;
		}

		public bool GetRecords(DataSet ds, string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.SelectCommand);
					m_Adapter.SelectCommand.CommandText = strSQL;
					m_Adapter.Fill(ds);

					// 2007.11.06
					m_Adapter.SelectCommand.Parameters.Clear();
					bRet = true;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.SelectCommand.Parameters.Clear();

					LogTextOut("DB(GetRecords(DataSet ds, string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.SelectCommand);
				}
			}
			return bRet;
		}
		public bool GetRecords(DataTable dt, string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.SelectCommand);
					m_Adapter.SelectCommand.CommandText = strSQL;
					m_Adapter.Fill(dt);

					// 2007.11.06
					m_Adapter.SelectCommand.Parameters.Clear();
					bRet = true;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.SelectCommand.Parameters.Clear();

					LogTextOut("DB(GetRecords(DataTable dt, string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.SelectCommand);
				}
			}
			return bRet;
		}
		public bool GetRecords(DataSet ds, string tableName, string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.SelectCommand);
					m_Adapter.SelectCommand.CommandText = strSQL;
					m_Adapter.Fill(ds, tableName);

					// 2007.11.06
					m_Adapter.SelectCommand.Parameters.Clear();
					bRet = true;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.SelectCommand.Parameters.Clear();

					LogTextOut("DB(GetRecords(DataSet ds, string tableName, string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.SelectCommand);
				}
			}
			return bRet;
		}
		public void Close()
		{
			if (m_Connection.State != ConnectionState.Closed)
			{
				m_Connection.Close();
				m_Connection = null;
			}
		}

		public bool UpdateDB(string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.UpdateCommand);
					m_Adapter.UpdateCommand.CommandText = strSQL;
					m_Adapter.UpdateCommand.ExecuteNonQuery();

					// 2007.11.06
					m_Adapter.UpdateCommand.Parameters.Clear();
					bRet = true;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.UpdateCommand.Parameters.Clear();

					LogTextOut("DB(UpdateDB(string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.UpdateCommand);
				}
			}
			return bRet;
		}

		public bool InsertDB(string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.InsertCommand);
					m_Adapter.InsertCommand.CommandText = strSQL;
					m_Adapter.InsertCommand.Prepare();

					//DataTable _Tables = m_Adapter.InsertCommand.Connection.GetSchema();

					m_Adapter.InsertCommand.ExecuteNonQuery();

					// 2007.11.06
					m_Adapter.InsertCommand.Parameters.Clear();
					bRet = true;
				}
				catch (OdbcException ex)
				{
					if (m_Adapter != null)
						m_Adapter.InsertCommand.Parameters.Clear();

					LogTextOut("DB(InsertDB(string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.InsertCommand.Parameters.Clear();

					LogTextOut("DB(InsertDB(string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.InsertCommand);
				}
			}
			return bRet;
		}

		public bool DeleteDB(string strSQL)
		{
			bool bRet = false;
			lock (_LockObj)
			{
				try
				{
					//OpenConnection(m_Adapter.DeleteCommand);
					m_Adapter.DeleteCommand.CommandText = strSQL;
					m_Adapter.DeleteCommand.ExecuteNonQuery();

					// 2007.11.06
					m_Adapter.DeleteCommand.Parameters.Clear();
					bRet = true;
				}
				catch (Exception ex)
				{
					if (m_Adapter != null)
						m_Adapter.DeleteCommand.Parameters.Clear();

					LogTextOut("DB(DeleteDB(string strSQL)) [SQL] " + strSQL + "," + ex.Message);
					bRet = false;
				}
				finally
				{
					//CloseConnection(m_Adapter.DeleteCommand);
				}
			}
			return bRet;
		}
	}
}
