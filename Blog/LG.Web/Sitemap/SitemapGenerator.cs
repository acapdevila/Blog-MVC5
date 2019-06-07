using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LG.Web.Sitemap
{
    public abstract class SitemapGenerator
    {
        private const string SitemapsNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
        /// <summary>
        /// The maximum number of sitemaps a sitemap index file can contain.
        /// </summary>
        private const int MaximumSitemapCount = 50000;
        /// <summary>
        /// The maximum number of sitemap nodes allowed in a sitemap file. The absolute maximum allowed is 50,000
        /// according to the specification. See http://www.sitemaps.org/protocol.html but the file size must also be
        /// less than 10MB. After some experimentation, a maximum of 25,000 nodes keeps the file size below 10MB.
        /// </summary>
        private const int MaximumSitemapNodeCount = 25000;
        /// <summary>The maximum size of a sitemap file in bytes (10MB).</summary>
        private const int MaximumSitemapSizeInBytes = 10485760;

        /// <summary>
        /// Gets the collection of XML sitemap documents for the current site. If there are less than 25,000 sitemap
        /// nodes, only one sitemap document will exist in the collection, otherwise a sitemap index document will be
        /// the first entry in the collection and all other entries will be sitemap XML documents.
        /// </summary>
        /// <param name="sitemapNodes">The sitemap nodes for the current site.</param>
        /// <returns>A collection of XML sitemap documents.</returns>
        protected virtual List<string> GetSitemapDocuments(IReadOnlyCollection<SitemapNode> sitemapNodes)
        {
            int num = (int)Math.Ceiling((double)sitemapNodes.Count / 25000.0);
            this.CheckSitemapCount(num);
            IEnumerable<KeyValuePair<int, IEnumerable<SitemapNode>>> sitemaps = Enumerable.Range(0, num).Select<int, KeyValuePair<int, IEnumerable<SitemapNode>>>((Func<int, KeyValuePair<int, IEnumerable<SitemapNode>>>)(x => new KeyValuePair<int, IEnumerable<SitemapNode>>(x + 1, sitemapNodes.Skip<SitemapNode>(x * 25000).Take<SitemapNode>(25000))));
            List<string> stringList = new List<string>(num);
            if (num > 1)
            {
                string sitemapIndexDocument = this.GetSitemapIndexDocument(sitemaps);
                stringList.Add(sitemapIndexDocument);
            }
            foreach (KeyValuePair<int, IEnumerable<SitemapNode>> keyValuePair in sitemaps)
            {
                string sitemapDocument = this.GetSitemapDocument(keyValuePair.Value);
                stringList.Add(sitemapDocument);
            }
            return stringList;
        }

        /// <summary>Gets the URL to the sitemap with the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        protected abstract string GetSitemapUrl(int index);

        /// <summary>
        /// Logs warnings when a sitemap exceeds the maximum size of 10MB or if the sitemap index file exceeds the
        /// maximum number of allowed sitemaps. No exceptions are thrown in this case as the sitemap file is still
        /// generated correctly and search engines may still read it.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        protected virtual void LogWarning(Exception exception)
        {
        }

        /// <summary>
        /// Gets the sitemap index XML document, containing links to all the sitemap XML documents.
        /// </summary>
        /// <param name="sitemaps">The collection of sitemaps containing their index and nodes.</param>
        /// <returns>The sitemap index XML document, containing links to all the sitemap XML documents.</returns>
        private string GetSitemapIndexDocument(IEnumerable<KeyValuePair<int, IEnumerable<SitemapNode>>> sitemaps)
        {
            XNamespace xnamespace = (XNamespace)"http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement xelement1 = new XElement(xnamespace + "sitemapindex");
            foreach (KeyValuePair<int, IEnumerable<SitemapNode>> sitemap in sitemaps)
            {
                DateTime? nullable = sitemap.Value.Select<SitemapNode, DateTime?>((Func<SitemapNode, DateTime?>)(x => x.LastModified)).Where<DateTime?>((Func<DateTime?, bool>)(x => x.HasValue)).DefaultIfEmpty<DateTime?>().Max<DateTime?>();
                XElement xelement2 = new XElement(xnamespace + "sitemap", new object[2]
                {
                    (object) new XElement(xnamespace + "loc", (object) this.GetSitemapUrl(sitemap.Key)),
                    nullable.HasValue ? (object) new XElement(xnamespace + "lastmod", (object) nullable.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")) : (object) (XElement) null
                });
                xelement1.Add((object)xelement2);
            }
            string sitemapXml = new XDocument(new object[1]
            {
                (object) xelement1
            }).ToString(Encoding.UTF8);
            this.CheckDocumentSize(sitemapXml);
            return sitemapXml;
        }

        /// <summary>
        /// Gets the sitemap XML document for the specified set of nodes.
        /// </summary>
        /// <param name="sitemapNodes">The sitemap nodes.</param>
        /// <returns>The sitemap XML document for the specified set of nodes.</returns>
        private string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xnamespace = (XNamespace)"http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement xelement1 = new XElement(xnamespace + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement xelement2 = new XElement(xnamespace + "url", new object[4]
                {
                    (object) new XElement(xnamespace + "loc", (object) Uri.EscapeUriString(sitemapNode.Url)),
                    !sitemapNode.LastModified.HasValue ? (object) (XElement) null : (object) new XElement(xnamespace + "lastmod", (object) sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    !sitemapNode.Frequency.HasValue ? (object) (XElement) null : (object) new XElement(xnamespace + "changefreq", (object) sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    !sitemapNode.Priority.HasValue ? (object) (XElement) null : (object) new XElement(xnamespace + "priority", (object) sitemapNode.Priority.Value.ToString("F1", (IFormatProvider) CultureInfo.InvariantCulture))
                });
                xelement1.Add((object)xelement2);
            }
            string sitemapXml = new XDocument(new object[1]
            {
                (object) xelement1
            }).ToString(Encoding.UTF8);
            this.CheckDocumentSize(sitemapXml);
            return sitemapXml;
        }

        /// <summary>
        /// Checks the size of the XML sitemap document. If it is over 10MB, logs an error.
        /// </summary>
        /// <param name="sitemapXml">The sitemap XML document.</param>
        private void CheckDocumentSize(string sitemapXml)
        {
            if (sitemapXml.Length < 10485760)
                return;
            this.LogWarning((Exception)new SitemapException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Sitemap exceeds the maximum size of 10MB. This is because you have unusually long URL's. Consider reducing the MaximumSitemapNodeCount. Size:<{0}>.", new object[1]
            {
                (object) sitemapXml.Length
            })));
        }

        /// <summary>
        /// Checks the count of the number of sitemaps. If it is over 50,000, logs an error.
        /// </summary>
        /// <param name="sitemapCount">The sitemap count.</param>
        private void CheckSitemapCount(int sitemapCount)
        {
            if (sitemapCount <= 50000)
                return;
            this.LogWarning((Exception)new SitemapException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Sitemap index file exceeds the maximum number of allowed sitemaps of 50,000. Count:<{1}>", new object[1]
            {
                (object) sitemapCount
            })));
        }
    }


}