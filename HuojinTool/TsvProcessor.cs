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
    class TsvProcessor
    {
        public void Generate(List<Article> articles)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Article article in articles)
            {
                sb.AppendLine(article.FeedUrl);
                
            }

            OpFile.WriteStringToFile(articles[0].Folder, articles[0].TsvOutputFileName, sb.ToString());
        }
    }
}
