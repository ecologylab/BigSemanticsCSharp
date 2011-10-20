using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DomExtraction;
using Simpl.Fundamental.Net;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.generated.library.rss;
using ecologylab.semantics.metadata.scalar;

namespace RSSReader.ViewModels
{

  internal class MainWindowViewModel : ViewModelBase
  {

    private MMDExtractionBrowser                _browser;

    private ICommand                            _showFeedsCommand;

    private string                              _feedURLs;

    private ObservableCollection<FeedViewModel> _feeds;

    public MainWindowViewModel()
    {
      _browser = new MMDExtractionBrowser();
    }

    public string FeedURLs
    {
      get { return this._feedURLs; }
      set
      {
        this._feedURLs = value;
        this.NotifyPropertyChanged("FeedURLs");
      }
    }

    public ObservableCollection<FeedViewModel> Feeds
    {
      get { return this._feeds; }
      set
      {
        this._feeds = value;
        this.NotifyPropertyChanged("Feeds");
      }
    }

    public ICommand ShowFeedsCommand
    {
      get
      {
        if (this._showFeedsCommand == null)
        {
          this._showFeedsCommand = new MyCommand((param) => this.ShowFeeds1());
        }
        return this._showFeedsCommand;
      }
    }

    private void ShowFeeds1()
    {
      if (Feeds == null)
      {
        ObservableCollection<FeedViewModel> fakeFeeds = new ObservableCollection<FeedViewModel>();
        Item item = new Item();
        item.DcCreator = new MetadataString("samzenpus");
        item.DcTitle = new MetadataString("Federated Media Lands WordPress.com Deal");
        item.DcDescription = new MetadataString(@"Federated Media has just announced a partnership with WordPress.com. This deal follows shortly after Federated Media acquired the advertising network Lijit. The deal will include all of the blogs that are hosted under the company which comes to around 25 million. WordPress.com users will now be able to choose to allow sponsored posts, and Federated Media ads on their blog.");
        item.DcDate = new MetadataDate(DateTime.Parse("2011-10-20T00:25:00+00:00"));
        FeedViewModel vm = new RssItemFeedViewModel(item);
        fakeFeeds.Add(vm);
        fakeFeeds.Add(vm);
        Feeds = fakeFeeds;
      }
    }

    private async void ShowFeeds()
    {
      ObservableCollection<FeedViewModel> feeds = await this.DoGetFeeds();
      this.Feeds = feeds;
    }

    private async Task<ObservableCollection<FeedViewModel>> DoGetFeeds()
    {
      var tcs = new TaskCompletionSource<ObservableCollection<FeedViewModel>>();
      var newFeeds = new ObservableCollection<FeedViewModel>();
      if (FeedURLs != null)
      {
        foreach (string url in FeedURLs.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries))
        {
          var puri = new ParsedUri(url);
          Document feedDoc = (Document) await _browser.ExtractMetadata(puri);
          var feedViewModels = FeedViewModelFactory.GetFeedViewModels(feedDoc);
          foreach (var viewModel in feedViewModels)
          {
            newFeeds.Add(viewModel);
          }
        }
      }
      tcs.TrySetResult(newFeeds);
      return await tcs.Task;
    }

  }

}
