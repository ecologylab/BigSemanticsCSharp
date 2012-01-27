using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMTemplate.View
{
    public class Commands
    {
        public static readonly RoutedUICommand ExpandCollapse = new RoutedUICommand("Expand or collapse", "ExpandCollapse", typeof(ExpandCollapseButton));

        
    }
}
