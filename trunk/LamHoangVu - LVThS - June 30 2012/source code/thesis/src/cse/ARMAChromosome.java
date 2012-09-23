package cse;

import java.io.PrintStream;
import java.util.Random;


public class ARMAChromosome {
	//private double[] coefficient;
    static double g0 = 0;
	private double[] pcoeff = new double[Configuration.MAX_AR_ORDER];
	private double[] qcoeff = new double[Configuration.MAX_MA_ORDER];
	private int p;
	private int q;
	private double rmse;
	private double sse;
	private double nmseTest;
	private int fitness;
	public boolean mutatation = false;
	public String parent = null;

	public ARMAChromosome(boolean[] bcoeff, int p, int q) {
		this.p = p;
		this.q = q;
		
		for (int i = 0; i < Configuration.MAX_AR_ORDER; i++) {
			if (bcoeff[i]) {
				this.pcoeff[i] = Utils.nextDouble(-1, 1);
			} else {
				this.pcoeff[i] = 0;
			}
		}
		
		for (int i = 0; i < Configuration.MAX_MA_ORDER; i++) {
			if (bcoeff[Configuration.MAX_AR_ORDER+i]) {
				this.qcoeff[i] = Utils.nextDouble(-1, 1);
			} else {
				this.qcoeff[i] = 0;
			}
		}
	}

	public ARMAChromosome(ARMAChromosome armaChromosome) {
		for (int i = 0; i < Configuration.MAX_AR_ORDER; i++)
			this.pcoeff[i] = armaChromosome.pcoeff[i];
		
		for (int i = 0; i < Configuration.MAX_MA_ORDER; i++)
			this.qcoeff[i] = armaChromosome.qcoeff[i];
		
		this.p = armaChromosome.p;
		this.q = armaChromosome.q;
		
		this.rmse = armaChromosome.rmse;
		this.fitness = armaChromosome.fitness;
		this.sse = armaChromosome.sse;
	}

	public ARMAChromosome() {
	    super();
	    for (int i = 0; i < Configuration.MAX_AR_ORDER; i++) this.pcoeff[i] = 0;
        
        for (int i = 0; i < Configuration.MAX_MA_ORDER; i++) this.qcoeff[i] = 0;
    }

    public double getSse() {
		return sse;
	}

	public void setSse(double sse) {
		this.sse = sse;
	}

	public void setRmse(double[] ts) {
		this.rmse = Utils.round( calcRmse(ts) );
	}
	
	public double getRmse() {
		return rmse;
	}

    public double getNmseTest() {
		return nmseTest;
	}

	public void setNmseTest(double nmseTest) {
		this.nmseTest = nmseTest;
	}

	private double calcRmse(double[] ts) {
    	this.sse = calcSSE(ts);
        return Utils.round(Math.sqrt( this.sse/(ts.length - Math.max(p, q)) ));
    }
    
    private double calcSSE(double[] ts) {
    	Double sse = new Double(0);
    	int n = ts.length;
        
        double[] e = new double[n];
        double f = 0;
        for (int i = 0; i < n; i++) {
        	if (i < Math.max(p, q)) {
        		e[i] = 0;
        	} else {
        		e[i] = ts[i] - f;
        	}
        	f = 0;
        	
        	for (int j = 0; j < Math.min(i+1, p); j++)
        		f += this.pcoeff[j]*ts[i-j];
        	
        	for (int j = 0; j < Math.min(i+1, q); j++)
        		f += this.qcoeff[j]*e[i-j];
        	
        	if (i >= Math.max(p, q)) {
        		sse += Math.pow(e[i], 2);
        	}
        }
    	
        if (sse.isInfinite() || sse.isNaN()) {
        	return Double.MAX_VALUE;
        }
    	return Utils.round(sse);
    }
    
    protected double calcRmseForTestSet(double[] ts)
    {
    	double sse = 0;
    	int n = ts.length;
    	int trainNum = (int) (0.9*n);
		int testNum = n - trainNum;
		double[] e = new double[n];
		double f = g0;
		double tmp = 0;
		double sse1 = 0;
		double mean = Utils.calcSampleMean(ts);
        for (int i = 0; i < n-1; i++) {
        	if (i < Math.max(p, q)) {
        		e[i] = 0;
        	} else {
        		e[i] = ts[i] - f;
        	}
        	f = g0;
        	
        	for (int j = 0; j < Math.min(i+1, p); j++)
        		f += this.pcoeff[j]*ts[i-j];
        	
        	for (int j = 0; j < Math.min(i+1, q); j++)
        		f += this.qcoeff[j]*e[i-j];
        	
        	if (i >= trainNum-1) {
        		sse += Math.pow(ts[i+1]-f, 2);
        		tmp += Math.pow(ts[i+1]-mean, 2);
        	} else if (i >= Math.max(p, q)) {
                sse1 += Math.pow(e[i], 2);   
        	}
        }
        
        this.nmseTest = Utils.round(100*sse/tmp);
        return Utils.round(Math.sqrt(sse/testNum));
    }
    
    protected double calcRmseForTestSet(double[] ts, PrintStream ps) {
    	double sseTest = 0;
    	int n = ts.length;
    	int trainNum = (int) (0.9*n);
		int testNum = n - trainNum;
		double[] e = new double[n];
		double f = g0;
		double tmp = 0;
		ps.println("No      Real     Forecast");
		double mean = Utils.calcSampleMean(ts);
        for (int i = 0; i < n-1; i++) {
        	if (i < Math.max(p, q)) {
        		e[i] = 0;
        	} else {
        		e[i] = ts[i] - f;
        	}
        	f = g0;
        	
        	for (int j = 0; j < Math.min(i+1, p); j++)
        		f += this.pcoeff[j]*ts[i-j];
        	
        	for (int j = 0; j < Math.min(i+1, q); j++)
        		f += this.qcoeff[j]*e[i-j];
        	
        	if (i >= trainNum-1) {
        		sseTest += Math.pow(ts[i+1]-f, 2);
        		tmp += Math.pow(ts[i+1]-mean, 2);
        		ps.printf("%d: %.3f %.3f\n", i+1, ts[i+1], f);
        	}
        }
        
        double rmseTest = Utils.round(Math.sqrt(sseTest/testNum));
        double nmseTest = Utils.round(100*sseTest/tmp);
        ps.printf("RMSEtest = %f\n\n", rmseTest);
        ps.printf("NMSEtest = %f\n\n", nmseTest);
        return rmseTest;
    }
    
    protected double getCoeff(int index) {
    	if (index < this.p) {
    		return this.pcoeff[index];
    	} else {
    		return this.qcoeff[index-this.p];
    	}
	}
    
    protected void setCoeff(int index, double value) {
    	if (index < this.p) {
    		this.pcoeff[index] = value;
    	} else {
    		this.qcoeff[index-this.p] = value;
    	}
	}
    
	public static void crossover(ARMAChromosome chromosome1, ARMAChromosome chromosome2) {
		if (Utils.nextBoolean()) {
			// arithmetic crossover
			double lamda = Utils.nextDouble();
			for (int i = 0; i < chromosome1.getCoeffLength(); i++) {
				double a = chromosome1.getCoeff(i);
				double b = chromosome2.getCoeff(i);
				chromosome1.setCoeff(i, Utils.round(lamda*a + (1-lamda)*b));
				chromosome2.setCoeff(i, Utils.round(lamda*b + (1-lamda)*a));
			}
		} else {
			// single point crossover
			if (chromosome1.p > 1) {
				int pos = Utils.nextInt(chromosome1.p);
				for (int i = pos; i < chromosome1.p; i++) {
					double tmp = chromosome1.getCoeff(i);
					chromosome1.setCoeff(i, chromosome2.getCoeff(i));
					chromosome2.setCoeff(i, tmp);
				}
			}
			
			if (chromosome1.q > 1) {
				int pos = Utils.nextInt(chromosome1.q);
				for (int i = chromosome1.p + pos; i < chromosome1.getCoeffLength(); i++) {
					double tmp = chromosome1.getCoeff(i);
					chromosome1.setCoeff(i, chromosome2.getCoeff(i));
					chromosome2.setCoeff(i, tmp);
				}
			}
		}
	}
	
	public static void mutate(ARMAChromosome chromosome) {
		double[] tmp = new double[chromosome.getCoeffLength()];
		for (int i = 0; i < chromosome.getCoeffLength(); i++)
			tmp[i] = chromosome.getCoeff(i);
		
		double variance = Utils.calcSampleVariance(tmp);
		double mean = Utils.calcSampleMean(tmp);
		
		int choice = Utils.nextInt(3);
		if (choice == 0) {
			// uniform mutation
			int index;
			double gene;
			do {
				index = Utils.nextInt(chromosome.getCoeffLength());
				gene = chromosome.getCoeff(index);
			} while (gene==0);
			
			double addum = gauss(gene, mean, variance);
			//double addum = new Random().nextGaussian();
			chromosome.setCoeff(index, Utils.round(gene + addum));
		} else if (choice == 1) {
			// Relative Gaussian Pertubation
			for (int i = 0; i < chromosome.getCoeffLength(); i++) {
				double x = chromosome.getCoeff(i);
				if (x==0) continue;
				double newx = Utils.round(x*(1 + gauss(x, mean, variance)));
				chromosome.setCoeff(i, newx);
			}
		} else if (choice == 2) {
			//Zero-Preserving Gaussian Pertubation
			for (int i = 0; i < chromosome.getCoeffLength(); i++) {
				double x = chromosome.getCoeff(i);
				if (x==0) continue;
				double newx = Utils.round(x + gauss(x, mean, variance));
				chromosome.setCoeff(i, newx);
			}
		} else {
			System.out.println("Error in mutation");
		}
		
		chromosome.markMutation();
	}
	
	private static double gauss(double x, double mean, double variance) {
		//return (1/Math.sqrt(2*Math.PI*variance))*Math.exp(-Math.pow(x-mean, 2)/(2*variance));
		return new Random().nextGaussian();
	}

	private void markMutation() {
		this.mutatation = true;
	}

	public int getFitness() {
		return fitness;
	}

	public void setFitness(int fitness) {
		this.fitness = fitness;
	}

	public int getP() {
		return p;
	}

	public void setP(int p) {
		this.p = p;
	}

	public int getQ() {
		return q;
	}

	public void setQ(int q) {
		this.q = q;
	}

	public double[] getPcoeff() {
		return pcoeff;
	}

	public void setPcoeff(double[] pcoeff) {
		this.pcoeff = pcoeff;
	}

	public double[] getQcoeff() {
		return qcoeff;
	}

	public void setQcoeff(double[] qcoeff) {
		this.qcoeff = qcoeff;
	}
	
	private int getCoeffLength() {
		return p+q;
	}

	public double getCoeffAtMeta(int index) {
		if (index < Configuration.MAX_AR_ORDER) {
    		return this.pcoeff[index];
    	} else {
    		return this.qcoeff[index-Configuration.MAX_AR_ORDER];
    	}
	}
	
	public void setCoeffAtMeta(int index, double value) {
    	if (index < Configuration.MAX_AR_ORDER) {
    		this.pcoeff[index] = value;
    	} else {
    		this.qcoeff[index-Configuration.MAX_AR_ORDER] = value;
    	}
	}

	public int hashCode() {
		String str = "";
		for (int i = 0; i < getCoeffLength(); i++)
			str += "_" + Double.toString(getCoeff(i));
		return str.hashCode();
	}

	public void swapCoeff() {
		int pos1 = Utils.nextInt(p+q);
		int pos2 = Utils.nextInt(p+q);
		if (pos1==pos2) {
			if (pos1 > 0) {
				pos1--;
			} else {
				pos1++;
			}
		}
		double tmp = getCoeff(pos1);
		setCoeff(pos1, getCoeff(pos2));
		setCoeff(pos2, tmp);
		setRmse(GeneticEngine.ts);
	}

	public void info(PrintStream ps) {
		ps.printf("AR(");
		for (int i = 0; i < p-1; i++)
			if (pcoeff[i] != 0)
				ps.printf("%d, ", i+1);
		ps.printf("%d):", p);
		for (int i = 0; i < p; i++)
			ps.printf("   %.3f", Utils.round(pcoeff[i]));
		
		ps.printf("\nMA(");
		for (int i = 0; i < q-1; i++)
			if (qcoeff[i] != 0)
				ps.printf("%d, ", i+1);			
		ps.printf("%d):", q);
		for (int i = 0; i < q; i++)
			ps.printf("   %.3f", Utils.round(qcoeff[i]));
		ps.println();
	}
	
	public void exportToXML(PrintStream ps) {
		ps.printf("<ARMA p=\"%d\" q=\"%d\">\n", p, q);
		ps.printf("    <AR>\n");
		for (int i = 0; i < p; i++)
			if (pcoeff[i] != 0)
				ps.printf("        <coefficient lag=\"%d\" value=\"%.5f\"/>\n", i+1, pcoeff[i]);
		ps.printf("    <AR>\n");
		ps.printf("    <MA>\n");
		for (int i = 0; i < q; i++)
			if (qcoeff[i] != 0)
				ps.printf("        <coefficient lag=\"%d\" value=\"%.5f\"/>\n", i+1, qcoeff[i]);
		ps.printf("    <MA>\n");
		ps.printf("</ARMA>\n");
	}

	public void updateOrder(boolean[] b) {
		q = Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER;
		while (q > Configuration.MAX_AR_ORDER && !b[q-1]) q--;
		q = q - Configuration.MAX_AR_ORDER;
		
		p = Configuration.MAX_AR_ORDER;
		while (p > 0 && !b[p-1]) p--;
	}
}
