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
    public String toString() {
        return "TestPojo [f1=" + f1 + ", f2=" + f2 + ", f3=" + f3 + "]";
    }

}
