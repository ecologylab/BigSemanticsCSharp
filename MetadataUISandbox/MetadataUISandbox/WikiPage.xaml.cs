using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MetadataUISandbox.Utils;

namespace MetadataUISandbox
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