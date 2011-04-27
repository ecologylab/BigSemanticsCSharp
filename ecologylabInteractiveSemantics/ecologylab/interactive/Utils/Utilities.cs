using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

/// Forgive the mess. This will get organized when there are well defined places they need to be in.

namespace ecologylab.interactive.Utils
{
    public delegate HitTestResultBehavior HitTestResultDelegate(HitTestResult result);

    public class Utilities
    {
        public static double Distance(Point? a, Point? b)
        {
            if (!a.HasValue || !b.HasValue)
                return Double.NaN;
            double deltaX = (a.Value.X - b.Value.X);
            double deltaY = (a.Value.Y - b.Value.Y);
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }

    public class Logger
    {
        String _logPrefix;
        short clsLength;
        public Logger(short clsLength = (short) 40)
        {
            this.clsLength = clsLength;
            ResetPrefix();
        }

        public void ResetPrefix()
        {
            Type declType = new StackFrame(2, false).GetMethod().DeclaringType;
            String callingClassName = (declType.DeclaringType == null ? declType.Name : declType.DeclaringType.Name);
            _logPrefix = "[     " + callingClassName.Substring(0, callingClassName.Length < clsLength ? callingClassName.Length : clsLength).PadRight(clsLength, ' ') +
                                "]: ";
        }

        public void Log(String val)
        {
            Console.WriteLine( _logPrefix + val);
        }

    }
    
    public interface ILabelledCommand
    {
        String GetLabel();
    }

    /// <summary>
    /// Allows behaviours to request the AssociatedObject for an AcceptableObject while iterating through the visual hit test results.
    /// 
    /// Ideally, this would have been only a boolean. 
    /// However, visual hit tests on RichTextObjects are funny, we might have to return an element out of the hitTestZone as the accepted object. 
    /// 
    /// </summary>
    public interface IHitTestAcceptor
    {
        DependencyObject AcceptableObject(DependencyObject obj);
    }
}
