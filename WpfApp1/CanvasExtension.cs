using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public static class CanvasExtensions
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(CanvasExtensions),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static IEnumerable GetItemsSource(DependencyObject obj)
        {
            return (IEnumerable)obj.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(DependencyObject obj, IEnumerable value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }

        private static void OnItemsSourceChanged(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            var canvas = obj as Canvas;
            if (canvas == null) return;

            canvas.Children.Clear();

            if (e.NewValue is IEnumerable<Box> boxes)
            {
                foreach (var box in boxes)
                {
                    var rectangle = new Rectangle
                    {
                        Width = box.Width,
                        Height = box.Height,
                        Fill = Brushes.Blue // Customize as desired
                    };

                    Canvas.SetLeft(rectangle, box.X);
                    Canvas.SetTop(rectangle, box.Y);

                    var thumb = new Thumb
                    {
                        Width = 10,
                        Height = 10,
                        Background = Brushes.Red // Customize as desired
                    };

                    thumb.DragDelta += (sender, evt) =>
                    {
                        var newLeft = Canvas.GetLeft(rectangle) + evt.HorizontalChange;
                        var newTop = Canvas.GetTop(rectangle) + evt.VerticalChange;

                        var newWidth = rectangle.Width - evt.HorizontalChange;
                        var newHeight = rectangle.Height - evt.VerticalChange;

                        if (newWidth > 0 && newHeight > 0)
                        {
                            Canvas.SetLeft(rectangle, newLeft);
                            Canvas.SetTop(rectangle, newTop);

                            rectangle.Width = newWidth;
                            rectangle.Height = newHeight;
                        }
                    };

                    Canvas.SetLeft(thumb, box.X - thumb.Width / 2);
                    Canvas.SetTop(thumb, box.Y - thumb.Height / 2);

                    canvas.Children.Add(rectangle);
                    canvas.Children.Add(thumb);
                }
            }
        }
    }
}
