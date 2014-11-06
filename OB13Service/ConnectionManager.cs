using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using DataInterface;

namespace OB13Service
{
    public static class ConnectionManager
    {
        private static ConcurrentDictionary<string,OBSession> _connections;

        public static OBSession GetConnection(string UserName, string Password)
        {
            if (_connections == null)
            {
                _connections = new ConcurrentDictionary<string, OBSession>();
            }

            OBSession session = null;

            _connections.TryGetValue(UserName, out session);

            if (session == null)
            {
                session = GetSessionFromDatabase(UserName);

                if (session != null)
                {
                    _connections.AddOrUpdate(UserName, session, (key,oldValue) => session);
                }
                else
                {
                    DeleteSession(UserName);
                }
            }

            if (session == null)
            {
                session = CreateNewConnection(UserName, Password);

                if (session != null)
                {
                    SaveSession(session);
                    _connections.AddOrUpdate(UserName, session, (key, oldValue) => session);
                }
            }
            
            return session;
        }

        public static void CloseConnection(string UserName, string Password)
        {
            OBSession session = GetConnection(UserName, Password);

            if (session != null)
            {
                CloseConnection(session);
            }
        }

        public static void CloseConnection(OBSession session)
        {
            session.ApplicationObject.Disconnect();
            DeleteSession(session.OnBaseUserName);
        }

        private static OBSession CreateNewConnection(string UserName, string Password)
        {
            OBSession session = new OBSession
            {
                OnBaseUserName = UserName, 
                OnBasePassword = Password,
                ServiceURL = System.Configuration.ConfigurationManager.AppSettings["DmsServiceURL"],
                ServiceDataSource = System.Configuration.ConfigurationManager.AppSettings["DmsServiceDataSource"]
            } ;

            if (session.ConnectWithCredentials())
            {
                return session;
            }

            return null;
        }

        private static OBSession GetSessionFromDatabase(string UserName)
        {
            using (SqlStoredProc sp = new SqlStoredProc("dbo.OBUserSessionSelect"))
            {
                sp.AddInputParameter("@OBUserName",UserName);
                using (SqlDataReader dr = sp.OpenDataReader())
                {
                    if (dr.Read())
                    {
                        OBSession session = new OBSession(dr);

                        if (session.ConnectWithSessionID())
                        {
                            return session;
                        }
                    }
                }
            }

            return null;
        }

        private static void DeleteSession(string UserName)
        {
            using (SqlStoredProc sp = new SqlStoredProc("dbo.OBUserSessionDelete"))
            {
                sp.AddInputParameter("@OBUserName",UserName);
                sp.ExecNonQuery();
            }

            OBSession session;
            if(_connections.TryRemove(UserName, out session))
            {
                session.Disconnect();
            }
        }

        private static void SaveSession(OBSession session)
        {
            using (SqlStoredProc sp = new SqlStoredProc("dbo.OBUserSessionInsert"))
            {
                sp.AddInputParameter("@OBUserName",session.OnBaseUserName);
                sp.AddInputParameter("@SessionID",session.SessionID);
                sp.AddInputParameter("@ServiceURL",session.ServiceURL);
                sp.AddInputParameter("@ServiceDataSource",session.ServiceDataSource);
                sp.ExecNonQuery();
            }
        }
    }

    
}
