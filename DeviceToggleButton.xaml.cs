using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StructControl
{
    /// <summary>
    /// DeviceToggleButton.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceToggleButton : UserControl
    {
        public DeviceToggleButton()
        {
            InitializeComponent();
        }

        #region AllowsAnimationProperty 启用旋转动画
        /// <summary>
        /// 启用旋转动画
        /// </summary>
        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.RegisterAttached("AllowsAnimation"
            , typeof(bool), typeof(DeviceToggleButton), new FrameworkPropertyMetadata(false, AllowsAnimationChanged));

        public static bool GetAllowsAnimation(DependencyObject d)
        {
            return (bool)d.GetValue(AllowsAnimationProperty);
        }

        public static void SetAllowsAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowsAnimationProperty, value);
        }

        /// <summary>
        /// 旋转动画刻度
        /// </summary>
        private static DoubleAnimation RotateAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)));

        /// <summary>
        /// 绑定动画事件
        /// </summary>
        private static void AllowsAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FrameworkElement;
            if (uc == null) return;
            if (uc.RenderTransformOrigin == new Point(0, 0))
            {
                uc.RenderTransformOrigin = new Point(0.5, 0.5);
                RotateTransform trans = new RotateTransform(0);
                uc.RenderTransform = trans;
            }
            var value = (bool)e.NewValue;
            if (value)
            {
                RotateAnimation.To = 180;
                uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            }
            else
            {
                RotateAnimation.To = 0;
                uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            }
        }
        #endregion
    }
}
