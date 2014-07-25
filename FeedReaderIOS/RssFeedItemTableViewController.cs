using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RssFeedItemTableViewController : UITableViewController {
        private RssFeed _feed;

        public RssFeedItemTableViewController( RssFeed feed ) {
            _feed = feed;
        }

        public override void ViewDidLoad( ) {
            base.ViewDidLoad( );

            Title = _feed.Title;
            TableView.Source = new RssFeedItemSource( _feed.Items );
        }
    }
}