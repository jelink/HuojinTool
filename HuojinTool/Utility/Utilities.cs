using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using System.Globalization;
using System.Windows.Forms;

namespace Microsoft.AppEx.Ingestion.Utilities
{
    public static class HTMLAgilityUtils
    {
        
        public static List<string> RestrictedHtmlNodes = new List<string> { "iframe", "embed", "script" };

        /// <summary>
        /// cleanup scope markers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanupText(string input)
        {
            string response = null;
            if (!string.IsNullOrEmpty(input))
            {
                //remove scope pattern
                string pattern = "#R##N#|#N#|#TAB#|\r|\n|\t";
                Regex rx = new Regex(pattern);
                response = rx.Replace(input, " ");

            }
            return response;
        }

        /// <summary>
        /// replace scope markers for parsing
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveScopeMarkers(string input)
        {
            string response = null;
            if (!string.IsNullOrEmpty(input))
            {
                //remove scope markers
                response = input.Replace("#R##N#", "\n");
                response = response.Replace("#N#", "\n");
                response = response.Replace("#TAB#", "\t");
            }
            return response;
        }

        /// <summary>
        /// Extract the data string from a byte array
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string GetDataString(byte[] byteArray, out Encoding feedencoding)
        {
            string dataString = null;
            bool encodingFound = false;
            feedencoding = Encoding.UTF8;

            dataString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            string encoderType = null;
            string matchPattern = "<?xml.*?>";
            Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
            // optimization reduce matching to first 100 characters.
            var matchStr = dataString;
            if (dataString.Length > 100)
            {
                matchStr = dataString.Substring(0, 100);
            }

            var match = re.Match(dataString);
            while (match.Success && !encodingFound)
            {
                for (var i = 0; i < match.Groups.Count; i++)
                {
                    string val = match.Groups[i].Value;
                    var str = "encoding=";
                    var idx = val.IndexOf(str, StringComparison.OrdinalIgnoreCase);
                    if (idx > str.Length)
                    {
                        var quoteStartIdx = val.IndexOf("\"", idx);
                        if (quoteStartIdx > 0)
                        {
                            quoteStartIdx++;
                            var quoteEndIdx = val.IndexOf("\"", quoteStartIdx);

                            if (quoteEndIdx > 0)
                            {
                                encoderType = val.Substring(quoteStartIdx, quoteEndIdx - quoteStartIdx);
                                //log (encoderType);

                                var encoding = Encoding.GetEncoding(encoderType);
                                if (encoding != null)
                                {
                                    //set the encoding used here
                                    feedencoding = encoding;
                                }
                            }
                        }

                        encodingFound = true;
                        break;
                    }
                }

                match = match.NextMatch();
            }

            //if no encoding found assume the string is encoded as utf-8 and return the same, otherwise returns the converted string
            var encodedStr = GetStringFromEncoding(feedencoding, byteArray);
            if (encodedStr != null)
            {
                dataString = encodedStr;
            }
            return dataString;
        }

        /// <summary>
        /// Extract the data bytes from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static byte[] GetDataBytes(string data)
        {

            byte[] dataString = null;
            bool encodingFound = false;
            string encoderType = null;
            Encoding feedencoding = Encoding.UTF8;
            dataString = feedencoding.GetBytes(data);
            string matchPattern = "<?xml.*?>";
            Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
            // optimization reduce matching to first 100 characters.
            var matchStr = data;
            if (data.Length > 100)
            {
                matchStr = data.Substring(0, 100);
            }

            var match = re.Match(data);
            while (match.Success && !encodingFound)
            {
                for (var i = 0; i < match.Groups.Count; i++)
                {
                    string val = match.Groups[i].Value;
                    var str = "encoding=";
                    var idx = val.IndexOf(str, StringComparison.OrdinalIgnoreCase);
                    if (idx > str.Length)
                    {
                        var quoteStartIdx = val.IndexOf("\"", idx);
                        if (quoteStartIdx > 0)
                        {
                            quoteStartIdx++;
                            var quoteEndIdx = val.IndexOf("\"", quoteStartIdx);

                            if (quoteEndIdx > 0)
                            {
                                encoderType = val.Substring(quoteStartIdx, quoteEndIdx - quoteStartIdx);
                                //log (encoderType);

                                var encoding = Encoding.GetEncoding(encoderType);
                                if (encoding != null && encoding.WebName != "utf-8")
                                {
                                    //set the encoding used here
                                    feedencoding = encoding;
                                    var encodedStr = feedencoding.GetBytes(data);
                                    if (encodedStr != null)
                                    {
                                        dataString = encodedStr;
                                    }
                                }
                            }
                        }

                        encodingFound = true;
                        break;
                    }
                }

                match = match.NextMatch();
            }

            //if no encoding found assume the string is encoded as utf-8 and return the same, otherwise returns the converted string
            return dataString;
        }

        /// <summary>
        /// get a unicode string from the byte array of specified encoding
        /// </summary>
        /// <param name="encoding">input encoding</param>
        /// <param name="byteArray">inout byte array</param>
        /// <returns></returns>
        public static string GetStringFromEncoding(Encoding encoding, byte[] byteArray)
        {
            string dataString = null;
            var start = 0;
            var numBytes = byteArray.Length;

            byte[] preambleBytes = encoding.GetPreamble();
            if (numBytes >= preambleBytes.Length)
            {
                for (var i = 0; i < preambleBytes.Length; i++)
                {
                    if (byteArray[i] != preambleBytes[i])
                    {
                        // if the preamble byte code match was purely 
                        // co-incidental then ignore any count observations.
                        start = 0;
                        break;
                    }

                    // need to explicitly skip the pre-amble as it's 
                    // not parsed out by the encoder/decoder
                    start++;
                }

                dataString = encoding.GetString(byteArray, start, numBytes - start);
            }

            return dataString;
        }

        /// <summary>
        /// Convert the string to a stream object
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>stream object</returns>
        public static Stream ToStream(this string str, Encoding feedencoding)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream, feedencoding);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Return the value for any one of the specified xml nodes, returns the first valid value found
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="node">node name to parse</param>
        /// <returns>XpathNavigator object for the specified node</returns>
        public static XPathNavigator GetFirstAvailableNode(XPathNavigator item, string node)
        {
            return GetFirstAvailableNode(item, node, null);
        }

        /// <summary>
        /// Return the value for any one of the specified xml nodes, returns the first valid value found
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="node">node name to parse</param>
        /// <param name="xmlNS">Namespaces used</param>
        /// <returns>XpathNavigator object for the specified node</returns>
        public static XPathNavigator GetFirstAvailableNode(XPathNavigator item, string node, XmlNamespaceManager xmlNS)
        {

            XPathNavigator nodeItem = null;
            if (xmlNS != null)
            {
                nodeItem = item.SelectSingleNode(node, xmlNS);
            }
            else
            {
                nodeItem = item.SelectSingleNode(node);
            }
            return nodeItem;
        }

        /// <summary>
        /// Return a list of all nodes with the name specified
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="node">node name to parse</param>
        /// <returns>XpathNavigator object for the specified node</returns>
        public static List<XPathNavigator> GetAllNodesWithName(XPathNavigator item, string node)
        {
            return GetAllNodesWithName(item, node, null);
        }

        /// <summary>
        /// Return a list of all nodes with the name specified
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="node">node name to parse</param>
        /// <param name="xmlNS">Namespaces used</param>
        /// <returns>XpathNavigator object for the specified node</returns>
        public static List<XPathNavigator> GetAllNodesWithName(XPathNavigator item, string node, XmlNamespaceManager xmlNS)
        {

            var nodeItems = new List<XPathNavigator>();
            XPathNodeIterator nodeIter = null;
            if (xmlNS != null)
            {
                nodeIter = item.Select(node, xmlNS);
            }
            else
            {
                nodeIter = item.Select(node);
            }

            if (nodeIter != null)
            {
                foreach (XPathNavigator xpathNavItem in nodeIter)
                {
                    nodeItems.Add(xpathNavItem);
                }
            }
            return nodeItems;
        }

        /// <summary>
        /// Return the value for any one of the specified xml nodes, returns the first valid value found
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="nodes">node names to parse</param>
        /// <returns>first valid node value found</returns>
        public static string GetFirstAvailableNodeValue(XPathNavigator item, string node)
        {
            return GetFirstAvailableNodeValue(item, node, null);
        }

        /// <summary>
        /// Return the value for any one of the specified xml nodes, returns the first valid value found
        /// </summary>
        /// <param name="item">RSS item</param>
        /// <param name="nodes">node names to parse</param>
        /// <param name="xmlNS">Namespaces used</param>
        /// <returns>first valid node value found</returns>
        public static string GetFirstAvailableNodeValue(XPathNavigator item, string node, XmlNamespaceManager xmlNS)
        {

            XPathNavigator nodeItem = null;
            if (xmlNS != null)
            {
                nodeItem = item.SelectSingleNode(node, xmlNS);
            }
            else
            {
                nodeItem = item.SelectSingleNode(node);
            }
            if (nodeItem != null)
            {
                //found, return the value.
                return nodeItem.Value;
            }

            return null;
        }

 

        private static string CleanupSnippet(string text, int textLen)
        {          
            if (!string.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "<!--.*-->", string.Empty);
                text = text.Trim();
            }

            if (textLen > text.Length)
                return text;

            string snip = text.Substring(0, textLen);

            while (!Char.IsWhiteSpace(text[textLen]) && textLen < text.Length)
            {
                snip += text[textLen];
                textLen++;
            }         
  
            snip += " ...";

            //remove extra spaces
            snip = Regex.Replace(snip, "\\s+", " ");

            return snip;            
        }

        /// <summary>
        /// Generates XML string from an XElement
        /// summary>
        /// <param name="xml">XElement source</param>
        public static string GetXmlString(this XElement xml)
        {
            // could also be any other stream
            StringBuilder sb = new StringBuilder();

            // Initialize a new writer settings
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            xws.ConformanceLevel = ConformanceLevel.Auto;

            using (XmlWriter xw = XmlWriter.Create(sb, xws))
            {
                // the actual writing takes place
                xml.WriteTo(xw);
            }

            return sb.ToString();

        }

        /// <summary>
        /// Generates XML string from an XElement
        /// summary>
        /// <param name="xml">XElement source</param>
        public static string GetXmlString(this XNode xml)
        {
            // could also be any other stream
            StringBuilder sb = new StringBuilder();

            // Initialize a new writer settings
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            xws.ConformanceLevel = ConformanceLevel.Auto;

            using (XmlWriter xw = XmlWriter.Create(sb, xws))
            {
                // the actual writing takes place
                xml.WriteTo(xw);
            }

            return sb.ToString();

        }

        public static XPathNavigator GetFirstItemNode(this List<XPathNavigator> itemList)
        {
            if (itemList == null)
            {
                return null;
            }

            foreach (var nodeItem in itemList)
            {
                if (nodeItem != null) return nodeItem;
            }
            
            return null;
        }

        private static string ReplaceTimeZoneAbbreviation(string datetimestr)
        {
            if (string.IsNullOrEmpty(datetimestr))
            {
                return datetimestr;
            }

            string pattern = "GMT|EDT";
            Regex rx = new Regex(pattern);
            if (!rx.IsMatch(datetimestr))
            {
                return datetimestr;
            }

            string rplcDateTimeStr = datetimestr;
            if (datetimestr.Contains("GMT"))
            {
                rplcDateTimeStr = datetimestr.Replace("GMT", "+0000");
            }
            else if (datetimestr.Contains("EDT"))
            {
                rplcDateTimeStr = datetimestr.Replace("EDT", "-0400");
            }

            return rplcDateTimeStr;
        }

        public static DateTime? ParseDatetimeString(string dateValue)
        {
            DateTime? updatedtimeVal = null;
            try
            {
                if (!string.IsNullOrEmpty(dateValue))
                {
                    string dateValTimZoneAbbrvReplaced = ReplaceTimeZoneAbbreviation(dateValue);

                    updatedtimeVal = DateTime.Parse(dateValTimZoneAbbrvReplaced);
                    if (updatedtimeVal.Value.Kind != DateTimeKind.Unspecified)
                    {
                        updatedtimeVal = TimeZoneInfo.ConvertTimeToUtc(updatedtimeVal.Value);
                    }
                }
            }
            catch (FormatException)
            { }

            return updatedtimeVal;
        }

        public static DateTime? ParseUTCDatetimeString(string dateValue, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return ParseDatetimeString(dateValue);
            }

            DateTime? dateTimeVal = null;
            try
            {
                if (!string.IsNullOrEmpty(dateValue))
                {
                    dateTimeVal = DateTime.ParseExact(dateValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
                }
            }
            catch (Exception) { }
            return dateTimeVal;
        }

        public static DateTime? ParseDatetimeString(string dateValue, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return ParseDatetimeString(dateValue);
            }

            DateTime? dateTimeVal = null;
            try
            {
                if (!string.IsNullOrEmpty(dateValue))
                {
                    dateTimeVal = DateTime.ParseExact(dateValue, format, CultureInfo.InvariantCulture).ToUniversalTime();
                }
            }
            catch (Exception) { }
            return dateTimeVal;
        }

        /// <summary>
        /// Parses the datetime string.
        /// </summary>
        /// <param name="dateValue">The date value.</param>
        /// <param name="format">The format.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static DateTime? ParseDatetimeString(string dateValue, string format, string offset)
        {
            if (string.IsNullOrEmpty(offset))
            {
                return ParseDatetimeString(dateValue, format);
            }

            DateTime? dateTimeVal = null;
            
            try
            {
                DateTime dateConverted;

                // Convert the dateValue into a datetime
                if (string.IsNullOrEmpty(format))
                {
                    dateConverted = DateTime.Parse(dateValue);
                }
                else
                {
                    dateConverted = DateTime.ParseExact(dateValue, format, CultureInfo.InvariantCulture);
                }

                // Ensure the offset conforms to the standard
                if (!offset.StartsWith("+") & !offset.StartsWith("-"))
                {
                    offset = offset.Insert(0, "+").PadRight(5, '0');
                }

                // Convert the offset into timespan
                TimeSpan offsetConverted = new TimeSpan(int.Parse(offset.Substring(0, 3)), int.Parse(offset.Substring(3, 2)), 0);
                dateTimeVal = new DateTimeOffset(dateConverted, offsetConverted).ToUniversalTime().DateTime;

            }
            catch (Exception) { }
            return dateTimeVal;
        }

        public static string EscapeHtml(string text)
        {
            if (text != null)
            {
                return HttpUtility.HtmlEncode(text);
            }
            return null;
        }

        public static string removeNRT(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"\t|\n|\r", "");
            return null;
        }

        public static string replaceNWithSpace(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, @"\t|\r", "");
                return Regex.Replace(text, @"\n", " ");
            }
            return null;
        }

        /// <summary>
        /// DEPRICATED: Use RemoveTag
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveLinkTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<\/?(?i)(link)[^>]*>", "");
            return null;
        }
        /// <summary>
        /// DEPRICATED: Use RemoveTag
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAnchorTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<\/?[aA][^>]*>", "");
            return null;
        }

        public static string RemoveHeaderTags(string htmlTxt)
        {
            if (!String.IsNullOrEmpty(htmlTxt))
                return Regex.Replace(htmlTxt, @"<\/?[hH0-9][^>]*>", "");
            return null;
        }

        /// <summary>
        /// Removes the all decendents of refnode with Nodename==tag but retains their inner content.
        /// </summary>
        /// <param name="refNode">reference node</param>
        /// <param name="tag">name of the tag to be removed</param>
        /// <returns></returns>
        public static XmlNode RemoveTag(XmlNode refNode, string tag)
        {

            List<XmlNode> nodeList = new List<XmlNode>();
            //get all 'tag' nodes that are descendents of refnode
            foreach (XmlNode node in refNode.SelectNodes(".//" + tag))
            {
                nodeList.Add(node);
            }
            //reverse the node list so that the each node comes before its parent
            nodeList.Reverse();

            //delete the 'tag' node and move its children to its parents
            foreach (XmlNode node in nodeList)
            {
                XmlNode parent = node.ParentNode;
                while (node.HasChildNodes)
                {
                    parent.InsertAfter(node.FirstChild, node);
                }
                parent.RemoveChild(node);
            }


            return refNode;
        }

        public static string GetURLQueryValue(string Url, string queryName)
        {
            string queryVal = string.Empty;
            Uri uri = new Uri(Url);
            string[] parameters = uri.Query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string parameter in parameters)
            {
                if (parameter.Contains(queryName + "="))
                {
                    string[] parts = parameter.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        queryVal = parts[1];
                        break;
                    }
                }
            }
            return queryVal;
        }

        public static String HtmlDecode(String htmlString)
        {

            if (!string.IsNullOrEmpty(htmlString))
            {
                return HttpUtility.HtmlDecode(htmlString);
            }
            else
            {
                return htmlString;
            }
        }

        /// <summary>
        /// Remove a Xpath from the XHtml
        /// </summary>
        /// <param name="xpath">xpath to remove</param>
        /// <param name="xHtml">xHtml Nodes</param>
        /// <returns></returns>
        public static XmlNode RemoveXpath(XmlNode node, string xpath)
        {
            if (node != null)
            {
                foreach (XmlNode selectedNode in node.SelectNodes(xpath))
                {
                    selectedNode.ParentNode.RemoveChild(selectedNode);
                }
            }
            return node;
        }


        public static string RemoveStrongTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<\/?(?i)(strong)[^>]*>", "");
            return null;
        }

        public static string RemoveEmptyPTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<p\s*>\s*</p\s*>|<p\s*/\s*>", "");
            return null;
        }

        public static string RemoveSpanTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<\/?(?i)(span)[^>]*>", "");
            return null;
        }

        public static string RemoveDetailTags(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return Regex.Replace(text, @"<\/?(?i)(detail)[^>]*>", "");
            return null;
        }

        public static string DecodeHtmlString(string htmlStr)
        {
            // purpose here is to remove all HTML encodings in the html text
            if (string.IsNullOrWhiteSpace(htmlStr))
            {
                return htmlStr;
            }
            int strLen = htmlStr.Length;
            int prvLen = 0;

            // decode as long as there is reduction in the string length
            // whenever there is a decoding the string length will reduce by at least a few characters.
            // e.g. S&P represented as S&amp;amp;P along with S&amp;P 
            // - in some cases encodings have found to be left out in the text
            while (prvLen != strLen)
            {
                prvLen = strLen;
                htmlStr = HttpUtility.HtmlDecode(htmlStr);
                strLen = htmlStr.Length;
            }
            return htmlStr;
        }


        /// <summary>
        /// remove junk characters from the string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string TrimText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "<!--.*-->", string.Empty);
                text = text.Trim();
            }
            //remove extra spaces
            text = Regex.Replace(text, "\\s+", " ");

            return text;
        }
        
    }
}
