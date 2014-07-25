using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RssFeedItemSource : UITableViewSource {
        private List<RssItem> _items;

        public RssFeedItemSource( List<RssItem> items  ) {
            _items = items;
        }

        public override UITableViewCell GetCell( UITableView tableView, NSIndexPath indexPath ) {
            var cell = tableView.DequeueReusableCell( RssFeedItemCell.Key ) as RssFeedItemCell;
            if(cell == null)
                cell = new RssFeedItemCell();

            cell.TextLabel.Text = _items[indexPath.Row].Title;
            cell.DetailTextLabel.Text = string.Format( "{0} on {1}", _items[indexPath.Row].Creator, _items[indexPath.Row].PubDate );

            return cell;
        }

        public override int RowsInSection( UITableView tableview, int section ) {
            return _items.Count;
        }

        public override void RowSelected( UITableView tableView, NSIndexPath indexPath ) {
            var item = _items[indexPath.Row];

            var url = new NSUrl(item.Link);
            UIApplication.SharedApplication.OpenUrl( url );
            tableView.DeselectRow( indexPath, false );
        }
    }
}