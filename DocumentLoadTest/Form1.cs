using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using CompassFrameworkLibrary;
using Compass.ServiceFactory;
using System.Diagnostics;
using DataInterface;
using System.Net;
using System.Runtime.Serialization;


namespace DocumentLoadTest
{
    public partial class Form1 : Form
    {
        private List<cKeywordType> _keywordTypes;
        private List<cDocumentType> _documentTypes;
        private DmsEntityTypes _entityTypes;
        private bool _userTokenCreated;
        private int PageCount;
        private bool isDocColor;
        private int _chunkSize = 0;

        public Form1()
        {
            InitializeComponent();

            CompassDocumentsDAL.Common.gConfigOptions = new ConfigurableOptions();
            DataSet dsConfig = DBInterface.GetDSfromSQL("select * from configuration");
            CompassDocumentsDAL.Common.gConfigOptions.ConfigInit(dsConfig);

        }

        private void SaveDocument(string FileName)
        {

            Stopwatch sw = Stopwatch.StartNew();

            int pkApplicationUser = 296;

            string UserNameID = txtOBUsername.Text;
            
            if (UserNameID.Contains("LoadTest"))
            {
                pkApplicationUser = int.Parse(UserNameID.Replace("LoadTest", ""));
                if (pkApplicationUser == 1)
                {
                    pkApplicationUser += 295;
                }
                else
                {
                    pkApplicationUser += 296;
                }
            }
            

            CreateUserToken(pkApplicationUser, txtOBUsername.Text);


            DataSet svcDirectory = new DataSet();
            //string FilePath = ConfigurationManager.AppSettings["TestFileFolder"] + @"\servicedirectory.xml";
            string FilePath = Environment.ExpandEnvironmentVariables(@"%userprofile%\TestFiles" + @"\servicedirectory.xml");
            svcDirectory.ReadXml(FilePath);

            CompassDocumentsDAL.Common.gConfigOptions.serviceDirectory = svcDirectory;

            cDocumentAssignment assignment = new cDocumentAssignment();
            assignment.AssignedUser = pkApplicationUser.ToString();
            assignment.CreateDate = DateTime.Now;
            assignment.CreateUser = pkApplicationUser.ToString();
            assignment.FollowUpDate = DateTime.Now.AddDays(3);
            assignment.Note = "task note";
            assignment.fkDocument = "-1";
            assignment.fkRefAssignmentType = 1;
            assignment.pkDocumentUserAssignment = -1;

            List<cKeywordType> BlankKeywordTypes = GetBlankKeywordTypes();

            cKeywordType compassNumberKeyword =
                BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "Compass Number");
            compassNumberKeyword.KeywordValue = "OH123000000101";

            cKeywordType ssnKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "SSN");
            ssnKeyword.KeywordValue = "111-11-1111";

            cKeywordType firstNameKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "First Name");
            firstNameKeyword.KeywordValue = "Load";

            cKeywordType lastNameKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "Last Name");
            lastNameKeyword.KeywordValue = "Tester";

            cKeywordType docDateKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "Doc Date");
            docDateKeyword.KeywordValue = DateTime.Today.ToShortDateString();

            cKeywordType receivedDateKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "RECEIVED DATE");
            receivedDateKeyword.KeywordValue = DateTime.Today.ToShortDateString();

            cKeywordType caseTagKeyword = BlankKeywordTypes.FirstOrDefault(x => x.KeywordName == "Case Tag");
            caseTagKeyword.KeywordValue = string.Empty;

            List<cKeywordType> keywords = new List<cKeywordType>
            {
                compassNumberKeyword,
                ssnKeyword,
                firstNameKeyword,
                lastNameKeyword,
                docDateKeyword,
                receivedDateKeyword,
                caseTagKeyword
            };

            List<cDocumentType> BlankDocumentTypes = GetBlankDocumentTypes();

            cDocumentType docType = BlankDocumentTypes.FirstOrDefault(x => x.Name == "CSEA FISC Audit");

            cDocument.eDocStatus docStatus = cDocument.eDocStatus.NotFiled;

            string testFile = Environment.ExpandEnvironmentVariables(@"%userprofile%\TestFiles" + @"\" + FileName);
            byte[] documentAsBytes = System.IO.File.ReadAllBytes(testFile);
            string sUploadId = Guid.NewGuid().ToString();


            string oRet = null;

            if (rbWCF.Checked)
            {
                oRet = DoWCFClassicCall(documentAsBytes, sUploadId, docType, keywords, assignment, pkApplicationUser,
                    docStatus).pkDocument;
            }
            else
            {
                oRet = DoRestCall(documentAsBytes, sUploadId, docType, keywords, assignment, pkApplicationUser,
                    docStatus);
            }
            //IsolatedStorage.KillIsoFile(testFile,"CaptureTest");

            LogResults(sw.ElapsedMilliseconds);
            sw.Stop();

        }

        private string DoRestCall(byte[] documentAsBytes, string sUploadId,
            cDocumentType docType, List<cKeywordType> keywords, cDocumentAssignment assignment, int pkApplicationUser, cDocument.eDocStatus docStatus)
        {
            PutDocumentParameters putDocumentParams = new PutDocumentParameters(sUploadId,
                            documentAsBytes,
                            docType,
                            keywords,
                            new List<cDocumentAssignment> {assignment},
                            pkApplicationUser,
                            documentAsBytes.Length,
                            "tif",
                            @"image/tiff",
                            "0",
                            docStatus,
                            false,
                            51,
                            0);
            putDocumentParams.IdentityToken = Helper.GetIdentityToken();

            string address = ConfigurationManager.AppSettings["RestAddr"] + "/PutDocument";
            WebRequest req = WebRequest.Create(Uri.EscapeUriString(address));
            req.Method = "POST";
            req.ContentType = "application/json";
            
            //putDocumentParams.DocBytes = new byte[]{0,0,0};
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PutDocumentParameters),new List<System.Type>{typeof(PutDocumentParameters)});
            serializer.WriteObject(req.GetRequestStream(), putDocumentParams);
            req.GetRequestStream().Close();

            string oRet = null;

            using (WebResponse resp = req.GetResponse())
            {
                Stream stream = resp.GetResponseStream();
                if (stream != null)
                {
                    DataContractJsonSerializer ResponseSerializer = new DataContractJsonSerializer(typeof(string));
                    oRet = (string)ResponseSerializer.ReadObject(stream);
                }
            }

            return oRet;
            //return CompassDocumentsDAL.WebAPI.ProcessPostWithReturn<cDocument, PutDocumentParameters>(
            //    "/WebAPI/Documents/", putDocumentParams);
        }

        private cDocument DoWCFClassicCall(byte[] documentAsBytes, string sUploadId,
            cDocumentType docType, List<cKeywordType> keywords, cDocumentAssignment assignment, int pkApplicationUser, cDocument.eDocStatus docStatus)
        {
            cDocument oRet;

            ICompassDocumentsService svc = null;
            
            try
            {
                svc = Factory.GetService<ICompassDocumentsService>(CompassDocumentsDAL.Common.gConfigOptions, 10, false);

                if (CompassDocumentsDAL.Common.gConfigOptions.ChunkFileSizeInBytes - 5120 > documentAsBytes.Length)
                {
                    oRet = svc.PutDocument(Helper.GetIdentityToken(),
                        new PutDocumentParameters(sUploadId,
                            documentAsBytes,
                            docType,
                            keywords,
                            new List<cDocumentAssignment> {assignment},
                            pkApplicationUser,
                            documentAsBytes.Length,
                            "tif",
                            @"image/tiff",
                            "0",
                            docStatus,
                            false,
                            51,
                            0));
                }
                else
                {
                    ChunkFileAndUpload(sUploadId, ref svc, documentAsBytes);

                    oRet = svc.PutDocument(Helper.GetIdentityToken(),
                        new PutDocumentParameters(sUploadId,
                            docType,
                            keywords,
                            new List<cDocumentAssignment> {assignment},
                            pkApplicationUser,
                            documentAsBytes.Length,
                            "tif",
                            @"image/tiff",
                            "0",
                            docStatus,
                            false,
                            51,
                            0));
                }
            }
            catch (Exception ex)
            {
                Factory.CloseService(svc);
                throw ex;
            }
            finally
            {
                Factory.CloseService(svc);
            }
            return oRet;
        }

        private void LogResults(long ms)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Append("INSERT INTO CaptureLoadTestLog(pages, color, duration, username, method)");
            cmd.AppendLine(string.Format("VALUES({0}, {1}, {2}, '{3}','{4}')", PageCount, isDocColor? "1" : "0", ms, txtOBUsername.Text,rbREST.Checked? "REST": "WCF"));
            try
            {
                DBInterface.ExecuteSQLNonQuery(
                "Server=172.31.27.187;Database=CompassPilotAppFabricMonitoring;User Id=sa;Password=northwoods;", cmd.ToString());
            }
            catch
            {
            }
        }

        private void ChunkFileAndUpload(string sUploadId, ref ICompassDocumentsService svc, byte[] documentAsBytes)
        {
            int chunkSize = CompassDocumentsDAL.Common.gConfigOptions.ChunkFileSizeInBytes;
            int dChunkPosition = 0;
            int iChunkNumber = 0;


            if (chunkSize > 0)
            {
                //initially check that the chunk size isn't larger than the total size
                //for instance: a chunk size of 2MB and we only need 10K total
                if (dChunkPosition + chunkSize > documentAsBytes.Length)
                {
                    chunkSize = Convert.ToInt32(documentAsBytes.Length - dChunkPosition);
                }


                while (dChunkPosition < documentAsBytes.Length)
                {
                    iChunkNumber += 1;

                    //setup our chunk to fit the next set of bytes
                    byte[] bCurrentChunk = new byte[chunkSize];

                    //loop through our actual document and copy each byte over to our chunk array
                    for (int i = 0; i <= chunkSize - 1; i++)
                    {
                        bCurrentChunk[i] = documentAsBytes[dChunkPosition + i];
                    }

                    svc.UploadFileChunk(Helper.GetIdentityToken(), sUploadId, bCurrentChunk, iChunkNumber);

                    //move our position within the document to account for the bytes we just read
                    dChunkPosition += chunkSize;

                    //check to see if we're on the last chunk, if so adjust the size of the chunk to match how many bytes are left to read
                    if (dChunkPosition + chunkSize > documentAsBytes.Length - 1)
                    {
                        chunkSize = documentAsBytes.Length - dChunkPosition;
                    }
                }
            }

        }


        public void CreateUserToken(decimal ApplicationUserID, string UserName)
        {
            if (_userTokenCreated)
                return;

            string AppUserID = ApplicationUserID.ToString();
            string CompanyName = "AgencyName";
            string[] AppRoles = new string[]{};
            string SessionID = Guid.NewGuid().ToString();
            //Environment.UserName
            string IPAddress = "";
            string MACAddress = "";
            string MachineName = Environment.MachineName;
            string CountyCode = string.Empty;

            bool ldapUser = false;

            List<decimal> ProgramTypes = new List<decimal>();
            string sessiontoken = GetExistingIdentityToken(ApplicationUserID);

            CompassFrameworkLibrary.CompassIdentity id = 
                new CompassFrameworkLibrary.CompassIdentity(Convert.ToInt32(AppUserID), 
                    SessionID, 
                    UserName, 
                    IPAddress, 
                    MACAddress, 
                    MachineName, 
                    "Test", 
                    "User", 
                    sessiontoken, 
                    ProgramTypes,
                    ldapUser);
            id.County = CountyCode;

            CompassFrameworkLibrary.CompassPrincipal gp = new CompassFrameworkLibrary.CompassPrincipal(id, AppRoles);
            gp.CompanyName = CompanyName;

            System.Threading.Thread.CurrentPrincipal = gp;

            _userTokenCreated = true;
        }


        private string GetExistingIdentityToken(decimal ApplicationUserID)
        {
            string sql =
                string.Format(
                    "select top 1 AuthenticationToken from PublicAuthenticatedSession where fkApplicationUser = {0} order by KeepAlive desc",ApplicationUserID);

            return DataInterface.DBInterface.ScalarString(sql, string.Empty);
        }
        private DmsEntityTypes GetEntityTypes()
        {
            if (_entityTypes != null) return _entityTypes;

            string FilePath = Environment.ExpandEnvironmentVariables(@"%userprofile%\TestFiles" + @"\EntityTypes");
            byte[] EntityBytes = File.ReadAllBytes(FilePath);
            _entityTypes = CompassFrameworkLibrary.Common.BinaryDeserializeObject<DmsEntityTypes>(EntityBytes);

            //_entityTypes = CompassDocumentsDAL.CommonDBAccess.GetDMSEntityTypes(CacheRefreshLevel.UseAllCaching);

            //File.WriteAllBytes(@"c:\EntityTypes",CompassFrameworkLibrary.Common.BinarySerializeObject(_entityTypes));
            return _entityTypes;
        }

        private List<cDocumentType> GetBlankDocumentTypes()
        {
            return GetEntityTypes().DocumentTypes;
        }

        private List<cKeywordType> GetBlankKeywordTypes()
        {
            return GetEntityTypes().KeywordTypes;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {

            switch (cboPages.SelectedItem.ToString())
            {
                case "A":
                    PageCount = 1;
                    break;
                case "B":
                    PageCount = 5;
                    break;
                case "C":
                    PageCount = 20;
                    break;
                case "D":
                    PageCount = 50;
                    break;
                case "E":
                    PageCount = 100;
                    break;
            }
                
            string fileName = PageCount.ToString();
            
            if (rbBW.Checked)
            {
                fileName += "bw";
                isDocColor = false;
            }
            else
            {
                fileName += "c";
                isDocColor = true;
            }
            fileName += ".tif";


            lblWorking.Visible = true;
            this.BackColor = Color.Red;
            Application.DoEvents();

            int copies = int.Parse(txtCopies.Text);

            for (int i = 1; i <= copies; i++)
            {
                SaveDocument(fileName);
            }

            lblWorking.Visible = false;
            this.BackColor = SystemColors.Control;

        }

        private void cboPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnScan.Enabled = (cboPages.SelectedIndex != -1);
            pnlPending.Visible = (cboPages.SelectedIndex == -1);

        }

        private void txtCopies_Enter(object sender, EventArgs e)
        {
            txtCopies.SelectAll();
        }

        private void txtOBUsername_TextChanged(object sender, EventArgs e)
        {
            ShowCredentialsEntered();
        }

        private void txtOBPassword_TextChanged(object sender, EventArgs e)
        {
            ShowCredentialsEntered();
        }

        private void ShowCredentialsEntered()
        {
            if (txtOBUsername.Text.Length == "LoadTest0001".Length && txtOBPassword.Text == "password")
            {
                pnlPending.BackColor = Color.Orange;
            }
            else
            {
                pnlPending.BackColor = Color.Chartreuse;
            }
        }


    }
}
