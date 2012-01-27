using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MVVMTemplate.View
{
    public interface IExpandable
    {
        void Expand();
        void Collapse();
        void ToggleExpand();

        UIElement ExpandAffordance { get; set; }
        bool IsExpanded { get; set; }
    }
}
