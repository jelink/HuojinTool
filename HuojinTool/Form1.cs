using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Form1 : Form
    {
        XmlProcessor XmlPro = new XmlProcessor();
        List<FileInfo> fileInfolist = new List<FileInfo>();
        List<Article> articles = new List<Article>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox.Text) & !string.IsNullOrEmpty(this.txtProvider.Text) & !string.IsNullOrEmpty(this.txtMarket.Text) &!string.IsNullOrEmpty(this.txtWebPageID.Text))
            {
                var IsIncludeEntityList = rbArticleAndEntityList.Checked ? true : false;
                foreach (string str in richTextBox.Lines)
                {
                    var article = new Article(cmboxDC.SelectedValue.ToString(), cmboxApp.SelectedValue.ToString(), str, cmboxFeedType.SelectedValue.ToString(), txtProvider.Text.Trim(), txtMarket.Text.Trim(),txtWebPageID.Text.Trim(), IsIncludeEntityList);
                    //var doc=XmlPro.LoadFeed(str);
                    //XmlPro.ValidateFeed(doc, ref article);
                    articles.Add(article);
                }


                XamlProcessor xamlPro = new XamlProcessor();
                xamlPro.Generate(this.articles[0]);
                TsvProcessor tsvPro = new TsvProcessor();
                tsvPro.Generate(this.articles);
                IniProcessor iniPro = new IniProcessor();
                iniPro.Generate(this.articles[0]);
                XmlProcessor xmlPro = new XmlProcessor();
                xmlPro.Generate(this.articles);
            }
            else
            {
                MessageBox.Show("FeedUrl and Provider can not empty");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtProvider.Text = "ABC";
            txtMarket.Text = "en-gb";
            txtWebPageID.Text = "250036798";

            List<DictionaryEntry> DCList = new List<DictionaryEntry>();
            DCList.Add(new DictionaryEntry("EMEA", "EMEA"));
            DCList.Add(new DictionaryEntry("BLU", "BLU"));
            DCList.Add(new DictionaryEntry("SG", "SG"));
            DCList.Add(new DictionaryEntry("JP", "JP"));

            this.cmboxDC.DataSource = DCList;
            this.cmboxDC.DisplayMember = "Key";
            this.cmboxDC.ValueMember = "Value";

            List<DictionaryEntry> AppList = new List<DictionaryEntry>();
            AppList.Add(new DictionaryEntry("News", "News"));
            AppList.Add(new DictionaryEntry("Sports", "Sports"));
            AppList.Add(new DictionaryEntry("Finance", "Finance"));
            AppList.Add(new DictionaryEntry("Travel", "Travel"));

            this.cmboxApp.DataSource = AppList;
            this.cmboxApp.DisplayMember = "Key";
            this.cmboxApp.ValueMember = "Value";

            List<DictionaryEntry> ContentTypeList = new List<DictionaryEntry>();
            XmlDocument xmlConfig = XmlHelper.LoadXmlFromFileorPath(System.Environment.CurrentDirectory + @"\ConfigMetaData.xml");
            if (xmlConfig != null & xmlConfig.HasChildNodes)
            {
                var contenttypelist = xmlConfig.SelectNodes("root/ContentType");
                foreach (XmlNode node in contenttypelist)
                {
                    ContentTypeList.Add(new DictionaryEntry(node.Attributes["FeedType"].Value, node.Attributes["FeedType"].Value));
                }
                this.cmboxFeedType.DataSource = ContentTypeList;
                this.cmboxFeedType.DisplayMember = "Key";
                this.cmboxFeedType.ValueMember = "Value";
            }
        }
    }
}
