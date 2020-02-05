using Experimenter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
//using TspLibNet;
//using TspLibNet.TSP;


namespace Dynamic_Meta_00
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string WorkingDirectory;
        string ActualFile;
        int[][] Distances; 

        FileInfo Actualfile;
        DirectoryInfo Actualdirectory;


        public MainWindow()
        {
            InitializeComponent();

            WorkingDirectory = @"C:\Users\sabry\Dropbox\Docs\Research\Projects\TSP";
            Actualdirectory = new DirectoryInfo(WorkingDirectory);
            WorkingDirectory = @"C:\Users\sabry\Dropbox\Docs\Research\Projects\";

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

                    else if (Char.IsWhiteSpace(c[0]))
                    {

                        string[] coodiny = line.Split(' ');
                        double[] coor = new double[2];
                        coor[0] = Convert.ToDouble(coodiny[1].Replace('.', ','));
                        coor[1] = Convert.ToDouble(coodiny[2].Replace('.', ','));

                        coordinates.Add(coor);
                    }
                   




                }
                      
                         
                    }
                }



            
           


                int[][] distances = new int[size][];
                for (int i = 0; i < distances.Length; ++i)
                    distances[i] = new int[size];

          
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j)
                        {
                            distances[i][j] = distances[j][i] = 0;
                            
                        }

                        else
                        {
                            double[] x = coordinates[i];
                            double[] y = coordinates[j];

                            double dx = x[0] - y[0];
                            double dy = x[1] - y[1];



                            distances[i][j] = Convert.ToInt32( Math.Sqrt(dx * dx + dy * dy));
                        }
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

                Distances = distances;



                RichTextbox1.Document.Blocks.Clear();

                //for (int i = 0; i < size; i++)
                //{
                //    for (int j = 0; j < size; j++)
                //    {
                //        RichTextbox1.AppendText(distances[i][j].ToString());
                //        RichTextbox1.AppendText(" ");


                //    }

                //    RichTextbox1.AppendText("\n");
                //}

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (infotext.Content == "TSP instance successfully loaded !")
            {

                double alpha = Convert.ToDouble(alphatxt.Text);
                double beta = Convert.ToDouble(betatxt.Text);
                double rho = Convert.ToDouble(rhotxt.Text);
                double q = Convert.ToDouble(qtxt.Text);


                if (radiobutton.IsChecked == true)
                {

                    Ant_Colony_Optim ant = new Ant_Colony_Optim(Distances, alpha, beta, rho, q, Distances.Length, 1000);
                    for (int i = 0; i < ant.Trail.Length; i++)
                    {
                        RichTextbox1.AppendText(ant.Trail[i].ToString());
                        RichTextbox1.AppendText(" ");
                    }


                    RichTextbox1.AppendText("\n Static Best tour Lenght : " + ant.Best.ToString());

                    using (StreamWriter wr = new StreamWriter("results.dat"))
                    {

                        for (int i = 0; i < ant.results.Count; i++)
                        {
                            double[] line = ant.results[i];
                            wr.WriteLine(Convert.ToString(line[0]) + " " + Convert.ToString(line[1]));
                        }



                    }

                }




                else
                { 
                
                
                Dynamic_Ant_Colony_Optim Dante = new Dynamic_Ant_Colony_Optim(Distances, alpha, beta, rho, q, Distances.Length, 1000);

                for (int i = 0; i < Dante.Trail.Length; i++)
                {
                    RichTextbox1.AppendText(Dante.Trail[i].ToString());
                    RichTextbox1.AppendText(" ");
                }


                RichTextbox1.AppendText("\n Dynamic Best tour Lenght : " + Dante.Best.ToString());

                using (StreamWriter wr = new StreamWriter("results.dat"))
                {

                    for (int i = 0; i < Dante.results.Count; i++)
                    {
                        double[] line = Dante.results[i];
                        wr.WriteLine(Convert.ToString(line[0]) + " " + Convert.ToString(line[1]));
                    }



                }

                
                
                } 

                Process.Start("results.dat");
                

               
                
            }
        }

        
    }
}
    

