using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

//  参考资料：
//  1.WPF MeasureOverride And ArrangeOverride
//  https://www.cnblogs.com/dingli/archive/2011/04/22/2024786.html
//  2.在WPF中实现平滑滚动
//  https://www.cnblogs.com/TianFang/p/4198731.html

namespace StructControl
{
    class MyPanel : StackPanel
    {
        TranslateTransform _transForm;
        public MyPanel()
        {
            _transForm = new TranslateTransform();
            this.RenderTransform = _transForm;
        }

        #region Layout

        Size _screenSize;
        Size _totalSize;

        protected override Size MeasureOverride(Size availableSize)
        {
            _screenSize = availableSize;

            if (Orientation == Orientation.Horizontal)
                availableSize = new Size(availableSize.Width, double.PositiveInfinity);
            else
                availableSize = new Size(double.PositiveInfinity, availableSize.Height);

            _totalSize = base.MeasureOverride(availableSize);
            return _totalSize;
        }

        double x = 0;
        double y = 0;
        protected override Size ArrangeOverride(Size finalSize)
        {
            //foreach(UIElement element in InternalChildren)
            //{
            //    element.Arrange(new Rect(new Point(x,y), element.DesiredSize));
            //    if (Orientation == Orientation.Horizontal)
            //        x += element.DesiredSize.Width;
            //    else
            //        y += element.DesiredSize.Height;
            //}

            var size = base.ArrangeOverride(finalSize);
            if (ScrollOwner != null)
            {
                var yOffsetAnimation = new DoubleAnimation() { To = -VerticalOffset, Duration = TimeSpan.FromSeconds(1) };
                _transForm.BeginAnimation(TranslateTransform.YProperty, yOffsetAnimation);

                var xOffsetAnimation = new DoubleAnimation() { To = -HorizontalOffset, Duration = TimeSpan.FromSeconds(1) };
                _transForm.BeginAnimation(TranslateTransform.XProperty, xOffsetAnimation);

                ScrollOwner.InvalidateScrollInfo();
            }
            return _screenSize;
        }
       
        #endregion

        //#region IScrollInfo
        //public new ScrollViewer ScrollOwner { get; set; }
        //public new bool CanHorizontallyScroll { get; set; }
        //public new bool CanVerticallyScroll { get; set; }

        //public new double ExtentHeight { get { return _totalSize.Height; } }
        //public new double ExtentWidth { get { return _totalSize.Width; } }

        //public new double HorizontalOffset { get; private set; }
        //public new double VerticalOffset { get; private set; }

        //public new double ViewportHeight { get { return _screenSize.Height; } }
        //public new double ViewportWidth { get { return _screenSize.Width; } }

        //void appendOffset(double x, double y)
        //{
        //    var offset = new Vector(HorizontalOffset + x, VerticalOffset + y);

        //    offset.Y = range(offset.Y, 0, _totalSize.Height - _screenSize.Height);
        //    offset.X = range(offset.X, 0, _totalSize.Width - _screenSize.Width);

        //    HorizontalOffset = offset.X;
        //    VerticalOffset = offset.Y;

        //    InvalidateArrange();
        //}

        //double range(double value, double value1, double value2)
        //{
        //    var min = Math.Min(value1, value2);
        //    var max = Math.Max(value1, value2);

        //    value = Math.Max(value, min);
        //    value = Math.Min(value, max);

        //    return value;
        //}

        //const double _lineOffset = 30;
        //const double _wheelOffset = 90;

        //public new void LineDown()
        //{
        //    appendOffset(0, _lineOffset);
        //}

        //public new void LineUp()
        //{
        //    appendOffset(0, -_lineOffset);
        //}

        //public new void LineLeft()
        //{
        //    appendOffset(-_lineOffset, 0);
        //}

        //public new void LineRight()
        //{
        //    appendOffset(_lineOffset, 0);
        //}

        ////public new Rect MakeVisible(Visual visual, Rect rectangle)
        ////{
        ////    throw new NotSupportedException();
        ////}

        //public new void MouseWheelDown()
        //{
        //    appendOffset(0, _wheelOffset);
        //}

        //public new void MouseWheelUp()
        //{
        //    appendOffset(0, -_wheelOffset);
        //}

        //public new void MouseWheelLeft()
        //{
        //    appendOffset(0, _wheelOffset);
        //}

        //public new void MouseWheelRight()
        //{
        //    appendOffset(_wheelOffset, 0);
        //}

        //public new void PageDown()
        //{
        //    appendOffset(0, _screenSize.Height);
        //}

        //public new void PageUp()
        //{
        //    appendOffset(0, -_screenSize.Height);
        //}

        //public new void PageLeft()
        //{
        //    appendOffset(-_screenSize.Width, 0);
        //}

        //public new void PageRight()
        //{
        //    appendOffset(_screenSize.Width, 0);
        //}

        //public new void SetVerticalOffset(double offset)
        //{
        //    this.appendOffset(HorizontalOffset, offset - VerticalOffset);
        //}

        //public new void SetHorizontalOffset(double offset)
        //{
        //    this.appendOffset(offset - HorizontalOffset, VerticalOffset);
        //}
        //#endregion

    }
}
