using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Creator { get; set; }
        public DateTime PubDate { get; set; }
        public string Link { get; set; }
    }
}