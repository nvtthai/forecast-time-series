package cse;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.util.List;

public class TabuSearch {

	//private TabuSearchSolution best;
	
	// maximum number of iterations
	private int itermax;
	
	// maximum number of successive iterations where f(x*) does not improve
	private int nitermax;

	private TabuSearchSolution best;
	
	private TabuSearchSolution bestRMSE;

	public TabuSearch(int itermax, int nitermax) {
		super();
		this.itermax = itermax;
		this.nitermax = nitermax;
	}

	public TabuSearchSolution getBest() {
		return best;
	}

	public void setBest(TabuSearchSolution best) {
		this.best = best;
	}

	public void run(double[] ts, PrintStream ps) {
		// Initialize
		long start = System.currentTimeMillis();
		TabuSearchSolution s0 = TabuSearchSolution.initialize();
		best = new TabuSearchSolution(s0);
		TabuSearchSolution x = best;
		bestRMSE = best;
		Utils.printFooter(System.currentTimeMillis() - start);
		Utils.printFooter(ps, System.currentTimeMillis() - start);
		
		int iter = 0;
		int niter = 0;
		
		boolean stop = false;
		//double mean = Utils.calcSampleMean(ts);
		while (!stop) {
			iter++;
			niter++;
			//if (iter % 10 == 0) {
			    System.out.println("iteration = " + iter + " successive iterations where f(x*) does not improve = " +
			        niter + " | " + x.getSolution().getRmse() + " " + x.getCost() + " " + x.asKey1() + " " + bestRMSE.getSolution().getRmse());
			//}
			
			List<TabuSearchSolution> moves = x.generateLocalMoves(bestRMSE.getSolution().getRmse());
			while (moves.isEmpty() && x.getNext() != null) {
			    TabuSearchSolution tmp = x;
				x = x.getNext();
				tmp.setNext(null);
				moves = x.generateLocalMoves(bestRMSE.getSolution().getRmse());
			}
			if (moves.isEmpty()) {
			    stop = true;
			    continue;
			}
			
			x = moves.get(0);
			if (Double.compare(x.getCost(), best.getCost()) < 0 ||
					Double.compare(x.getSolution().getRmse(), best.getSolution().getRmse()) < 0) {
				best = x;
				niter = 0;
				/*double rmsetest = best.getSolution().calcRmseForTestSet(ts);
				System.out.printf("%d: RMSE = %f RMSEtest = %f NMSETest = %f BIC = %f\n", iter, 
						best.getSolution().getRmse(), rmsetest, best.getSolution().getNmseTest(), best.getBic());
				ps.printf("%d: RMSE = %f RMSEtest = %f NMSETest = %f BIC = %f\n", iter,
						best.getSolution().getRmse(), rmsetest, best.getSolution().getNmseTest(), best.getBic());
				best.getSolution().exportToXML(ps);
				best.getSolution().calcRmseForTestSet(ts, ps);
				ps.println("--------------------------------------------------------------------------");*/
				
				if (Double.compare(best.getSolution().getRmse(), bestRMSE.getSolution().getRmse()) < 0) {
				    bestRMSE = best;
				}
			}
			
			if (iter == itermax || niter == nitermax) {
				stop = true;
			}
		}
		
		ps.println("\n BEST BIC " + iter + " " + niter);
        double rmsetest = best.getSolution().calcRmseForTestSet(ts, ps);
        ps.printf("%d: RMSE = %f RMSEtest = %f NMSETest = %f BIC = %f\n", iter,
                best.getSolution().getRmse(), rmsetest, best.getSolution().getNmseTest(), best.getBic());
        best.getSolution().exportToXML(ps);
        ps.println("\n END OF BEST BIC");
        
        ps.println("----------------------------------------------------");
		
		ps.println("\n BEST RMSE");
		rmsetest = bestRMSE.getSolution().calcRmseForTestSet(ts, ps);
		ps.printf("%d: RMSE = %f RMSEtest = %f NMSETest = %f BIC = %f\n", iter,
		        bestRMSE.getSolution().getRmse(), rmsetest, bestRMSE.getSolution().getNmseTest(), bestRMSE.getBic());
		bestRMSE.getSolution().exportToXML(ps);
		ps.println("\n END OF BEST RMSE");
	}
	
	/**
	 * @param args
	 * @throws IOException 
	 * @throws NumberFormatException 
	 */
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
		
		//Configuration.LOW_POP_SIZE = 50;
		//Configuration.LOW_MAX_GENERATION = 1000;
		Configuration.LOW_POP_SIZE = Integer.valueOf(args[2]);
        Configuration.LOW_MAX_GENERATION = Integer.valueOf(args[3]); 
		//TabuSearch tabu = new TabuSearch(1000, 100);
		TabuSearch tabu = new TabuSearch(Integer.valueOf(args[4]), Integer.valueOf(args[5]));

		PrintStream ps = new PrintStream(new FileOutputStream(args[1]));
		tabu.run(ts, ps);
		tabu.getBest().getSolution().calcRmseForTestSet(ts, ps);
		Utils.printFooter(ps, System.currentTimeMillis() - start);
		ps.close();
		
		Utils.printFooter(System.currentTimeMillis() - start);
	}
}
