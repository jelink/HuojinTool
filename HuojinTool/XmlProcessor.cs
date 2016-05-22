using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
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
    public class XmlProcessor
    {
        public XmlDocument LoadFeed(string feedurl)
        {
            var xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(feedurl);
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return xmldoc;
        }

        public void Generate(List<Article> articles)
        {

            StringBuilder sb = new StringBuilder();
            var samplexml = OpFile.GetFileToXml(articles[0].XmlSampleFileName);
            if (samplexml != null)
            {
                //var node = samplexml.SelectSingleNode("PartnerConfig/FeedConfig/Feed[@href='default']/ContentConfig[@type='article']/UniqueContentId/DefaultPrefix");
                var feedRootNode = samplexml.SelectSingleNode("PartnerConfig/FeedConfig");
                var bedrockRootNode = samplexml.SelectSingleNode("PartnerConfig/BedrockConfig");
                //var feedNode = articles[0].ConfigMetaData.SelectSingleNode("root/ContentType[@FeedType='" + articles[0].FeedType + "']/Feed");
                foreach(Article article in articles)
                {
                    //var guidNode = itemNode.SelectSingleNode("//DefaultPrefix");
                    //guidNode.InnerText= article.Market + "." + article.App + "." + article.Provider;
                    //samplexml.ImportNode(
                    var feedNode = GenerateFeed(article);
                    feedNode = samplexml.ImportNode(feedNode, true);
                    feedRootNode.AppendChild(feedNode);

                    var articleNode = this.GenerateArticleFeed(article);
                    articleNode = samplexml.ImportNode(articleNode, true);
                    bedrockRootNode.AppendChild(articleNode);

                }
                samplexml.Save(articles[0].XmlOutputFileName);
            }
            else
            {
                MessageBox.Show("Can not loading the xaml file.");
            }


        }

        public XmlNode GenerateFeed(Article article)
        {
            var itemNode = article.ConfigMetaData.SelectSingleNode("root/ContentType[@FeedType='" + article.FeedType + "']/Feed");
            itemNode.Attributes["href"].Value = HttpUtility.HtmlEncode(article.FeedUrl);
            itemNode.SelectSingleNode("//DefaultPrefix").InnerText = article.Market + "." + article.App + "." + article.Provider;
            return itemNode;
        }

        public XmlNode GenerateArticleFeed(Article article)
        {
            var itemNode = article.ConfigMetaData.SelectSingleNode("root/ContentType[@FeedType='" + article.FeedType + "']/ArticleFeed");
            itemNode.Attributes["href"].Value = HttpUtility.HtmlEncode(article.FeedUrl);
            itemNode.SelectSingleNode("SiteId").InnerText = article.SiteId;
            itemNode.SelectSingleNode("ParentFolderId").InnerText = article.ParentFolderId;
            itemNode.SelectSingleNode("WebPageID").InnerText = article.WebPageID;
            itemNode.SelectSingleNode("FolderPath").InnerText = string.Format("BingDaily/{0}/{1}/{2}/topStories/Article", article.Market, article.App, article.Provider);
            itemNode.SelectSingleNode("Market").InnerText = article.Market;
            return itemNode;
        }

        //public void ValidateFeed(XmlDocument xmldoc, ref Article article)
        //{
        //    if (xmldoc != null)
        //    {
        //        var newsitemlist = xmldoc.SelectNodes("NewsML/NewsItem");
        //        foreach (XmlNode newitem in newsitemlist)
        //        {
        //            var morelink = newitem.SelectSingleNode("NewsComponent/NewsComponent/NewsComponent/NewsLines/MoreLink");
        //            var imagemedia = newitem.SelectSingleNode("NewsComponent/NewsComponent/NewsComponent/ContentItem/DataContent/nitf/body/body.content/media[@media-type='image']/media-reference[@mime-type='image/jpeg']/@source");
        //            if (imagemedia == null)
        //            {
        //                newitem.SelectSingleNode("NewsComponent/NewsComponent/NewsComponent[Role/@FormalName='Supporting']/NewsComponent[1]/ContentItem/@Href");
        //            }
        //        }
        //    }

        //}
    }
}
