using Data;
using Domain;
using Interfaces;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            DbPZPN dbContext = new DbPZPN();
            var zawodnikRepo = new Repository<Zawodnik>(dbContext);
            var klubRepo = new Repository<Klub>(dbContext);
            var statystykaRepo = new Repository<Statystyka>(dbContext);
            var raporty = new Raporty(zawodnikRepo, klubRepo, statystykaRepo);
            this.DataContext = new ZawodnikViewModel(zawodnikRepo, raporty);
        }

        
    }
}