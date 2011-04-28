using System;
using System.Windows;
using System.Windows.Documents;

namespace DomExtraction
{
  /// <summary>
  /// A subclass of the Run element that exposes a DependencyProperty property 
  /// to allow data binding.
  /// </summary>
  public class BindableRun : Run
  {
    public static readonly DependencyProperty BoundTextProperty = DependencyProperty.Register("BoundText", typeof(string), typeof(BindableRun), new PropertyMetadata(new PropertyChangedCallback(BindableRun.onBoundTextChanged)));
 
    private static void onBoundTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((Run) d).Text = (string) e.NewValue;
    }
 
    public String BoundText
    {
      get { return (string)GetValue(BoundTextProperty); }
      set { SetValue(BoundTextProperty, value); }
    }
  }
}