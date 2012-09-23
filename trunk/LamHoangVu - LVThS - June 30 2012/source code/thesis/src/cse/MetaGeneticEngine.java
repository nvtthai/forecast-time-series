package cse;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.Random;

public class MetaGeneticEngine {
	private static Model metaModel;
	private static List<MetaChromosome> population = new ArrayList<MetaChromosome>();
	private MetaChromosome elitism;
	
	public static double[] ts;
	
	// Random generator
	protected static Random random = new Random();
	
	public static void setMetaModel(Model metaModel) {
		MetaGeneticEngine.metaModel = metaModel;
	}
	
	public static Model getMetaModel() {
		return metaModel;
	}

	public void initPop() {
		System.out.println("[INFO] - Initialize the population of meta level");
		long start = System.currentTimeMillis();
		for (int i = 0; i < Configuration.META_POP_SIZE; i++) {
			System.out.println("~~~~~~~~~~~~> " + i);
			MetaChromosome chromosome = new MetaChromosome(metaModel);
			chromosome.setFitness();
			population.add(chromosome);
		}
		orderPopulation();
		Utils.printFooter(System.currentTimeMillis() - start);
	}
	
	public void evolve() {
		initPop();
		/*for (int i = 0; i < population.size(); i++) {
			System.out.printf("%d %f %f\n", i, population.get(i).getBestarma().getRmse(),
					population.get(i).getBestarma().calcRmse(ts));
		}*/
		setElitism(getBestChromosome());
		
		for (int i = 0; i < Configuration.META_MAX_GENERATION; i++) {
			System.out.println("[INFO] - Evolve at the " + i + "'s generation of meta level");
			nextGeneration(selection(population.size()));
			if (elitism.getFitness() > population.get(population.size()-1).getFitness()) {
				System.out.printf("----------> %d %f %f %f %f\n", i, elitism.getFitness(),
						getBestChromosome().getFitness(),
						elitism.getBestarma().getRmse(),
						getBestChromosome().getBestarma().getRmse());
				setElitism(getBestChromosome());
			}
		}
		
	}
	
	private void orderPopulation() {
		Collections.sort(population, new Comparator<MetaChromosome>() {
	         public int compare(MetaChromosome o1, MetaChromosome o2) {
	            double cost1 = o1.getFitness();
	            double cost2 = o2.getFitness();
	            return (cost1 > cost2 ? -1 : (cost1 < cost2 ? 1 : 0));
	         }
	      });
	}
	
	private void nextGeneration(List<MetaChromosome> selectedPop) {
		// crossover
		for (int i = 0; i < selectedPop.size()/2; i++) {
			if (Utils.nextDouble() <= Configuration.META_CROSSOVER_RATE) {
				MetaChromosome parent1 = selectedPop.get(2*i);
				MetaChromosome parent2 = selectedPop.get(2*i+1);
				MetaChromosome.crossover(parent1, parent2);
			}
		}
		
		// mutate
		for (MetaChromosome child : selectedPop) {
			if (Utils.nextDouble() <= Configuration.META_MUTATION_RATE) {
				MetaChromosome.mutate(child);
			}
			
			child.getBestarma().setRmse(ts);
			child.setFitness();
		}
		
		population = new ArrayList<MetaChromosome>(selectedPop);
		orderPopulation();
	}
	
	private List<MetaChromosome> selection(int numparents) {
		double totalFitness = 0;
		for (int i = 0; i < population.size(); i++) {
			totalFitness += Utils.round( (1/population.get(i).getFitness()) );
		}
		
		double[] prob = new double[population.size()];
		double cumulativeProb = 0;
		double[] qProb = new double[population.size()];
		if (totalFitness < 0.0001) {
			System.out.println("------------> " + totalFitness);
		}
		for (int i = 0; i < population.size(); i++) {
			//prob[i] = Utils.round( (1/population.get(i).getFitness()) / totalFitness );
			prob[i] = (1/population.get(i).getFitness()) / totalFitness;
			cumulativeProb += prob[i];
			qProb[i] = Utils.round( cumulativeProb );
		}
		qProb[population.size()-1] = 1.0;
		
		List<MetaChromosome> parents = new ArrayList<MetaChromosome>();
		for (int i = 0; i < numparents; i++) {
			double r = random.nextDouble();
			int j = Arrays.binarySearch(qProb, r);
			if (j < 0) {
				j = Math.abs(j + 1);
			}
			parents.add(population.get(j));
			/*for (int j = 0; j < population.size(); j++) {
				if (r < qProb[j]) {
					parents.add(population.get(j));
					break;
				}
			}*/
		}
		return parents;
	}

	public MetaChromosome getElitism() {
		return elitism;
	}

	public void setElitism(MetaChromosome elitism) {
		this.elitism = new MetaChromosome(elitism);
	}
	
	public MetaChromosome getBestChromosome() {
		return population.get(population.size()-1);
	}
}
