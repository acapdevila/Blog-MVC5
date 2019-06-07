using System.IO;
using System.Text;
using System.Xml.Linq;

namespace LG.Web.Sitemap
{
    public static class XDocumentExtensions
    {
        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the XML document in the specified encoding.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the XML document.
        /// </returns>
        public static string ToString(this XDocument document, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = (StringWriter)new StringWriterWithEncoding(stringBuilder, encoding))
                document.Save((TextWriter)stringWriter);
            return stringBuilder.ToString();
        }
    }
}