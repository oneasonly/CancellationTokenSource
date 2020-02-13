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
        private CancellationTokenSource cts = null;
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            Ex.Log($"AddTask_Click()");
            Ex.Try(false,()=>cts.Cancel());
            cts = null;
            cts = new CancellationTokenSource();
            count++;
            WorkTask(count, cts.Token);            
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            Ex.Log($"CancelTask_Click()");
        }

        private async Task WorkTask(int name, CancellationToken cancel)
        {
            while(true)
            {
                if (cancel.IsCancellationRequested)
                {
                    Ex.Log($"{name} CANCELED");
                    break;
                }
                await Task.Delay(1000);
                Ex.Log($"{name} is working....");
            }
        }
    }
}
