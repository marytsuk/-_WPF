using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
namespace DataLib
{
    
    interface IDeepCopy
    {
        object DeepCopy();
    }
    [Serializable]
    public class Activity : IDeepCopy
    {

        public string Name { get; set; }
        public System.DateTime[] BegEnd { get; set; }
        public Activity(DateTime t1, DateTime t2, string a1 = "Math")
        {
            BegEnd = new DateTime[2];
            BegEnd[0] = t1;
            BegEnd[1] = t2;
            Name = a1;
        }
        //public DateTime first_date
        //{
        //    get { return Begin[0]; }
        //}
        public override string ToString()
        {
            return "Name :  " + Name + "\nBegin: " + BegEnd[0].ToLongDateString() 
                + ",  End :  " + BegEnd[1].ToLongDateString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Activity ob = obj as Activity;
            if (ob as Activity == null)
                return false;
            return (ob.BegEnd[0] == this.BegEnd[0] && ob.BegEnd[1] == this.BegEnd[1] && ob.Name == this.Name);
        }
        //public static bool operator ==(Activity ob1, Activity ob2)
        //{
        //    if (Object.ReferenceEquals(ob1, null) && Object.ReferenceEquals(ob2, null))
        //        return true;
        //    else
        //        if (Object.ReferenceEquals(ob1, null) || Object.ReferenceEquals(ob2, null))
        //        return false;

        //    // return (ob1.Name == ob2.Name && ob1.BegEnd[0] == ob2.BegEnd[0] && ob1.BegEnd[1] == ob2.BegEnd[1]);
        //    return ob1.Equals(ob2);
        //}
        //public static bool operator !=(Activity ob1, Activity ob2)
        //{
        //    // return (!(ob1.Name == ob2.Name && ob1.BegEnd[0] == ob2.BegEnd[0] && ob1.BegEnd[1] == ob2.BegEnd[1]));
        //    // return !(ob1.Equals(ob2));
        //    return (!(ob1 == ob2));
        //}
        //public override int GetHashCode()
        //{
        //    return (Name.GetHashCode() + BegEnd[0].GetHashCode() + BegEnd[1].GetHashCode());
        //}
        virtual public object DeepCopy()
        {
            return new Activity(BegEnd[0], BegEnd[1], Name);
        }
    }
}