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
    class OpFile
    {
        public static string GetFileToString(FileInfo fi)
        {
            try
            {
                using (StreamReader sr = fi.OpenText())
                {
                    var str = sr.ReadToEnd();
                    sr.Close();
                    return str;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static XmlDocument GetFileToXml(string filename)
        {
            try
            {
                var xmldoc = new XmlDocument();
                xmldoc.Load(filename);
                return xmldoc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static void WriteStringToXaml(string data, Article article)
        {
            if (string.IsNullOrEmpty(data) && article != null)
            {
                WriteStringToFile(article.Folder, article.XamlOutputFileName, data);
            }
        }

        public static void WriteStringToTsv(string data, Article article)
        {
            if (string.IsNullOrEmpty(data) && article != null)
            {
                WriteStringToFile(article.Folder, article.TsvOutputFileName, data);
            }
        }

        public static void WriteStringToIni(string data, Article article)
        {
            if (string.IsNullOrEmpty(data) && article != null)
            {
                WriteStringToFile(article.Folder, article.IniOutputFileName, data);
            }
        }

        public static void WriteStringToXml(string data, Article article)
        {
            if (string.IsNullOrEmpty(data) && article != null)
            {
                WriteStringToFile(article.Folder, article.XmlOutputFileName, data);
            }
        }

        public static void WriteStringToFile(DirectoryInfo folder,string filename,string data)
        {
            if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(data))
            {
                if (!folder.Exists)
                {
                    folder.Create();
                }
                
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Write(data);
                            sw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }


}
