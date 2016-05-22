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
using System.Xml.XPath;

namespace HuojinTool
{
    public class ConfigurationCreateProcessor
    {



    }



    public class XamlProcessor
    {
        public void Generate(Article article)
        {
            FileInfo fi = new FileInfo(article.XamlSampleFileName);

            if (fi.Extension == ".xaml")
            {
                if (!string.IsNullOrEmpty(OpFile.GetFileToString(fi)))
                {
                    var str = OpFile.GetFileToString(fi);
                    str = str.Replace("{0}", article.Provider + article.App + "Generic");
                    OpFile.WriteStringToFile(article.Folder, article.XamlOutputFileName, str);
                }
                else
                {
                    MessageBox.Show("Can not loading the xaml file.");
                }

            }
        }
    }
}
