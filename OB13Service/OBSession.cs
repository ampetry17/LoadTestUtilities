using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using Hyland.Types;
using System.Data;
using System.Data.SqlClient;

namespace OB13Service
{
    public class OBSession : IDisposable
    {
        public Application ApplicationObject { get; set; }
        public string SessionID { get; set; }
        public string ServiceURL { get; set; }
        public string ServiceDataSource { get; set; }
        public string OnBaseUserName { get; set; }
        public string OnBasePassword { get; set; }
        public bool IsConnected { get; set; }

        public OBSession()
        {
            SessionID = string.Empty;
            ServiceURL = string.Empty;
            ServiceDataSource = string.Empty;
            OnBaseUserName = string.Empty;
            OnBasePassword = string.Empty;
            IsConnected = false;
        }

        public OBSession(SqlDataReader dr) : this()
        {
            SessionID = (string)dr["SessionID"];
            ServiceURL = (string)dr["ServiceURL"];
            ServiceDataSource = (string)dr["ServiceDataSource"];
        }

        public bool ConnectWithSessionID()
        {
            AuthenticationProperties authProperties = Application.CreateSessionIDAuthenticationProperties(ServiceURL,SessionID,true);
            try
            {
                ApplicationObject = Application.Connect(authProperties);
                IsConnected = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ConnectWithCredentials()
        {

            AuthenticationProperties authProperties = 
                Application.CreateOnBaseAuthenticationProperties(ServiceURL,
                OnBaseUserName, OnBasePassword, ServiceDataSource);

            try
            {
                ApplicationObject = Application.Connect(authProperties);
                SessionID = ApplicationObject.SessionID;
                IsConnected = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            ApplicationObject.Disconnect();
            IsConnected = false;
        }

        public void Dispose()
        {
            if (IsConnected)
            {
                ApplicationObject.Dispose();
            }
        }
    }
}
