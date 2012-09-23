package cse;

import java.util.Random;

public class MetaChromosome {
	private boolean[] gene;
	private ARMAChromosome bestarma;
	private double fitness;
	
	private static Random random = new Random();

	public MetaChromosome(Model metaModel) {
		super();
		int chrsize = metaModel.getNumParams();
		initGene(chrsize);
		int p = metaModel.getAROrder();
		int q = metaModel.getMAOrder();
		while (q-1 > 0 && !gene[p+q-1]) q--;
		while (p-1 > 0 && !gene[p-1]) p--;
		boolean[] bcoeff = new boolean[p+q];
		for (int i = 0; i < p; i++)
			bcoeff[i] = gene[i];
		for (int i = 0; i < q; i++)
			bcoeff[p+i] = gene[metaModel.getAROrder() + i];
		Model model = new Model(p, q);
		GeneticEngine ge = new GeneticEngine(model, bcoeff);
		ge.evolve();
		this.setBestarma(ge.getElitism());
	}

	public MetaChromosome(MetaChromosome metaChromosome) {
		this.gene = new boolean[metaChromosome.gene.length];
		for (int i = 0; i < metaChromosome.gene.length; i++)
			this.gene[i] = metaChromosome.gene[i];
		this.bestarma = new ARMAChromosome(metaChromosome.bestarma);
		this.fitness = metaChromosome.fitness;
	}

	public boolean[] getGene() {
		return gene;
	}
	
	public boolean getGene(int i) {
		return gene[i];
	}

	public void setGene(boolean[] gene) {
		this.gene = gene;
	}
	
	public void setGene(int i, boolean value) {
		gene[i] = value;
	}
	
	public void setFitness(double fitness) {
		this.fitness = fitness;
	}
	
	public void setFitness() {
		this.fitness = calcFitness();
	}
	
	public double getFitness() {
		return this.fitness;
	}
	
	private double calcFitness() {
		// Bayesian Information Criterion (BIC)
		// the number of training samples N
		//int N = ge.getModel().getTs().length - Math.max(ge.getModel().getAROrder(), ge.getModel().getMAOrder());
		int N = MetaGeneticEngine.ts.length;
		// the number of model parameters
		int numparam = 0;
		for (int i = 0; i < gene.length; i++)
			if (gene[i]) numparam++;
		
		double SSE = getBestarma().getSse();
		double bic = N*Math.log(SSE /N) + numparam*Math.log(N);
		return bic;
	}
	
	public void initGene(int chrsize) {
		gene = new boolean[chrsize];
		for (int i = 0; i < chrsize; i++) {
			gene[i] = random.nextBoolean();
		}
	}

	public static void crossover(MetaChromosome chromosome1, MetaChromosome chromosome2) {
		int pos1 = random.nextInt(chromosome1.gene.length);
		int pos2 = random.nextInt(chromosome2.gene.length);
		while (pos2==pos1)
			pos2 = random.nextInt(chromosome2.gene.length);
		for (int i = Math.min(pos1, pos2); i < Math.max(pos1, pos2); i++) {
			boolean tmp = chromosome1.getGene(i);
			chromosome1.setGene(i, chromosome2.getGene(i));
			chromosome2.setGene(i, tmp);
			
			double tmpCoeff = chromosome1.getBestarma().getCoeffAtMeta(i);
			chromosome1.getBestarma().setCoeffAtMeta(i, chromosome2.getBestarma().getCoeffAtMeta(i));
			chromosome2.getBestarma().setCoeffAtMeta(i, tmpCoeff);
		}
		
		chromosome1.resetOrderOfARMA();
		chromosome2.resetOrderOfARMA();
	}

	private void resetOrderOfARMA() {
		int i = gene.length;
		while (i > Configuration.MAX_AR_ORDER && !gene[i-1]) i--;
		this.bestarma.setQ(i-Configuration.MAX_AR_ORDER);
		
		i = Configuration.MAX_AR_ORDER;
		while (i > 0 && !gene[i-1]) i--;
		this.bestarma.setP(i);
	}

	public static void mutate(MetaChromosome chromosome) {
		int pos = random.nextInt(chromosome.gene.length);
		if (chromosome.getGene(pos)) {
			chromosome.getBestarma().setCoeffAtMeta(pos, 0);
		} else {
			chromosome.getBestarma().setCoeffAtMeta(pos, Utils.nextDouble(-1, 1));
		}
		chromosome.setGene(pos, !chromosome.getGene(pos));
		chromosome.resetOrderOfARMA();
	}

	/*public void foo(Model metaModel, int lowPopSize) {
		int p = metaModel.getAROrder();
		int q = metaModel.getMAOrder();
		while (q-1 > 0 && !gene[p+q-1]) q--;
		while (p-1 > 0 && !gene[p-1]) p--;
		boolean[] bcoeff = new boolean[p+q];
		for (int i = 0; i < p; i++)
			bcoeff[i] = gene[i];
		for (int i = 0; i < q; i++)
			bcoeff[p+i] = gene[metaModel.getAROrder() + i];
		Model model = new Model(p, q);
		//Model model = new Model(p, q, metaModel.getTs());
		GeneticEngine ge = new GeneticEngine(lowPopSize, model, bcoeff);
		ge.evolve();
		this.setBestarma(ge.getBestChromosome());
	}*/

	public ARMAChromosome getBestarma() {
		return bestarma;
	}

	public void setBestarma(ARMAChromosome bestarma) {
		this.bestarma = bestarma;
	}
}
