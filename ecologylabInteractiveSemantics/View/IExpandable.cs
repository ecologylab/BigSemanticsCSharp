using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MVVMTemplate.View
{
    public interface IExpandable
    {
        void Expand();
        void Collapse();
        void ToggleExpand();

        bool        IsExpanded { get; set; }
        IEnumerator Enumerator { get; set; }

    }
}
