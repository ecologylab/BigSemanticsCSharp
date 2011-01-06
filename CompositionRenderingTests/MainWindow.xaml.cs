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
using System.Xml;

namespace CompositionRenderingTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string path = "C:\\Users\\damaraju.m2icode\\workspace\\cSharp\\ecologylabSemantics\\TestCases\\untitled.xml";
            XmlDataProvider p = new XmlDataProvider();
            p.Source = new Uri(path);

            compositionItemsControl.DataContext = p;

        }
    }

    public class SurrogateTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Window pres = Application.Current.MainWindow;
            if ((item as XmlElement).FirstChild.Name.Contains("text"))
            {
                return pres.TryFindResource("textSurrogateTemplate") as DataTemplate;
            }
            if ((item as XmlElement).FirstChild.Name.Contains("image"))
            {
                return pres.TryFindResource("imageSurrogateTemplate") as DataTemplate;
            }
            return null;
        }
    }
}
