package org.mydotey.scf.yaml;

import java.util.List;

/**
 * @author koqizhao
 *
 * Jul 25, 2018
 */
public class TestPojo {

    private String f1;
    private int f2;
    private List<Integer> f3;

    public String getF1() {
        return f1;
    }

    public void setF1(String f1) {
        this.f1 = f1;
    }

    public int getF2() {
        return f2;
    }

    public void setF2(int f2) {
        this.f2 = f2;
    }

    public List<Integer> getF3() {
        return f3;
    }

    public void setF3(List<Integer> f3) {
        this.f3 = f3;
    }

    @Override
    public int hashCode() {
        final int prime = 31;
        int result = 1;
        result = prime * result + ((f1 == null) ? 0 : f1.hashCode());
        result = prime * result + f2;
        result = prime * result + ((f3 == null) ? 0 : f3.hashCode());
        return result;
    }

    // required for value change compare
    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (getClass() != obj.getClass())
            return false;
        TestPojo other = (TestPojo) obj;
        if (f1 == null) {
            if (other.f1 != null)
                return false;
        } else if (!f1.equals(other.f1))
            return false;
        if (f2 != other.f2)
            return false;
        if (f3 == null) {
            if (other.f3 != null)
                return false;
        } else if (!f3.equals(other.f3))
            return false;
        return true;
    }

    @Override
    public String toString() {
        return "TestPojo [f1=" + f1 + ", f2=" + f2 + ", f3=" + f3 + "]";
    }

}
