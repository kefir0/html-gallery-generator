using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace HtmlGalleryGenerator
{
    public static class GalleryProcessor
    {
        #region public static methods

        public static void CreateGallery(string[] files, string title, string serverImagePath,
            string outputFileNames, int pageSize, string targetPath)
        {
            files = files.Select(x => serverImagePath + x).ToArray();
            var photos = files.Select((x, i) => new Photo {id = i + 1, src = x}).ToArray();
            var groups = photos.GroupBy(x => (x.id - 1)/pageSize);
            var galleries = groups.Select(g => new Gallery {Photos = g.ToArray(), Title = title}).ToArray();
            var galleryId = 0;
            var pages =
                galleries.Select(
                    (x, i) =>
                        new Page
                        {
                            id = i + 1,
                            url = string.Format("{1}{0}.htm", i == 0 ? "" : "_" + i.ToString(), outputFileNames)
                        })
                    .ToArray();
            foreach (var gallery in galleries)
            {
                gallery.PageId = galleryId + 1;
                gallery.Pages = pages;
                var html = Transform(@"photo_page_template.xslt", gallery);
                var pagePath = Path.Combine(targetPath, pages[galleryId].url);
                File.WriteAllText(pagePath, html);
                galleryId++;
            }
        }

        #endregion

        #region private static methods

        private static string Transform(string templatePath, object report)
        {
            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb))
            {
                new XmlSerializer(report.GetType()).Serialize(stream, report);
            }
            //sb.ToString().Dump();

            var resultSb = new StringBuilder();
            using (var xmlReader = XmlReader.Create(new StringReader(sb.ToString())))
            using (var templateStream = File.OpenRead(templatePath))
            using (var xsltReader = XmlReader.Create(templateStream))
            using (var resultWriter = new StringWriter(resultSb))
            using (var xmlWriter = XmlWriter.Create(resultWriter))
            {
                var t = new XslCompiledTransform();
                t.Load(xsltReader);
                t.Transform(xmlReader, xmlWriter);
                return resultSb.ToString();
            }
        }

        #endregion
    }

    #region Nested type: Gallery

    public class Gallery
    {
        #region public properties and indexers

        public Photo[] Photos { get; set; }
        public int PageId { get; set; }
        public Page[] Pages { get; set; }
        public string Title { get; set; }

        #endregion
    }

    #endregion

    #region Nested type: Page

    public class Page
    {
        #region public properties and indexers

        public int id { get; set; }
        public string url { get; set; }

        #endregion
    }

    #endregion

    #region Nested type: Photo

    public class Photo
    {
        #region public properties and indexers

        public string src { get; set; }
        public int id { get; set; }

        #endregion
    }

    #endregion
}