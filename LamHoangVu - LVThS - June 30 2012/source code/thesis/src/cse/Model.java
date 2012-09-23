package cse;

public class Model {
	// the order of autoregression
    private int p;

    // the order of difference
    // private int d = 0;

    // the order of moving average
    private int q;

    //private double[] ts; //note that only trained set is load into this
    
    public Model(int p, int q) {
		super();
		this.p = p;
		this.q = q;
	}

    public int getAROrder() {
        return p;
    }

    public int getMAOrder() {
        return q;
    }
    
    public int getNumParams() {
    	return p+q;
    }
}
