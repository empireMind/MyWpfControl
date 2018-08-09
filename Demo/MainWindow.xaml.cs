using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        ObservableCollection<UIClass> viewList = new ObservableCollection<UIClass>();
        ObservableCollection<UIClass> viewList1 = new ObservableCollection<UIClass>();
        ObservableCollection<UIClass> viewList2 = new ObservableCollection<UIClass>();
        ObservableCollection<UIClass> viewList3 = new ObservableCollection<UIClass>();
        ObservableCollection<UIClass> viewList4 = new ObservableCollection<UIClass>();

        BitmapImage lbj = new BitmapImage(new Uri(@"Image/LeBron James.jpg", UriKind.Relative));
        string time;
        int viewCount = 4;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HorizontalList.DataContext = viewList;
            VerticalList1.DataContext = viewList1;
            VerticalList2.DataContext = viewList2;
            VerticalList3.DataContext = viewList3;
            VerticalList4.DataContext = viewList4;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time = string.Format("{0:T}", DateTime.Now);
            UIClass record = new UIClass { Photo = lbj, Name = "LeBron James", Time = time };
            if (viewList.Count > viewCount)
                viewList.Remove(viewList[viewCount]);
            viewList.Insert(0, record);
            if (viewList1.Count > viewCount)
                viewList1.Remove(viewList1[viewCount]);
            viewList1.Insert(0, record);
            if (viewList2.Count > viewCount)
                viewList2.Remove(viewList2[viewCount]);
            viewList2.Insert(0, record);
            if (viewList3.Count > viewCount)
                viewList3.Remove(viewList3[viewCount]);
            viewList3.Insert(0, record);
            if (viewList4.Count > viewCount)
                viewList4.Remove(viewList4[viewCount]);
            viewList4.Insert(0, record);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
        }
    }
}
