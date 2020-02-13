using ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CancellationTokenSourceProjectTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private int count = 0;
        private Task single;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            Ex.Log($"AddTask_Click()");
            Ex.Try(false,()=>cts.Cancel());
            cts = new CancellationTokenSource();
            WorkTask(count, cts.Token);
            count++;
            
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            Ex.Log($"CancelTask_Click()");
            Ex.Try(false, () => cts.Cancel());
            cts = new CancellationTokenSource();
        }

        private async Task WorkTask(int name, CancellationToken cancel)
        {
            Ex.Log($"WorkTask() {name}");            
            await Ex.Try(Task.Delay(5000, cancel));
            if (cancel.IsCancellationRequested)
            {
                Ex.Log($"{name} CANCELED!");
                return;
            }
            Ex.Log($"{name} is work done!");
        }

        private void SingletonTask_Click(object sender, RoutedEventArgs e)
        {
            Ex.Log($"SingletonTask_Click()");
            if (single == null || single.IsCompleted)
            {
                count++;
                single = WorkTask(count, cts.Token);                
            }
            else Ex.Log($"{single.Status} {count}");
        }
    }
}
