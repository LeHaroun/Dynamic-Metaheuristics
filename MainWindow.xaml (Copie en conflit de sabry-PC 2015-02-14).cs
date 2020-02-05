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

            WorkingDirectory = @"C:\Users\sabry\Dropbox\Docs\Research\Projects\TSP";
            Actualdirectory = new DirectoryInfo(WorkingDirectory);
            WorkingDirectory = @"C:\Users\sabry\Dropbox\Docs\Research\Projects";

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


                FileInfo[] TspFiles = Actualdirectory.GetFiles("*.TSP"); //Getting TSP files
                FileInfo[] TourFiles = Actualdirectory.GetFiles("*.TOUR"); //Getting TOUR files

                

                foreach (FileInfo file in TspFiles)
                {



                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = file.Name;

                    combobox.Items.Add(item);

                    Random rnd = new Random();
                    progressbar.Value += rnd.Next(3, 10);
                    progressbar.UpdateLayout();
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




            FileInfo[] TspFiles = Actualdirectory.GetFiles("*.TSP"); //Getting TSP files
            FileInfo[] TourFiles = Actualdirectory.GetFiles("*.TOUR"); //Getting TOUR files

            foreach (FileInfo f in TspFiles)
            {
                ComboBoxItem item = new ComboBoxItem();

                


                if (combobox.SelectionBoxItem.ToString() == f.Name.ToString())
                {
                    Actualfile = f;
                    break;

                }


            }


           

            
            string s = Actualfile.FullName;
                
           
            
            

           

            int size = 0;
            List<double[]> coordinates = new List<double[]>();
          

                using (StreamReader sr = new StreamReader(Actualfile.FullName))
                {
                    string line;

                     while ((line = sr.ReadLine()) != null)
                    {
                        if (!String.IsNullOrEmpty(line))
                        {
                            

                        if(line.Contains("DIMENSION: "))
                      
                      {
                          size = Convert.ToInt32(line.Replace("DIMENSION: ",""));

                      }
                      if (line.Contains("DIMENSION : "))
                      {
                          size = Convert.ToInt32(line.Replace("DIMENSION : ", ""));

                      }

                         char[] c = line.ToCharArray();
                         
                         
                         

                      
                    if (Char.IsDigit(c[0]))
                    {
                        string[] coodiny = line.Split(' ');
                        double[] coor = new double[2];
                        coor[0] = Convert.ToDouble(coodiny[1].Replace('.',','));
                        coor[1] = Convert.ToDouble(coodiny[2].Replace('.', ','));
                                          


                        coordinates.Add(coor);
                    }
                        }
                      
                         
                    }
                }
                double[][] distances = new double[size][];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        distances[i][j] = 0;
                    }
                }

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

        private void CreateCoordinates(string line)
        {
            throw new NotImplementedException();
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
    

