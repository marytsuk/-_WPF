using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    [Serializable]
    public class Consulting : Activity, IDeepCopy, IComparable<Consulting>
    {
        public bool IfInter { get; set; }
        public double Finance { get; set; }
        public Consulting(DateTime t1, DateTime t2, bool i, double f, string a1 = "Default" )
            : base( t1, t2, a1)
        {
            IfInter = i;
            Finance = f;
        }
        public int CompareTo(Consulting ob)
        {
            return this.Finance.CompareTo(ob.Finance);
        }
        public override string ToString()
        {
            return "Name :  " + Name + "\nBegin : " + BegEnd[0].ToLongDateString() +
                ",  End :  " + BegEnd[1].ToLongDateString()
                + "\nIs It International? :  " + IfInter.ToString() + "\nFinance :  " + Finance.ToString();
        }
        public override object DeepCopy()
        {
            return new Consulting( base.BegEnd[0], base.BegEnd[1], IfInter, Finance, base.Name);
        }
        //public override int GetHashCode()
        //{
        //    return (Name.GetHashCode() + BegEnd[0].GetHashCode() + BegEnd[1].GetHashCode() + IfInter.GetHashCode() + Finance.GetHashCode());
        //}
    }
}
