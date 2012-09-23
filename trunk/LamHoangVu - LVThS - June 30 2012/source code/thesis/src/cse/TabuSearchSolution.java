package cse;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TabuSearchSolution {
	private boolean[] b = new boolean[Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER];
	private ARMAChromosome solution;
	private double bic;
	
	private TabuSearchSolution next = null;
	//private TabuSearchSolution prev = null;
	
	private static Map<Integer, Boolean> tabuHash = new HashMap<Integer, Boolean>();
	
	public ARMAChromosome getSolution() {
		return solution;
	}

	public void setSolution(ARMAChromosome solution) {
		this.solution = solution;
	}
	
	public double getBic() {
		return bic;
	}

	public void setBic(double bic) {
		this.bic = bic;
	}

	public TabuSearchSolution getNext() {
		return next;
	}

	public void setNext(TabuSearchSolution next) {
		this.next = next;
	}

	/*public TabuSearchSolution getPrev() {
		return prev;
	}

	public void setPrev(TabuSearchSolution prev) {
		this.prev = prev;
	}*/

	public TabuSearchSolution(boolean[] b) {
		super();
		this.b = b;
	}
	
	public TabuSearchSolution(TabuSearchSolution s) {
		super();
		for (int i = 0; i < s.b.length; i++) b[i] = s.b[i];
		solution = new ARMAChromosome(s.getSolution());
		bic = s.bic;
	}

	public static TabuSearchSolution initialize() {
		boolean[] b = new boolean[Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER];
		for (int i = 0; i < b.length; i++) b[i] = true;
		TabuSearchSolution s0 = new TabuSearchSolution(b);
		s0.findSolution();
		return s0;
	}
	
	public void findSolution() {
		// determine order of ARMA
		int q = Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER;
		while (q > Configuration.MAX_AR_ORDER && !b[q-1]) q--;
		q = q - Configuration.MAX_AR_ORDER;
		
		int p = Configuration.MAX_AR_ORDER;
		while (p > 0 && !b[p-1]) p--;
		
		Model model = new Model(p, q);
		GeneticEngine ge = new GeneticEngine(model, b);
		ge.evolve();
		
		ARMAChromosome elitism = ge.getElitism();
		this.setSolution(elitism);
		for (int i = 0; i < elitism.getP() + elitism.getQ(); i++) {
			if (getBit(i) && Double.compare(elitism.getCoeff(i), 0)==0) {
				setBit(i, false);
			}
		}
		elitism.updateOrder(b);
		elitism.setRmse(GeneticEngine.ts);
		
		this.calcBIC();
	}

	private void calcBIC() {
		// the number of model parameters
		int numparam = 0;
		for (int i = 0; i < b.length; i++)
			if (b[i]) numparam++;
		
		double SSE = solution.getSse();
		int N = GeneticEngine.ts.length - Math.max(solution.getP(), solution.getQ());
		//int N = GeneticEngine.ts.length;
		this.bic = N*Math.log(SSE/N) + numparam*Math.log(N);
	}

	public List<TabuSearchSolution> generateLocalMoves(double bestrmse) {
		List<Coefficient> coeffs = new ArrayList<Coefficient>();
		double average = 0;
		int sumorder = solution.getP() + solution.getQ();
		for (int i = 0; i < sumorder ; i++) {
			if (!getBit(i)) continue;
			coeffs.add( new Coefficient(Math.abs(solution.getCoeff(i)), i) );
			average += Math.abs(solution.getCoeff(i));
		}
		average = average/coeffs.size();
		Coefficient.sortCoeffs(coeffs);
		
		List<TabuSearchSolution> neighboors = new ArrayList<TabuSearchSolution>();
		if (coeffs.size() < 3) return  neighboors;
		for (int i = 0; i < coeffs.size(); i++) {
			if (Double.compare(coeffs.get(i).getValue(), average) < 0) {
				TabuSearchSolution neighboor = new TabuSearchSolution(this);
				int index = coeffs.get(i).getIndex();
				neighboor.b[index] = false;
				neighboor.getSolution().setCoeff(index, 0);
				neighboor.getSolution().updateOrder(b);
				neighboor.getSolution().setRmse(GeneticEngine.ts);
				neighboor.calcBIC();
				if (Double.compare(neighboor.getSolution().getRmse(),
				        this.getSolution().getRmse()) > 0) {
					neighboor.findSolution();
				}
				/*if (Double.compare(neighboor.getSolution().getRmse(), this.getSolution().getRmse()) > 0) {
					neighboor.findSolution();
				}*/
				
				if (!tabuHash.containsKey(neighboor.asKey()) ||
						Double.compare(neighboor.getSolution().getRmse(), bestrmse) < 0) {
					tabuHash.put(neighboor.asKey(), true);
					//neighboor.setPrev(this);
					neighboors.add(neighboor);
				}
			}
		}
		
		if (neighboors.size() > 0) {
			sortSolution(neighboors);
			for (int i = 0; i < neighboors.size()-1; i++)
				neighboors.get(i).setNext(neighboors.get(i+1));
			neighboors.get(neighboors.size()-1).setNext(this.getNext());
		}
		
		return neighboors;
	}
	
	private boolean getBit(int index) {
		if (index < this.solution.getP())
			return this.b[index];
		return this.b[Configuration.MAX_AR_ORDER + (index - this.solution.getP())];
	}
	
	private void setBit(int index, boolean c) {
		if (index < this.solution.getP()) {
			this.b[index] = c;
		} else {
			this.b[Configuration.MAX_AR_ORDER + (index - this.solution.getP())] = c;
		}
	}

	public int asKey() {
		String str = "";
		for (int i = 0; i < b.length; i++)
			if (b[i]) {
				str += "1";
			} else {
				str += "0";
			}
		return str.hashCode();
	}
	
	public String asKey1() {
		String str = "AR(";
		for (int i = 0; i < solution.getP()-1; i++) {
			if (b[i]) {
				str += (i + 1) + ", ";
			}
		}
		str += solution.getP() + ") MA(";
		for (int i = 0; i < solution.getQ()-1; i++) {
			if (b[Configuration.MAX_MA_ORDER + i]) {
				str += (i + 1) + ", ";
			}
		}
		str += solution.getQ() + ")";
		return str;
		/*for (int i = 0; i < b.length; i++)
			if (b[i]) {
				str += "1";
			} else {
				str += "0";
			}
		return str + "p=" + solution.getP() + "q=" + solution.getQ();*/
	}

	public double getCost() {
		//return solution.getRmse();
		return bic;
	}
	
	private static void sortSolution(List<TabuSearchSolution> solutions) {
		Collections.sort(solutions, new Comparator<TabuSearchSolution>() {
			@Override
			public int compare(TabuSearchSolution o1, TabuSearchSolution o2) {
				double cost1 = o1.getSolution().getRmse();
				double cost2 = o2.getSolution().getRmse();
				// sort incrementally
				return (cost1 > cost2 ? 1 : (cost1 < cost2 ? -1 : 0));
			}
		});
	}
	
	static class Coefficient {
		private double value;
		private int index;
		
		public Coefficient(double value, int index) {
			super();
			this.value = value;
			this.index = index;
		}

		public double getValue() {
			return value;
		}

		public void setValue(double value) {
			this.value = value;
		}

		public int getIndex() {
			return index;
		}

		public void setIndex(int index) {
			this.index = index;
		}
		
		static void sortCoeffs(List<Coefficient> coeffs) {
			Collections.sort(coeffs, new Comparator<Coefficient>() {
				@Override
				public int compare(Coefficient o1, Coefficient o2) {
					double cost1 = o1.getValue();
					double cost2 = o2.getValue();
					return (cost1 > cost2 ? 1 : (cost1 < cost2 ? -1 : 0));
				}
			});
		}
	}
}
