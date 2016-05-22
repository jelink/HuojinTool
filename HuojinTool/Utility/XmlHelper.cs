

namespace Microsoft.AppEx.Ingestion.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml;

    public class XmlHelper
    {
        public static XmlDocument LoadXmlFromFileorPath(string FileorPath)
        {
            FileorPath = FileorPath.Trim();
            XmlDocument xmldoc = new XmlDocument();
            if (FileorPath.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    xmldoc.Load(FileorPath);
                    return xmldoc;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Get a value of some xpath from node
        /// </summary>
        /// <param name="element">XMl node of document</param>
        /// <param name="xpath">path to traverse</param>
        /// <returns>value of path</returns>
        public static string GetFromElement(XElement element, string xpath)
        {
            string ret = null;
            //XElement selectedElement = element.XPathSelectElement(xpath);
            //if (selectedElement != null)
            //{
            //    ret = selectedElement.Value;
            //}

            return ret;
        }

        /// <summary>
        /// Get a value of some xpath from node
        /// </summary>
        /// <param name="node">XMl node of document</param>
        /// <param name="xpath">path to traverse</param>
        /// <returns>value of path</returns>
        public static string GetFromNode(XmlNode node, string xpath, XmlNamespaceManager xmlnsManager=null, bool text=true, bool isNtoSpace=false)
        {
            string ret = string.Empty;

            if (!string.IsNullOrEmpty(xpath))
            {
                XmlNode selectedNode = node.SelectSingleNode(xpath, xmlnsManager);
                if (selectedNode != null && selectedNode.InnerText != null)
                {
                    if (text)
                    {
                        ret = selectedNode.InnerText.Trim();  // Bug 444455: Trim() used to remove empty space at the begining and End which was causing misalignment.
                    }
                    else
                    {
                        ret = selectedNode.InnerXml.Trim();
                    }
                }
                if (isNtoSpace)
                {
                    return HTMLAgilityUtils.replaceNWithSpace(ret);
                }
                else
                {
                    return HTMLAgilityUtils.removeNRT(ret);
                }
            }
            return ret;
        }

        public static string GetOuterXmlFromNode(XmlNode node, string xpath, XmlNamespaceManager xmlnsManager = null)
        {
            string ret = string.Empty;
            XmlNode selectedNode = node.SelectSingleNode(xpath, xmlnsManager);
            if (selectedNode != null)
            {
                ret = selectedNode.OuterXml;
            }

            return HTMLAgilityUtils.removeNRT(ret);
        }

        /// <summary>
        /// Get a vaule from complet xml document
        /// </summary>
        /// <param name="doc">Loaded xml document</param>
        /// <param name="nodeName">Path of node</param>
        /// <param name="xmlnsManager">namespace manager</param>
        /// <returns>value of path</returns>
        public static string GetFromXML(XmlDocument doc, string nodeName, System.Xml.XmlNamespaceManager xmlnsManager=null, bool text=true)
        {
            string temp = string.Empty;
            XmlNode node = null;
            if (xmlnsManager != null)
            {
                node = doc.SelectSingleNode(nodeName, xmlnsManager);
            }
            else
            {
                node = doc.SelectSingleNode(nodeName);
            }

            if (node != null)
            {
                temp = text ? node.InnerText : node.InnerXml;
            }

            return HTMLAgilityUtils.removeNRT(temp);
        }

        public static string GetAttributeFromNode(XmlNode node, string attribute, XmlNamespaceManager xmlnsManager=null)
        {
            if (node != null && node.Attributes[attribute] != null && !string.IsNullOrEmpty(node.Attributes[attribute].Value))
            {
                return node.Attributes[attribute].Value;
            }

            return null;
        }


        public static string RemoveXmlTag(string xmlContent)
        {
            string contentToReturn = xmlContent;
            // TODO: to use a regex
            string removal = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            if (!string.IsNullOrEmpty(xmlContent))
            {
                contentToReturn = xmlContent.Replace(removal, string.Empty);
            }

            return contentToReturn;
        }

        public static string GetAttributeAndNode(XmlNode node, XmlNamespaceManager xmlnsManager, string xpath, string attribute)
        {
            string data = string.Empty;
            XmlNode askedNode = node.SelectSingleNode(xpath, xmlnsManager);
            if (askedNode != null)
            {
                data = XmlHelper.GetAttributeFromNode(askedNode, attribute, xmlnsManager);
            }

            return HTMLAgilityUtils.removeNRT(data);
        }
    }
}
