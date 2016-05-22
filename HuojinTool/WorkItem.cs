using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.AppEx.Ingestion.Utilities;

namespace HuojinTool
{
    public class Article
    {
        public XmlDocument ConfigMetaData = XmlHelper.LoadXmlFromFileorPath(System.Environment.CurrentDirectory + @"\ConfigMetaData.xml");
        public Bedrock bedrock; 
        public DirectoryInfo Folder { get; set; }
        public string TsvSampleFileName { get; set; }
        public string XamlSampleFileName { get; set; }
        public string IniSampleFileName { get; set; }
        public string XmlSampleFileName { get; set; }

        public string TsvOutputFileName { get; set; }
        public string XamlOutputFileName { get; set; }
        public string IniOutputFileName { get; set; }
        public string XmlOutputFileName { get; set; }

        public string WebService { get; set; }
        public string SiteId { get; set; }
        public string ParentFolderId { get; set; }
        public string App { get; set; }
        public string FeedUrl { get; set; }
        public string FeedType { get; set; }
        public string Provider { get; set; }
        public string Market { get; set; }
        public string WebPageID { get; set; }
        public bool IsIncludeEntityList { get; set; }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public string Published { get; set; }
        public string Updated { get; set; }
        public string Body { get; set; }
        public string MoreLink { get; set; }
        public Image Image { get; set; }

        public Article(string datacenter, string app, string feedurl,string feedtype,string provider,string market,string webpageid,bool isincludeentitylist)
        {
            bedrock = new Bedrock();
            this.WebService = bedrock.WebService[datacenter];
            this.SiteId = bedrock.SiteId[datacenter];
            this.ParentFolderId = bedrock.ParentFolderId[datacenter];
            this.App = app;
            this.FeedUrl = feedurl;
            this.FeedType = feedtype;
            this.Provider = provider;
            this.Market = market;
            this.WebPageID = webpageid;
            this.IsIncludeEntityList = isincludeentitylist;

            this.Folder = new DirectoryInfo(System.Environment.CurrentDirectory + @"\" + provider + app);
            this.TsvOutputFileName = this.Folder.FullName + @"\" + this.Provider + this.App + "Generic.tsv";
            this.XamlOutputFileName = this.Folder.FullName + @"\" + this.Provider + this.App + "Generic.xaml";
            this.IniOutputFileName = this.Folder.FullName + @"\" + this.Provider + this.App + "Generic.xaml.params.ini";
            this.XmlOutputFileName = this.Folder.FullName + @"\" + this.Provider + this.App + "GenericConfig.xml";


            if (ConfigMetaData != null & ConfigMetaData.HasChildNodes)
            {
                XmlNode itemNode = ConfigMetaData.SelectSingleNode("root/ContentType[@FeedType='" + feedtype + "']/ConfiguationLocation");
                this.XamlSampleFileName = itemNode.SelectSingleNode("XamlFile").InnerXml;
                this.IniSampleFileName = itemNode.SelectSingleNode("IniFile").InnerXml;
                this.TsvSampleFileName = itemNode.SelectSingleNode("TsvFile").InnerXml;
                this.XmlSampleFileName = itemNode.SelectSingleNode("XmlFile").InnerXml;
            }
        }

        public Article(string webservice, string app, string feedurl, string provider)
        {
            this.WebService = webservice;
            this.App = app;
            this.FeedUrl = feedurl;
            this.Provider = provider;
        }

    }

    public class Image
    {
        public string Url { get; set; }
        public string Copyright { get; set; }
        public string Attribution { get; set; }
        public string Alttext { get; set; }
        public string Imgtext { get; set; }
    }

    public class Bedrock
    {
        public Dictionary<string, string> WebService = new Dictionary<string, string>();
        public Dictionary<string, string> SiteId = new Dictionary<string, string>();
        public Dictionary<string, string> ParentFolderId = new Dictionary<string, string>();

        public Bedrock()
        {
            WebService.Add("EMEA", "http://bedrockemea:202/Editorial/WebServices");
            WebService.Add("BLU", "http://bedrockblu:616/Editorial/WebServices");
            WebService.Add("SG", "http://bedrocksg:414/Editorial/WebServices");
            WebService.Add("JP", "http://bedrockjp:616/Editorial/WebServices");

            SiteId.Add("EMEA", "250000715");
            SiteId.Add("BLU", "32125811");
            SiteId.Add("SG", "250000216");
            SiteId.Add("JP", "250000217");

            ParentFolderId.Add("EMEA", "250046960");
            ParentFolderId.Add("BLU", "265439458");
            ParentFolderId.Add("SG", "250033251");
            ParentFolderId.Add("JP", "250007877");
        }
    }
}
