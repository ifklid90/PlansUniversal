using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace PlansUniversal
{
	public class PhotosSearchController : UITableViewController, IUISearchResultsUpdating
	{
		ResultsTableController searchResultControler;
		public PhotosSearchControllerDelegate Delegate;
		public PhotosSearchController() : base()
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			AutomaticallyAdjustsScrollViewInsets = false;
			searchResultControler = new ResultsTableController();
			searchResultControler.RowWasSelected += RowWasSelected;
			UISearchController searchController = new UISearchController(searchResultControler);
			searchController.WeakSearchResultsUpdater = this;

			searchController.DimsBackgroundDuringPresentation = false;

			searchController.SearchBar.SizeToFit();
			TableView.TableHeaderView = searchController.SearchBar;

			DefinesPresentationContext = true;
			searchController.SearchBar.WeakDelegate = this;
			searchController.Active = true;


		}

		private void PhotosListDounloadFinish(object sender, EventArgs arg)
		{
			var newArg = (PhotosDounloadEventArg)arg;
			InvokeOnMainThread(() =>
			{
				searchResultControler.PhotosList = newArg.PhotosList;
				searchResultControler.TableView.ReloadData();	
			});

		}

		[Export("searchBarSearchButtonClicked:")]
		public virtual void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();		
			Flickr flickr = new Flickr();
			flickr.Search(searchBar.Text, PhotosListDounloadFinish);
		}

		[Export("updateSearchResultsForSearchController:")]
		public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			
		}

		private void RowWasSelected(object sender, EventArgs e)
		{
			int selectedrow = searchResultControler.TableView.IndexPathForSelectedRow.Row;
			var photo = searchResultControler.PhotosList[selectedrow];
			Delegate.ImageSelected(photo.LargeImage);
			NavigationController.PopViewController(true);
		}
	}
}
