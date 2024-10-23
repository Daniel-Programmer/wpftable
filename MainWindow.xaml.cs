using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFXML.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WPFXML
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var pathToFile = OpenFile();

            var cars = DeserializeXmlFile(pathToFile);

            if (cars != null && pathToFile != null)
            {
                RemoveDataFromTable();
                filenameTextBlock.Text = System.IO.Path.GetFileName(pathToFile);
                List<CarDto> filteredCars = FilterCars(cars.CarsList);
                AddDataToTable(filteredCars);
            }

            else if (cars == null && pathToFile != null)
            {
                RemoveDataFromTable();
                filenameTextBlock.Text = "Invalid XML data";
            }
        }

        private void RemoveDataFromTable()
        {
            tableDataGrid.Items.Clear();
        }

        private void AddDataToTable(List<CarDto> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                tableDataGrid.Items.Add(items[i]);
            }
            
        }


        private Cars? DeserializeXmlFile(string xmlPath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Cars));

                using StreamReader sr = new StreamReader(xmlPath, Encoding.UTF8);
                Cars? items = serializer.Deserialize(sr) as Cars;
                sr.Close();

                return items;

            }

            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }

        }

        private string? OpenFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (.xml)|*.xml"; 

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        private List<CarDto> FilterCars(List<Car> cars)
        {
            return cars
                .Where((c) => (c.SaleDate.DayOfWeek == DayOfWeek.Saturday) || (c.SaleDate.DayOfWeek == DayOfWeek.Sunday))
                .GroupBy(c => c.Model)
                .Select(c => new CarDto
                {
                    Model = c.First().Model,
                    Price = new Price
                    {
                        AmountText = c.Sum(c => c.Price.Amount).ToString()
                    },
                    PriceWithVat = c.Sum(c => c.PriceWithVat),
                }).ToList();
        }
    }
}
