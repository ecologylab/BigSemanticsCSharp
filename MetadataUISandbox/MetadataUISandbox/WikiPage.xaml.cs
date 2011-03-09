using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ecologylab.semantics.generated.library;
using MetadataUISandbox.Utilities;

namespace MetadataUISandbox
{


	/// <summary>
	/// Interaction logic for WikiPage.xaml
	/// </summary>
	public partial class WikiPage : UserControl, IHitTestAcceptor
	{
       
        public WikiPage()
		{
            this.InitializeComponent();
		}

        public DependencyObject AcceptableObject(DependencyObject obj)
        {
            DependencyObject result;
            if ((result = obj) is Image || (result = VisualTreeHelper.GetParent(obj)) is BindableRichTextBox)
                return result;
            else
                return null;
        }

	}

    
}