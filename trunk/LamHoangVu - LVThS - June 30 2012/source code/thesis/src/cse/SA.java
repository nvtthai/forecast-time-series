package cse;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;

public class SA {
	//private ARMAChromosome s0;
	private double t0;
	private int maxphase;
	private int nrep; //number of interations
	private double alpha; //temperature reduction coefficient
	
	public SA(double t0, int maxphase, int nrep, double alpha) {
		super();
		//this.s0 = s0;
		this.t0 = t0;
		this.maxphase = maxphase;
		this.nrep = nrep;
		this.alpha = alpha;
	}
	
	public SASolution runSA(PrintStream ps) {
		
		//Initialize a solution s0
		SASolution s0 = SASolution.initialize();
		SASolution bestS = new SASolution(s0);
		
		double t = t0;
		int phase = 0;
		int bestphase = 0;
		ps.println("Initialize a solution");
		ps.printf("rmse = %.3f  bic = %.3f\n", bestS.getSolution().getRmse(), bestS.getCost());
		bestS.getSolution().info(ps);
		ps.println("---------------\n");
		
		while (phase < maxphase) {
			int iterationCount = 0;
			while (iterationCount < nrep) {
				// Randomly select s belonging N(s0);
				SASolution s = s0.selectNeighbor();
				
				// compute the change in cost function
				double delta = s.getCost() - s0.getCost();
				if (delta < 0) {
					s0 = s;
					if (s0.getCost() < bestS.getCost()) {
						bestS = new SASolution(s0);
						bestphase = phase;
						ps.printf("----- phase = %d  iteration = %d ", phase, iterationCount);
						ps.printf("rmse = %3f  bic = %3f\n", bestS.getSolution().getRmse(), bestS.getCost());
						bestS.getSolution().info(ps);
						ps.println("---------------\n");
						System.out.println("At phase " + bestphase
								+ " with rmse = " + bestS.getSolution().getRmse()
								+ " BIC = " + bestS.getCost());
					}
				} else {
					if (Utils.nextDouble() < Math.exp(-delta/t)) {
						s0 = s;
					}
				}
				iterationCount++;
			}
			t = t*alpha;
			phase++;
		}
		
		System.out.println("Best solution reachs at phase " + bestphase
				+ " with rmse = " + bestS.getSolution().getRmse());
		return bestS;
	}
	
	public static void main(String[] args) throws NumberFormatException, IOException {
		long start = System.currentTimeMillis();
		
		System.out.println("Data set: " + args[0]);
		DataSet ds = new DataSet(args[0]);
		double[] ts = ds.loadData(1);
		
		// Assign trained set
		int trainNum = (int) (0.9*ts.length);
		GeneticEngine.ts = new double[trainNum];
		for (int i = 0; i < trainNum; i++) {
			GeneticEngine.ts[i] = ts[i];
		}
		
		SA sa = new SA(10000, 5000, 5, 0.98);
		//SA sa = new SA(10, 5, 5, 0.98);
		PrintStream ps = new PrintStream(new FileOutputStream(args[1]));
		SASolution s = sa.runSA(ps);
		
		s.getSolution().calcRmseForTestSet(ts, ps);
		ps.println("Runtime: " + Utils.formatRuntime(System.currentTimeMillis() - start));
		Utils.printFooter(System.currentTimeMillis() - start);
	}
}
