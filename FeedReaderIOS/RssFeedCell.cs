using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RssFeedCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("RssFeedCell");

        public RssFeedCell( ) : base(UITableViewCellStyle.Default, Key) {
            
        }
    }
}