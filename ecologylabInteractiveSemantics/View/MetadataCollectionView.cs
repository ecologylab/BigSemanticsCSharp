using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ecologylab.semantics.metadata;
using MVVMTemplate.ViewModel;

namespace MVVMTemplate.View
{
    public class MetadataCollectionView : ExpandableViewBase
    {
        private IEnumerator _currentIterator;

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
            ICollection collection      = e.NewValue as ICollection;
            MetadataCollectionView view = d as MetadataCollectionView;
            if (collection != null && view != null)
                view.Initialize(collection.GetEnumerator());
        }

        public ICollection Collection
        {
            get { return (ICollection) GetValue(CollectionProperty); } 
            set
            {
                SetValue(CollectionProperty, value);
                Initialize(value.GetEnumerator());
            }
        }

        public override void AddItem(object item)
        {
            if (item is Metadata)
            {
                Metadata metadata = (Metadata) item;
                //int row = CollectionRoot.Children.Add(new MetadataView(metadata, ((MetadataCollectionFieldViewModel) DataContext).NestedLevel));
                MetadataCollectionFieldViewModel viewModel = (MetadataCollectionFieldViewModel) DataContext;
                CollectionCompositeItemView itemView =
                    new CollectionCompositeItemView(viewModel.MetaMetadataField.GetChildComposite(), metadata, viewModel.NestedLevel);
                AddFieldAndAlignLabels(viewModel.NestedLevel, itemView);
            }
        }

        public override IEnumerator Enumerator
        {
            get { return _currentIterator ?? (_currentIterator = this.Collection.GetEnumerator()); }
            set { this._currentIterator = value; }
        }

        public override int ExpandableSize
        {
            get { return Collection.Count; }
        }
    }
}
