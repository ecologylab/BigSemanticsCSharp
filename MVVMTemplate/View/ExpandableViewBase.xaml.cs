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

namespace MVVMTemplate.View
{
    /// <summary>
    /// Interaction logic for ExpandableViewBase.xaml
    /// </summary>
    public partial class ExpandableViewBase : IExpandable
    {
        public void Expand()
        {
            this.IsExpanded = true;
            this.Enumerator.Reset();
            this.Enumerator.MoveNext();
            
            while (Enumerator.MoveNext())
            {
                AddItem(Enumerator.Current);
            }
        }

        public virtual void AddItem(object item)
        {
        }

        public void Initialize(IEnumerator enumerator)
        {
            this.LayoutRoot.Children.Clear();
            
            this.Enumerator = enumerator;
            
            while (this.Enumerator.MoveNext() && (this.IsExpanded || this.LayoutRoot.Children.Count == 0))
                AddItem(Enumerator.Current);
        }

        public void Collapse()
        {
            this.IsExpanded = false;
            for (int i=LayoutRoot.Children.Count-1; i > 0; i--)
            {
                this.LayoutRoot.Children.RemoveAt(i);
            }
        }

        public void ToggleExpand()
        {
            if (this.IsExpanded)
                Collapse();
            else
                Expand();
        }

        internal void AddFieldAndAlignLabels(int nestedLevel, MetadataViewBase field)
        {
            if (field != null)
            {
                if (nestedLevel > 0)
                    field.AlignLabelsByLevel(nestedLevel);
                int row = this.LayoutRoot.Children.Add(field);
                this.LayoutRoot.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(field, row);
            }
        }

        public bool                IsExpanded { get; set; }
        public virtual IEnumerator Enumerator { get; set; }
        public virtual int         ExpandableSize
        {
           get { return 0; }
        }

    }
}
