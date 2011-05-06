using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AttachedCommandBehavior;
using ecologylab.interactive.CommandBehaviours;
using ecologylab.interactive.Utils;
using ecologylab.net;
using ecologylab.semantics.generated.library;
using ecologylabInteractiveSemantics.ecologylab.interactive.Behaviours;
using MetadataUISandbox.ActivationBehaviours;

namespace  ecologylab.semantics.interactive.Controls
{


	/// <summary>
	/// Interaction logic for WikiPage.xaml
	/// </summary>
	public partial class WikiPage : UserControl
	{
	    private Logger logger = new Logger();
	    private WikipediaPage page;
        public String Title { get; set; }

	    public WikipediaPage Page
	    {
	        get { return page; }
            set
            {
                page = value;
                //Set data context for the Intro Para.
                WikiTitleTextBlock.Text = page.Title.Value;
                IntroPara.DataContext = page;
                IntroParaText.MinWidth = 250;
            }
	    }

	    public WikiPage()
	    {
	        this.InitializeComponent();

	        SingleTapBehaviour clickForMoreBehaviour = new SingleTapBehaviour();

	        clickForMoreBehaviour.Command = new SimpleCommand()
	        {
	            ExecuteDelegate = commandParams =>
	            {
	                Console.WriteLine("Setting context for scroll viewer." + DateTime.Now);
                    ScrollViewPage.DataContext = Page;
                    IntroPara.Visibility = Visibility.Collapsed;
                    ScrollViewPage.Visibility = Visibility.Visible;
	                ScrollViewPage.Width = 250;
	                CurrentVisualState = VisualState.FullArticle;
                    Console.WriteLine("Scroll viewer context set." + DateTime.Now);
	            }
	        };

	        clickForMoreBehaviour.Attach(ClickForMore);
	    }

        public WikiPage(String title)
            : this()
        {
            Title = title;
            WikiTitleTextBlock.Text = Title;

        }

	    public WikiPage(WikipediaPage page)
            :this()
        {
            Page = page;
        }

        public WikiPage(ParsedUri parsedUri, String title)
            :this(title)
        {
            this.parsedUri = parsedUri;
        }

        public void ShowFullArticle()
        {
            
        }

        public void ToggleDetailVisibility()
        {
            switch(CurrentVisualState)
            {
                case VisualState.TitleOnly:
                    if (ScrollViewPage.DataContext == null)
                    {
                        IntroPara.Visibility = Visibility.Visible;
                        CurrentVisualState = VisualState.IntroParagraph;
                    }
                    else
                    {
                        ScrollViewPage.Visibility = Visibility.Visible;
                        CurrentVisualState = VisualState.FullArticle;
                    }
                    break;
                case VisualState.IntroParagraph:
                    IntroPara.Visibility = Visibility.Hidden;
                    CurrentVisualState = VisualState.TitleOnly;
                    break;
                case VisualState.FullArticle:
                    ScrollViewPage.Visibility = Visibility.Hidden;
                    CurrentVisualState = VisualState.TitleOnly;
                    break;

            }
            Console.WriteLine("VisualState is now " + CurrentVisualState);
        }

        public enum VisualState{ TitleOnly, IntroParagraph, IntroParagraphWithImages, FullArticle }

        #region CurrentVisualState

        /// <summary>
        /// CurrentVisualState Dependency Property
        /// </summary>
        public static readonly DependencyProperty CurrentVisualStateProperty =
            DependencyProperty.Register("CurrentVisualState", typeof(VisualState), typeof(WikiPage),
                new FrameworkPropertyMetadata((VisualState)VisualState.TitleOnly,
                    new PropertyChangedCallback(OnCurrentVisualStateChanged)));
        private net.ParsedUri parsedUri;

        /// <summary>
        /// Gets or sets the CurrentVisualState property. This dependency property 
        /// indicates the current visual state of the page.
        /// </summary>
        public VisualState CurrentVisualState
        {
            get { return (VisualState)GetValue(CurrentVisualStateProperty); }
            set { SetValue(CurrentVisualStateProperty, value); }
        }

        /// <summary>
        /// Handles changes to the CurrentVisualState property.
        /// </summary>
        private static void OnCurrentVisualStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WikiPage target = (WikiPage)d;
            VisualState oldCurrentVisualState = (VisualState)e.OldValue;
            VisualState newCurrentVisualState = target.CurrentVisualState;
            target.OnCurrentVisualStateChanged(oldCurrentVisualState, newCurrentVisualState);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CurrentVisualState property.
        /// </summary>
        protected virtual void OnCurrentVisualStateChanged(VisualState oldCurrentVisualState, VisualState newCurrentVisualState)
        {
            //Do Transitions between visual states.

        }

        #endregion

        

        public DependencyObject AcceptableObject(DependencyObject obj)
        {
            DependencyObject result;
            logger.Log("\tHitTest on : " + obj);
            if ((result = obj) is Image || (result = VisualTreeHelper.GetParent(obj)) is BindableRichTextBox)
                return result;
            else
                return null;
        }

	}

    
}