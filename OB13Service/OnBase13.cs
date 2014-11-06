using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;

namespace OB13Service
{
    public class OnBase13
    {
        public void WriteDocument(byte[] DocumentBytes)
        {
            Application app = ConnectionManager.GetConnection("gfish", "gfish").ApplicationObject;
            StoreNewDocumentProperties props = createProperties(app);
            PageData pageData = app.Core.Storage.CreatePageData(new MemoryStream(DocumentBytes), "tif");

            Document NewDocument = app.Core.Storage.StoreNewDocument(pageData, props);
        }

        public void WriteDocument(string ImageFile)
        {
            Application app = ConnectionManager.GetConnection("gfish", "gfish").ApplicationObject;
            StoreNewDocumentProperties props = createProperties(app);
            //PageData pageData = app.Core.Storage.CreatePageData(ImageFile, "tif");

            Document NewDocument = app.Core.Storage.StoreNewDocument(new List<string> { ImageFile }, props);
        }

        private StoreNewDocumentProperties createProperties(Application app)
        {
            Storage storage = app.Core.Storage;

            DocumentType documentType = app.Core.DocumentTypes.Find("CSEA FISC Audit");
            FileType fileType = app.Core.FileTypes.Find("Image File Format");

            StoreNewDocumentProperties props = storage.CreateStoreNewDocumentProperties(documentType, fileType);

            KeywordType kwt = app.Core.KeywordTypes.Find("SSN");

            props.AddKeyword(GetKeyword(app,"SSN","111-11-1111"));
            props.AddKeyword(GetKeyword(app,"First Name", "Johnny"));
            props.AddKeyword(GetKeyword(app,"Last Name", "Northwoods"));
            props.AddKeyword(GetKeyword(app,"Compass Number", "OH123000000101"));
            //props.AddKeyword("Doc Date",@"10/13/2014");

            props.DocumentDate = DateTime.Now;
            props.Options = StoreDocumentOptions.SkipWorkflow;

            return props;
        }

        private Keyword GetKeyword(Application app, string keywordName, string keywordValue)
        {
            KeywordType kwt = app.Core.KeywordTypes.Find(keywordName);
            Keyword kw;

            switch (kwt.DataType)
            {
                case KeywordDataType.Currency:
                case KeywordDataType.SpecificCurrency:
                case KeywordDataType.Numeric20:
                case KeywordDataType.Numeric9:
                    kw = kwt.CreateKeyword(decimal.Parse(keywordValue));
                    break;
                case KeywordDataType.Date:
                    kw = kwt.CreateKeyword(DateTime.Parse(keywordValue).Date);
                    break;
                case KeywordDataType.DateTime:
                    kw = kwt.CreateKeyword(DateTime.Parse(keywordValue));
                    break;
                case KeywordDataType.FloatingPoint:
                    kw = kwt.CreateKeyword(double.Parse(keywordValue));
                    break;
                default:
                    kw = kwt.CreateKeyword(keywordValue);
                    break;
            }
            return kw;
        }

    }
}
