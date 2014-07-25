using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FeedReaderIOS
{
    public class RootViewController : UIViewController {
        private UITextField _urlField;
        private UIButton _addButton;
        private UITableView _tableView;
        private LoadingOverlay _overlay;
        private List<RssFeed> _feeds;

        public RootViewController( ) {
            _feeds = new List<RssFeed>( );
        }

        public override void ViewDidLoad( ) {
            base.ViewDidLoad( );

            Title = "Rss Feeds";

            View.BackgroundColor = UIColor.LightGray;

            var h = 31.0f;
            var w = View.Bounds.Width - 20;

            _urlField = new UITextField {
                Placeholder = "Enter a feed url",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new RectangleF( 10, 75, w, h ),
                AutocapitalizationType = UITextAutocapitalizationType.None,
                KeyboardType = UIKeyboardType.Url,
                ShouldReturn = field =>
                {
                    field.ResignFirstResponder();
                    return true;
                }
            };

            _addButton = UIButton.FromType( UIButtonType.RoundedRect );
            _addButton.Frame = new RectangleF(10, 120, w, 44);
            _addButton.SetTitle("Add", UIControlState.Normal);
            _addButton.TouchUpInside += async (sender, args) =>
            {
                _overlay = new LoadingOverlay(UIScreen.MainScreen.Bounds);
                View.AddSubview(_overlay);
                await AddNewFeedAsync(_urlField.Text);
                _urlField.Text = string.Empty;
                _tableView.ReloadData();
                _overlay.Hide();
            };

            _tableView = new UITableView {
                Frame = new RectangleF( 0, 180, View.Bounds.Width, View.Bounds.Height - 180 ),
                Source = new RssFeedSource(_feeds, this)
            };

            View.AddSubviews(_urlField, _addButton, _tableView);
        }

        public async Task AddNewFeedAsync( string url ) {
            if ( string.IsNullOrWhiteSpace( url ) ) {
                new UIAlertView("Add A Valid Url", "Please add a valid feed url to the text box", null, "OK").Show();
                _urlField.Text = string.Empty;
                _urlField.BecomeFirstResponder( );
                return;
            }

            var urlText = url.StartsWith( "http://" ) ? url : string.Concat( "http://", url );

            using ( var client = new HttpClient( ) ) {
                var newFeed = new RssFeed {
                    DateAdded = DateTime.Now.ToString( )
                };
                var feedString = await client.GetStringAsync( urlText );
                var doc = XDocument.Parse( feedString );

                var title = doc.Descendants( "channel" ).Elements( ).FirstOrDefault( e => e.Name == "title" ).Value;

                newFeed.Title = title;

                XNamespace dc = "http://purl.org/dc/elements/1.1/";
                var items = ( from item in doc.Descendants( "item" )
                    select new RssItem {
                        Title = item.Element("title").Value,
                        PubDate = DateTime.Parse(item.Element("pubDate").Value),
                        Creator = item.Element(dc + "creator").Value,
                        Link = item.Element("link").Value
                    } ).ToList();

                newFeed.Items.AddRange( items );

                _feeds.Add( newFeed );
            }
        }
    }
}