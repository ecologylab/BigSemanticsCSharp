using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ecologylab.interactive.Utils;

namespace  ecologylab.semantics.interactive.Controls
{


	/// <summary>
	/// Interaction logic for WikiPage.xaml
	/// </summary>
	public partial class WikiPage : UserControl, IHitTestAcceptor
	{
	    private Logger logger = new Logger();
        public WikiPage()
		{
            this.InitializeComponent();
		}

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