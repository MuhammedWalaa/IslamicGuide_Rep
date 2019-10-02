using System.Runtime.Remoting.Proxies;

namespace IslamicGuide.Data.ViewModels.Position
{
    public class BookContentVM
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentHTML { get; set; }
        public string AuthorName { get; set; }
        public string Title_English { get; set; }
        public string Content_English { get; set; }
        public string ContentHTML_English { get; set; }
        public string AuthorName_English { get; set; }

    }
}