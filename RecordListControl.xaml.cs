using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SmoothListView
{
    /// <summary>
    /// 交互逻辑
    /// </summary>
    public partial class RecordListControl : UserControl
    {
        private ScrollViewer scrollViewer = null;
        private double theOffset = 0;
        Storyboard storyboard = new Storyboard();
        object Lock = new object();

        public RecordListControl()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)RecordList.Items).CollectionChanged += RecordListControl_CollectionChanged;
        }

        private void RecordListControl_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    RecordList.UpdateLayout();
                    var listBoxItem = this.RecordList.ItemContainerGenerator.ContainerFromIndex(0) as ListViewItem;

                    if(Orient == Orientation.Horizontal)
                    {
                        var animation = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(500), From = -listBoxItem.Width };
                        Storyboard.SetTarget(animation, listBoxItem);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
                        storyboard.Children.Add(animation);
                    }
                    else if(Orient == Orientation.Vertical)
                    {
                        var animation = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(500), From = -listBoxItem.Height };
                        Storyboard.SetTarget(animation, listBoxItem);
                        Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.Y"));
                        storyboard.Children.Add(animation);
                    }

                    storyboard.Begin();                
                }
                else if(e.Action == NotifyCollectionChangedAction.Remove)
                {
                    storyboard.Children.RemoveAt(0);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void RecordList_Loaded(object sender, RoutedEventArgs e)
        {
            Decorator border = VisualTreeHelper.GetChild(RecordList, 0) as Decorator;
            if(border!=null)
            {
                scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToVerticalOffset(theOffset);
                }
                RecordList.ItemTemplate = DataPresenter;
            }
        }

        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
            set { SetValue(ItemCountProperty, value); }          
        }

        // Using a DependencyProperty as the backing store for ItemCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCountProperty =
            DependencyProperty.Register("ItemCount", typeof(int), typeof(RecordListControl), new PropertyMetadata(0));

        public Orientation Orient
        {
            get { return (Orientation)GetValue(OrientProperty); }
            set { SetValue(OrientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientProperty =
            DependencyProperty.Register("Orient", typeof(Orientation), typeof(RecordListControl), new PropertyMetadata(Orientation.Horizontal));

        public DataTemplate DataPresenter
        {
            get { return (DataTemplate)GetValue(DataPresenterProperty); }
            set { SetValue(DataPresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPresenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataPresenterProperty =
            DependencyProperty.Register("DataPresenter", typeof(DataTemplate), typeof(RecordListControl), new PropertyMetadata(null));
    }
}
