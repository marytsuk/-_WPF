using System;
using System.Collections.Generic;
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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataLib;

namespace WPF_Sample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        ResearcherObservable obj = new ResearcherObservable("Mary", "Tsukanova");
        Project pr_obj = new Project(new DateTime(2019,05,05), new DateTime(2019, 05, 10), 1, "New_Project");
 
        public MainWindow()
        {
            InitializeComponent();
            obj.CollectionChanged += Data_Changed_Handler;
            this.DataContext = obj;
            Grid_New_PR.DataContext = pr_obj;
            ComboBox_Organith.ItemsSource = obj.Orgs;
            ComboBox_Organith.SelectedIndex = 0;
        }
       
        public void Data_Changed_Handler(object source, NotifyCollectionChangedEventArgs args)
        {
            //ListBox_Project.ItemsSource = obj.Get_Projects;
            obj.NotifyPropertyChanged("Get_Count_Of_Projects");
            obj.NotifyPropertyChanged("Get_Projects");
            obj.NotifyPropertyChanged("Get_All");
        }

        private void Save()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            if (obj.IfChanged)
            {
                var result = MessageBox.Show("Save changes?", "Message", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (sfd.ShowDialog() == true)
                        ResearcherObservable.Save(sfd.FileName, ref obj);
                }
                else
                    MessageBox.Show("Data may be lost!", "Message");
            }
        }
        private void Update_Items()
        {
            this.DataContext = obj;
            obj.CollectionChanged += Data_Changed_Handler;
            //ListBox_All.ItemsSource = obj;         //
           // ListBox_Project.ItemsSource = obj.Get_Projects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            obj.AddDefaults();
            Update_Items();
            //ListBox_All.ItemsSource = obj;
            //var Q = from r in obj where (r is Project) select r;
            //ListBox_Project.ItemsSource = Q;
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Save();
        }
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBox_All.ItemsSource = obj;
        }

        private void Open_Clicked(object sender, RoutedEventArgs e)
        {
 
            Save();
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ResearcherObservable.Load(ofd.FileName, ref obj);
                Update_Items();
            }

        }

        private void New_Clicked(object sender, RoutedEventArgs e)
        {
            Save();
            obj = new ResearcherObservable("Mary", "Tsukanova");
           // this.DataContext = obj;
            Update_Items();
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                ResearcherObservable.Save(sfd.FileName, ref obj);
            }

        }

        private void Button_AddProject(object sender, RoutedEventArgs e)
        {
            obj.AddDefaultProject();
           
        }

        private void Button_AddConsulting(object sender, RoutedEventArgs e)
        {
            obj.AddDefaultConsulting();
            
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            obj.AddDefaults();
            
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            obj.RemoveActivityAt(ListBox_All.SelectedIndex);
            
        }

        private void Button_AddCustomProject(object sender, RoutedEventArgs e)
        {
            
            obj.Add(pr_obj.DeepCopy() as Project);
        }

        private void RadioButton_Clicked(object sender, RoutedEventArgs e)
        {
            RadioButton Rb = sender as RadioButton;
            DataTemplate tmp = (DataTemplate)TryFindResource("myAllTemplate");
            if (Rb.Name == "RadioButton_WithoutTemplate")
            {
                if (Rb.IsChecked == true)
                {
                    ListBox_All.ItemTemplate = null;
                }
                else
                {
                    ListBox_All.ItemTemplate = tmp;
                }
            }
            else
                 if (Rb.Name == "RadioButton_WithTemplate")
                 {
                    if (Rb.IsChecked == true)
                    {
                        ListBox_All.ItemTemplate = tmp;
                    }
                    else
                    {
                        ListBox_All.ItemTemplate = null;
                    }
                 }

        }

        
    }
}
