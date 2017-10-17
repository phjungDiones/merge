using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace TBDB_CTC.FileLib
{
    public class CAccDB
    {
        public delegate void DeleException(string strErr);
        public event DeleException deleException = null;

        private OleDbConnection m_ObjCon;
        private Boolean m_bConn = false;

        public CAccDB()
        {
            m_ObjCon = new OleDbConnection();
            m_ObjCon.ConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=ACCDBTEST.accdb;";
        }

        public CAccDB(string path)
        {
            m_ObjCon = new OleDbConnection();
            m_ObjCon.ConnectionString = string.Format("Provider=Microsoft.Ace.OLEDB.12.0;Data Source={0};", path);
        }

        public bool OpenDB()
        {
            bool bConn = false;

            try
            {
                m_ObjCon.Open();
                bConn = true;
            }
            catch(Exception ex)
            {
                bConn = false;
                if (deleException != null)
                    deleException("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
            }

            m_bConn = bConn;
            return bConn;
        }

        public bool CloseDB()
        {
            bool bClose = false;

            try
            {
                m_ObjCon.Close();
                bClose = true;
                m_bConn = false;
            }
            catch
            {
                bClose = false;
            }

            return bClose;
        }

        public bool CreateDB()
        {
            bool isCreate = false;
            try
            {
                Type objClassType = Type.GetTypeFromProgID("ADOX.Catalog");
                if (objClassType != null)
                {
                    object obj = Activator.CreateInstance(objClassType);

                    obj.GetType().InvokeMember("Create", System.Reflection.BindingFlags.InvokeMethod, null, obj,
                        new object[] { "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + m_ObjCon.ConnectionString + ";" });

                    isCreate = true;

                    // Clean Up
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                if (deleException != null)
                    deleException("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
            }

            return isCreate;
        }

        public OleDbConnection GetConObj()
        {
            return m_ObjCon;
        }

        public bool IsConn()
        {
            return m_bConn;
        }
    }
}
