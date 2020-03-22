using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    [Serializable]
    public class Project : Activity, IDeepCopy
    {
        public int Count { get; set; }
        public string Theme { get; set; }
        public Project( DateTime t1 , DateTime t2 , int c, string t , string a1 = "Default") : base( t1, t2, a1)
        {
            Count = c;
            Theme = t;
        }
        public override string ToString()
        {
            return "Name :  " + Name + "\nBegin :  " + BegEnd[0].ToLongDateString() +
                ",  End :  " + BegEnd[1].ToLongDateString() + 
                "\nCount_Of_Members :  " + Count.ToString() + "\nTheme :  " + Theme;
        }
        public override object DeepCopy()
        {
            return new Project( base.BegEnd[0], base.BegEnd[1], Count, Theme, base.Name);
        }
        
        //public override int GetHashCode()
        //{
        //    return (Name.GetHashCode() + BegEnd[0].GetHashCode() + BegEnd[1].GetHashCode() + Count.GetHashCode() + Theme.GetHashCode());
        //}
    }
}
