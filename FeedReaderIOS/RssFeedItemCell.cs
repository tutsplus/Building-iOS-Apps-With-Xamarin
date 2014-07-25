using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RssFeedItemCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("RssFeedItemCell");

        public RssFeedItemCell( ) : base(UITableViewCellStyle.Subtitle, Key) {
            
        }
    }
}