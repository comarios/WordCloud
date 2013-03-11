using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WordCloud {
    /// <summary>
    /// Interaction logic for CloudTabView.xaml
    /// </summary>
    public partial class CloudTabView : UserControl {
        public CloudTabView() {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Mouse.OverrideCursor = Cursors.Arrow;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e) {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri("theme.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
        }
    }
}
