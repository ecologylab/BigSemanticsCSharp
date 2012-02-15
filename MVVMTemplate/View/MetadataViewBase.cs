using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MVVMTemplate.View
{
    public abstract class MetadataViewBase : UserControl
    {
        public abstract Grid LayoutGrid { get; }

        public void AlignLabelsByLevel(int level)
        {
            if (LayoutGrid != null)
            {
                LayoutGrid.ColumnDefinitions[0].SharedSizeGroup = "Label_" + level;
                LayoutGrid.ColumnDefinitions[0].SharedSizeGroup = "Value_" + level;
            }
        }
    }
}
