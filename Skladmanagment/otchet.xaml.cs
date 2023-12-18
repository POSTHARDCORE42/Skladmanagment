using Skladmanagment.design;
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
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using static Skladmanagment.DatabaseContext;
using OxyPlot.Axes;

namespace Skladmanagment
{
    public partial class otchet : Window
    {
        

        public otchet()
        {
            
            InitializeComponent();
            LoadOtchet();
            DataContext = new MainViewModel();
        }
        public class MainViewModel
        {
            public PlotModel PlotModel { get; set; }

            public MainViewModel()
            {
                PlotModel = new PlotModel { Title = "Прибыль" };

                var dbContext = new DatabaseContext();
                var (Dates, Costs) = dbContext.GetDateAndCostData();

                // Создание графика
                var series = new LineSeries();
                for (int i = 0; i < Dates.Count; i++)
                {
                    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Dates[i]), Convert.ToDouble(Costs[i])));
                }

                PlotModel.Series.Add(series);
                PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Title = "Дата" });
                PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Прибыль" });
            }
        }
        private void LoadOtchet()
        {
            DatabaseContext db = new DatabaseContext();
            List<NakladnayaViewModel> otch = db.GetAllNakladnayaData();
            OtchetDataGrid.ItemsSource = otch;
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            homewindow homeWindow = new homewindow();
            homeWindow.Show();
            this.Close();
        }

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
