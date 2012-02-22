using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ecologylab.interactive.Utils
{
    public static class AnimationHelperExtensions
    {
        private static readonly Duration _fadeInOutAnimationDuration = TimeSpan.FromMilliseconds(300);

        /// <summary>
        /// delegate to process once the animation is complete.
        /// </summary>
        public delegate void AnimationCompleteDelegate();

        public static void AnimateOnce(this UIElement element, DependencyProperty prop, double toValue, Duration duration, AnimationCompleteDelegate onComplete = null)
        {
            DoubleAnimation anim = new DoubleAnimation(toValue, duration);
            anim.Completed += (s, e) =>
            {
                element.SetValue(prop, toValue);
                element.BeginAnimation(prop, null);
                if (onComplete != null)
                    onComplete();
            };
            element.BeginAnimation(prop, anim);
        }


        public static void FadeIn(this UIElement element, TimeSpan? timeSpan = null, AnimationCompleteDelegate onComplete = null)
        {
            Duration duration = timeSpan.HasValue ? _fadeInOutAnimationDuration : timeSpan.Value;

            AnimateOnce(element, UIElement.OpacityProperty, 1, duration, onComplete);
        }

        public static void FadeOut(this UIElement element, TimeSpan? timeSpan = null, AnimationCompleteDelegate onComplete = null)
        {
            Duration duration = timeSpan.HasValue ? _fadeInOutAnimationDuration : timeSpan.Value;

            AnimateOnce(element, UIElement.OpacityProperty, 0, duration, onComplete);
        }

        public static void AnimateCanvasMove(this UIElement el, double toPointX, double toPointY, TimeSpan? timeSpan = null, AnimationCompleteDelegate onComplete = null)
        {
            Duration duration = timeSpan.HasValue ? _fadeInOutAnimationDuration : timeSpan.Value;
            AnimateOnce(el, Canvas.LeftProperty, toPointX, duration, onComplete);
            AnimateOnce(el, Canvas.TopProperty, toPointY, duration, onComplete);
        }

        public static void AnimateCanvasMove(this UIElement el, Point toPoint, TimeSpan? timespan = null, AnimationCompleteDelegate onComplete = null)
        {
            AnimateCanvasMove(el, toPoint.X, toPoint.Y, timespan, onComplete);
        }
    }
}
