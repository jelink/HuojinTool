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
    class IniProcessor
    {
        public void Generate(Article article)
        {
            FileInfo fi = new FileInfo(article.IniSampleFileName);
            StringBuilder sb=new StringBuilder();
            
            if (fi.Extension == ".ini")
            {
                if (!string.IsNullOrEmpty(OpFile.GetFileToString(fi)))
                {
                    sb.Append(OpFile.GetFileToString(fi));
                    //sb.AppendFormat(OpFile.GetFileToString(fi), articles[0].Provider + articles[0].App, articles[0].App);
                    sb = sb.Replace("{TsvFile}", article.Provider + article.App + "Generic.tsv");
                    sb = sb.Replace("{ConfigFile}", article.Provider + article.App + "GenericConfig.xml");
                    sb = sb.Replace("{Environment}", article.Provider + article.App);
                    sb = sb.Replace("{App}", article.App);
                    OpFile.WriteStringToFile(article.Folder, article.IniOutputFileName, sb.ToString());
                }
                else
                {
                    MessageBox.Show("Can not loading the xaml file.");
                }

            }
        }
    }
}
