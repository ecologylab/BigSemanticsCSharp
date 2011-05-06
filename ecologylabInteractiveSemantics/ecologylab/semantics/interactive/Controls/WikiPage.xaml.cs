using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AttachedCommandBehavior;
using ecologylab.interactive.Utils;
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

        private String Title { get; set; }

	    public WikipediaPage Page
	    {
	        get { return page; }
            set
            {
                page = value;
                //Set data context for the title.
                WikiTitle.DataContext = page;
            }
	    }

	    public WikiPage()
	    {
	        this.InitializeComponent();
	    }

        public WikiPage(String title)
        {
            this.InitializeComponent();
            Title = title;
            WikiTitleTextBlock.Text = title;
        }

	    private void AddDoubleTapBehaviour()
	    {
	        DoubleTapBehaviour doubleTapOnTitle = new DoubleTapBehaviour
	        {
	            Command = new SimpleCommand
	            {
	                ExecuteDelegate = tappedParams =>
	                {
	                    Console.WriteLine("DoubleTapped on title");
	                    ShowIntroPara();
	                }
	            }
	        };
	        ScrollViewPage.Visibility = Visibility.Hidden;
	        doubleTapOnTitle.Attach(this);
	    }

	    public WikiPage(WikipediaPage page)
            :this()
        {
            this.Page = page;
        }

        public void ShowIntroPara()
        {
            
            ScrollViewPage.Visibility = ScrollViewPage.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden ;
            Console.WriteLine("Intro Para is now " + ScrollViewPage.Visibility);
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