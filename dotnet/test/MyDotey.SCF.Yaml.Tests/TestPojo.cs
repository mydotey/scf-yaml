using System;
using System.Collections.Generic;

namespace MyDotey.SCF.Yaml
{
    /**
     * @author koqizhao
     *
     * Jul 25, 2018
     */
    public class TestPojo
    {
        public String F1 { get; set; }
        public int F2 { get; set; }
        public List<int> F3 { get; set; }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((F1 == null) ? 0 : F1.GetHashCode());
            result = prime * result + F2;
            result = prime * result + ((F3 == null) ? 0 : F3.GetHashCode());
            return result;
        }

        // required for value change compare
        public override bool Equals(Object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            TestPojo other = (TestPojo)obj;
            if (F1 == null)
            {
                if (other.F1 != null)
                    return false;
            }
            else if (!F1.Equals(other.F1))
                return false;
            if (F2 != other.F2)
                return false;
            if (F3 == null)
            {
                if (other.F3 != null)
                    return false;
            }
            else if (!F3.Equal(other.F3))
                return false;
            return true;
        }

        public override string ToString()
        {
            return "TestPojo [f1=" + F1 + ", f2=" + F2 + ", f3=[" + (F3 == null ? null : string.Join(", ", F3)) + "]]";
        }
    }
}