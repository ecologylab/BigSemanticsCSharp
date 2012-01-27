using System;
using System.Collections;
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
using ecologylab.semantics.metadata;

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for MetadataCollectionView.xaml
    /// </summary>
    public partial class MetadataCollectionView : UserControl, IExpandable
    {
        public MetadataCollectionView()
        {
            InitializeComponent();
        }

        public MetadataCollectionView(ICollection collection)
        {
            InitializeComponent();

            this.Collection = collection;
        }

        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register(
            "Collection", 
            typeof(ICollection), 
            typeof(MetadataCollectionView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCollectionChanged)));

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ((MetadataCollectionView) d).BuildFields((ICollection) e.NewValue);
        }

        public ICollection Collection
        {
            get { return (ICollection) GetValue(CollectionProperty); } 
            set
            {
                SetValue(CollectionProperty, value);
                BuildFields(value);
            }
        }

        public void BuildFields(ICollection collection)
        {
            foreach (var item in collection)
            {
                if (item is Metadata)
                    this.LayoutRoot.Children.Add(new MetadataView((Metadata) item));
            }
        }

        public void Expand()
        {
            throw new NotImplementedException();
        }

        public void Collapse()
        {
            throw new NotImplementedException();
        }

        public void ToggleExpand()
        {
            throw new NotImplementedException();
        }

        public UIElement ExpandAffordance
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsExpanded
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
