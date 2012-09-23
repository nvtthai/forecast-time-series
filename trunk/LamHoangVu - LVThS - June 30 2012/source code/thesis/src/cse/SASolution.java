package cse;

public class SASolution {
	private boolean[] b = new boolean[Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER];
	private ARMAChromosome solution;
	private double cost;
	
	public SASolution(boolean[] b) {
		super();
		this.b = b;
	}

	public SASolution(SASolution saSolution) {
		super();
		for (int i = 0; i < saSolution.b.length; i++)
			b[i] = saSolution.b[i];
		solution = new ARMAChromosome(saSolution.getSolution());
		cost = saSolution.cost;
	}

	public ARMAChromosome getSolution() {
		return solution;
	}

	public void setSolution(ARMAChromosome solution) {
		this.solution = solution;
	}
	
	public static SASolution initialize() {
		boolean[] b = new boolean[Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER];
		for (int i = 0; i < b.length; i++)
			b[i] = Utils.nextBoolean();
		SASolution s0 = new SASolution(b);
		s0.findSolution();
		s0.setCost(s0.calcBIC());
		return s0;
	}
	
	public void findSolution() {
		// determine order of ARMA
		int q = Configuration.MAX_AR_ORDER + Configuration.MAX_MA_ORDER;
		while (!b[q-1]) q--;
		q = q - Configuration.MAX_AR_ORDER;
		
		int p = Configuration.MAX_AR_ORDER;
		while (!b[p-1]) p--;
		
		Model model = new Model(p, q);
		GeneticEngine ge = new GeneticEngine(model, b);
		ge.evolve();
		
		this.setSolution(ge.getElitism());
	}

	public SASolution selectNeighbor() {
		SASolution ns;
		int choice = Utils.nextInt(3);
		if (choice == 0) {
			// perturbation
			ns = new SASolution(b);
			ns.flipAllCoeffWithProb(0.2);
			ns.findSolution();
		} else if (choice==1) {
			// swap two value of two 
			ns = new SASolution(this);
			ns.solution.swapCoeff();
		} else {
			ns = new SASolution(b);
			ns.flipOneCoeff();
			ns.findSolution();
		}
		
		ns.setCost(calcBIC());
		return ns;
	}

	private void flipOneCoeff() {
		int pos = Utils.nextInt(b.length);
		b[pos] = !b[pos];
	}

	private void flipAllCoeffWithProb(double prob) {
		for (int i = 0; i < b.length; i++)
			if (Utils.nextDouble() <= prob) {
				b[i] = !b[i];
			}
	}
	
	public double calcBIC() {
		// the number of model parameters
		int numparam = 0;
		for (int i = 0; i < b.length; i++)
			if (b[i]) numparam++;
		
		double SSE = solution.getSse();
		int N = GeneticEngine.ts.length - Math.max(solution.getP(), solution.getQ());
		double bic = N *Math.log(SSE/N) + numparam*Math.log(N);
		return bic;
	}

	public double getCost() {
		return cost;
	}

	public void setCost(double cost) {
		this.cost = cost;
	}
}
