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

namespace MetadataUISandbox
{
	/// <summary>
	/// Interaction logic for WikiPage.xaml
	/// </summary>
	public partial class WikiPage : UserControl
	{
		public WikiPage()
		{
			this.InitializeComponent();
		}

		private void TextSize_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			
			if(LinkSize != null && TextSize != null && LinkSize.Value < TextSize.Value)
			{
				LinkSize.Value = TextSize.Value;
				LinkSize.UpdateLayout();
			}
		}
	}
}