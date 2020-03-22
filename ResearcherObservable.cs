using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Globalization;

namespace DataLib
{
    [Serializable]
    public class ResearcherObservable: ObservableCollection<Activity>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        private List<string> Org;
       
        public ResearcherObservable(string name = "Mary", string sur = "Tsukanova")
        {
            Name = name;
            Surname = sur;
            Org = new List<string>();
            Org.Add("Default");
            Org.Add("Groupe Auchan");
            Org.Add("Metro Group");
            Org.Add("Toyota Motor");
            Org.Add("Toyota Motor");
            Org.Add("Volkswagen Group");
            Org.Add("Japan Tobacco International");
            Org.Add("Philip Morris International");
            Org.Add("IKEA");
            IfChanged = false;
            base.CollectionChanged += Handler;
          
        }
        private bool chnd;
       
        public bool IfChanged
        {
            get
            {
                return chnd;
            }
            set
            {
                chnd = value;
                NotifyPropertyChanged("IfChanged");
            }
        }
        public void AddDefaults()
        {
            base.Add(new Consulting( new DateTime(2018, 09, 28), new DateTime(2019, 09, 30), true, 1400));
            base.Add(new Activity( new DateTime(2018, 10, 14), new DateTime(2019, 10, 14)));
            base.Add(new Project( new DateTime(2019, 10, 17), new DateTime(2019, 10, 18), 5, "Math"));
           
        }
        
        public List<string> Orgs
        {
            get { return Org; }
            set { }
        }
        public int Count_Of_Projects
        {
            get { return base.Count; }
        }
        public void AddActivity(params Activity[] activities)
        {
           for (int i = 0; i < activities.Length; i++)
            {
                base.Add(activities[i]);
            }
        }
        public void RemoveActivityAt(int index)
        {
            try
            {
                base.RemoveAt(index);
            }
            catch (Exception ex)
            {

                //Console.WriteLine(ex.Message);
                MessageBox.Show( ex.Message,"Error!");
            }
          
        }
        public void AddDefaultConsulting()
        {
            base.Add(new Consulting( new DateTime(2018, 11, 28), new DateTime(2019, 1, 30), true, 1500));
         
        }
        public void AddDefaultProject()
        {
            base.Add(new Project( new DateTime(2019, 10, 17), new DateTime(2019, 10, 18), 7, "Sport"));
            //NotifyPropertyChanged("Get_Count_Of_Projects");
        }
        public override string ToString()
        {
            return base.ToString() + "\nResearcher :  "+ Name + " " + Surname + "\nThe Organisation :  " + Org;
        }
        public static bool Save(string filename, ref ResearcherObservable obj)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                //Console.WriteLine("Исключение: " + ex.Message);
                return false;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
            obj.IfChanged = false;
            return true;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void Handler(object source, NotifyCollectionChangedEventArgs args)
        {
            ((ResearcherObservable)source).IfChanged = true;
            
        }

        public static bool Load(string filename, ref ResearcherObservable obj)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                obj = binaryFormatter.Deserialize(fileStream) as ResearcherObservable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                //Console.WriteLine("Исключение: " + ex.Message);
                return false;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
            obj.IfChanged = false;
            obj.CollectionChanged += Handler;
            return true;
        }

        

        public IEnumerable<Project> Get_Projects
        {
            get
            {
                var Q = from r in this where (r is Project) select (r as Project);
                return Q;
            }
        }

        public int Get_Count_Of_Projects
        {
            get
            {
                return Get_Projects.Count();
            }
        }

        public IEnumerable<Activity> Get_All
        {
            get
            {
                var Q = from r in this where (r is Activity) select (r as Activity);
                return Q;
            }
        }


        //public Project Selected_Project
        //{
        //    set
        //    {
        //        Selected_Project = value;
        //        NotifyPropertyChanged("Selected_Project");
        //    }

        //}

    }
}
