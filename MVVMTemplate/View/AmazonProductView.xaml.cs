using System;
using System.Collections.Generic;
using System.Linq;
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
using ecologylab.semantics.generated.library.products;
using MVVMTemplate.ViewModel;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for AmazonProductView.xaml
    /// </summary>
    public partial class AmazonProductView : UserControl
    {
        public AmazonProductView(AmazonProduct amazonProduct)
        {
            this.DataContext = new MetadataViewModelBase(amazonProduct);
            InitializeComponent();
        }
    }
}
