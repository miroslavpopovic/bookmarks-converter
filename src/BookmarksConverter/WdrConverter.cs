using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace CodeMind.BookmarksConverter
{
    public class WdrConverter
    {
        public IList<Bookmark> Convert(string inputPath)
        {
            var result = new List<Bookmark>();

            var doc = new HtmlDocument();
            doc.Load(inputPath);

            var parentCategories = doc.DocumentNode.SelectNodes("//div[@class='category']");

            foreach (var categoryNode in parentCategories)
            {
                ProcessCategory(categoryNode, result);
            }

            return result;
        }

        private static void ProcessCategory(HtmlNode categoryNode, ICollection<Bookmark> bookmarks)
        {
            var categoryTitle = "Web/" + categoryNode.PreviousSibling.PreviousSibling.InnerText;

            var childCategories = categoryNode.SelectNodes("div[@class='item']");

            foreach (var category in childCategories)
            {
                ProcessChildCategories(category, categoryTitle, bookmarks);
            }
        }

        private static void ProcessChildCategories(HtmlNode categoryNode, string parentCategory, ICollection<Bookmark> bookmarks)
        {
            var categoryTitle = parentCategory + "/" + categoryNode.ChildNodes["h3"].InnerText;
            var bookmarkNodes = categoryNode.SelectNodes("ul/li");

            foreach (var bookmarkNode in bookmarkNodes)
            {
                ProcessBookmark(bookmarkNode, categoryTitle, bookmarks);
            }
        }

        private static void ProcessBookmark(HtmlNode bookmarkNode, string parentCategory, ICollection<Bookmark> bookmarks)
        {
            var link = bookmarkNode.SelectSingleNode("a[@href]");
            var description = bookmarkNode.SelectSingleNode("span[@class='desc']");

            var bookmark = new Bookmark
            {
                Url = link.Attributes["href"].Value,
                Title = ClearText(link.InnerText),
                Description = ClearText(description.InnerText),
                Category = parentCategory
            };

            bookmarks.Add(bookmark);

            var childBookmarks = bookmarkNode.SelectNodes("ul/li");
            if (childBookmarks == null) return;

            foreach (var childBookmarkNode in childBookmarks)
            {
                ProcessBookmark(childBookmarkNode, parentCategory, bookmarks);
            }
        }

        private static string ClearText(string value)
        {
            value = value.Trim();
            value = value.Replace("\r\n", string.Empty);
            return Regex.Replace(value, @"\s{2,}", " ");
        }
    }
}