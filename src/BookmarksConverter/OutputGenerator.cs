using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeMind.BookmarksConverter
{
    public class OutputGenerator
    {
        public void Generate(IEnumerable<Bookmark> bookmarks, string outputPath)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(bookmarks, settings);

            File.WriteAllText(outputPath, json);
        }
    }
}