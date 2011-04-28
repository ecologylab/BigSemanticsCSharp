﻿using System.Windows;
using System.Windows.Input;

namespace ecologylab.interactive
{
    /// <summary>
    /// Interaction logic for RightHandedControlMenu.xaml
    /// </summary>
    /// 

    public struct CommandParameters
    {
        public DependencyObject visualHit;

        public TouchEventArgs touchEventArgs;

        public DependencyObject visualContainer;
    }
}