using System;
using System.Collections.Generic;
using System.IO;
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
//using HeuristicLab.Problems.TravelingSalesman;
//using HeuristicLab.Problems.Instances.TSPLIB;
//using HeuristicLab.Problems.Instances;
using TspLibNet;
using TspLibNet.TSP;


namespace Dynamic_Meta_00
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string WorkingDirectory;
        string ActualFile;

        FileInfo Actualfile;
        DirectoryInfo Actualdirectory;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Show();

            infotext.Content = "Loading... !";

            if (loadfiles() == true)

                infotext.Content = "All Files were loaded !";

            else

                System.Windows.MessageBox.Show("Could not fetch a valid TSP file in the specified Directory",
                    "Empty Directory", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        public bool loadfiles()
        {
            bool state = false;

            try
            {


                DirectoryInfo dir = new DirectoryInfo(@"C:\Users\sabry\Dropbox\Docs\Research\Projects\TSP");
                FileInfo[] TspFiles = dir.GetFiles("*.TSP"); //Getting TSP files
                FileInfo[] TourFiles = dir.GetFiles("*.TOUR"); //Getting TOUR files

                

                foreach (FileInfo file in TspFiles)
                {



                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = file.Name;

                    combobox.Items.Add(item);

                    Random rnd = new Random();
                    progressbar.Value += rnd.Next(3, 10);
                    this.UpdateLayout();



                    progressbar.Value = 100;

                    combobox.SelectedIndex = 0;
                    state = true;
                }
            }

            catch (Exception)
            {

                state = false;
            }



            return state;
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private bool LoadInstance()
        {




            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\sabry\Dropbox\Docs\Research\Projects\TSP");
            FileInfo[] TspFiles = dir.GetFiles("*.TSP"); //Getting TSP files
            FileInfo[] TourFiles = dir.GetFiles("*.TOUR"); //Getting TOUR files

            foreach (FileInfo f in TspFiles)
            {
                ComboBoxItem item = new ComboBoxItem();

                


                if (combobox.SelectionBoxItem.ToString() == f.Name.ToString())
                {
                    Actualfile = f;
                    break;

                }


            }


            var path = @"C:\Users\sabry\Dropbox\Docs\Research\Projects\TSP";
            TspLib95 lib = new TspLib95(path);

            int counter = lib.LoadTSP("a280");

            double h = lib.Items[0].OptimalTourDistance ;

            

            TspLib95Item t = new TspLib95Item(lib.Items[0].Problem, lib.Items[0].OptimalTour, lib.Items[0].OptimalTourDistance);


            string s = Actualfile.FullName;
                
                //TspFile file = TspFile.Load(s);
            
             
            
           
            

          //TravelingSalesmanProblem problem = TravelingSalesmanProblem.FromTspFile(file);



            //HeuristicLab.Problems.TravelingSalesman.TravelingSalesmanProblem problem = new HeuristicLab.Problems.TravelingSalesman.TravelingSalesmanProblem();

            //TSPData data = new TSPData();
            

            //TSPLIBTSPInstanceProvider provider = new TSPLIBTSPInstanceProvider();
           

            //data = provider.LoadData(Actualfile.FullName, null, null);
            //problem.Load(data);

           

            //DistanceMatrix matrix = new DistanceMatrix(data.Distances);


            //problem.DistanceMatrix = matrix;





            //System.Windows.MessageBox.Show(problem.DistanceMatrix.ToString(),
            //        "Empty Directory", MessageBoxButton.OK, MessageBoxImage.Error);



            return true;

        }

        private void combobox_DropDownClosed(object sender, EventArgs e)
        {
            if (infotext.Content != "Loading... !")
            {
                if (LoadInstance() == true)

                    infotext.Content = "TSP instance successfully loaded !";
                else
                    System.Windows.MessageBox.Show("Could not fetch a valid TSP file in the specified Directory",
                        "Empty Directory", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
    

