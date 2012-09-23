package cse;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class GeneticEngine {
	// Population of the chromosomes
	private Model model;
	private boolean[] bcoeff;
	private List<ARMAChromosome> population = new ArrayList<ARMAChromosome>();
	private ARMAChromosome elitism;
	
	public static double[] ts;
	
	private static int[] roulette;
	private static int total_fit;

	public GeneticEngine() {
		super();
	}

	public GeneticEngine(Model model, boolean[] bcoeff) {
		super();
		this.model = model;
		this.bcoeff = bcoeff;
	}
	
	public Model getModel() {
		return model;
	}

	public void setModel(Model model) {
		this.model = model;
	}
	
	public List<ARMAChromosome> getPopulation() {
		return population;
	}

	public void setPopulation(List<ARMAChromosome> population) {
		this.population = population;
	}

	public ARMAChromosome getBestChromosome() {
		return population.get(0);
	}
	
	public void evolve() {
		initPop();
		setElitism(getBestChromosome());
		int k = -1;
		for (int i = 0; i < Configuration.LOW_MAX_GENERATION; i++) {
			//List<ARMAChromosome> selectedPop = selection(population.size());
			//nextGeneration(selectedPop);
			//calcRankBasedFitness();
			nextGeneration(i);
			calcRankBasedFitness();
			if (this.elitism.getRmse() > getBestChromosome().getRmse()) {
				setElitism(getBestChromosome());
				k = i;
			}
		}
	}
	
	private void initPop() {
		for (int i = 0; i < Configuration.LOW_POP_SIZE; i++) {
			ARMAChromosome chromosome = new ARMAChromosome(bcoeff, model.getAROrder(), model.getMAOrder());
			chromosome.setRmse(ts);
			population.add(chromosome);
		}
		
		// calculate the finess values for all chromosomes based on the rank-based selection 
		calcRankBasedFitness();
	}
	
	private void nextGeneration(int generation) {
		// parent selection
		int popsize = population.size();
		List<ARMAChromosome> parents = selectParents(popsize);
		
		// crossover
		List<ARMAChromosome> childs = new ArrayList<ARMAChromosome>();
		Map<Integer, Boolean> hash = new HashMap<Integer, Boolean>();
		for (int i = 0; i < parents.size()/2; i++) {
			if (Utils.nextDouble() <= Configuration.LOW_CROSSOVER_RATE) {
				ARMAChromosome parent1 = new ARMAChromosome(parents.get(2*i));
				ARMAChromosome parent2 = new ARMAChromosome(parents.get(2*i+1));
				ARMAChromosome.crossover(parent1, parent2);
				parent1.parent = 2*i + "_" + (2*i+1);
				parent2.parent = 2*i + "_" + (2*i+1);
				
				if (hash.get(parent1.hashCode()) == null) {
					childs.add(parent1);
					hash.put(parent1.hashCode(), true);
				}
				
				if (hash.get(parent2.hashCode()) == null) {
					childs.add(parent2);
					hash.put(parent2.hashCode(), true);
				}
			}
		}
		
		// mutation, and update fitness
		sort(childs);
		//double sampleVariance = Utils.calcSampleVariance(ts);
		for (ARMAChromosome child : childs) {
			if (Utils.nextDouble() <= Configuration.LOW_MUTATION_RATE) {
				ARMAChromosome.mutate(child);
				child.parent += ("_mutate");
			}
			child.setRmse(ts);
		}
		
		// survivor selection
		//sort(population);
		int m = childs.size();
		int k = (int) (0.4*popsize);
		List<ARMAChromosome> newpop = new ArrayList<ARMAChromosome>(childs);
		int index = 0;
		do {
			if (hash.get(population.get(index).hashCode()) == null) {
				newpop.add(population.get(index));
				hash.put(population.get(index).hashCode(), true);
			}
			index++;
		} while (newpop.size() < popsize && index < popsize);
		
		population = new ArrayList<ARMAChromosome>(newpop);
		/*if (population.size() < Configuration.LOW_POP_SIZE) {
			System.out.println("Generation " + generation + "'nth " + population.size());
		}*/
		
		/*System.out.println(generation + ": childs = " + m + " " + population.size());
		sort(population);
		double prev = population.get(0).getRmse();
		for (int i = 1; i < popsize; i++) {
			if (population.get(i-1).hashCode()==population.get(i).hashCode()) {
				System.out.println("HERE");
			}
		}*/
	}
	
	private List<ARMAChromosome> selectParents(int size) {
		List<ARMAChromosome> parents = new ArrayList<ARMAChromosome>();
		Map<String, Boolean> hash = new HashMap<String, Boolean>();
		for (int i = 0; i < size/2; i++) {
			// roulette wheel selection
			int j, k;
			boolean stop = false;
			do {
				j = Utils.nextInt(total_fit + 1);
				k = Utils.nextInt(total_fit + 1);
				while (k==j) {
					k = Utils.nextInt(total_fit + 1);
				}
				if (hash.get(j + "_" + k)==null && hash.get(k + "_" + j)==null) {
					hash.put(j + "_" + k, true);
					hash.put(k + "_" + j, true);
					stop = true;
				}
			} while (!stop);
			
			int index1 = Arrays.binarySearch(roulette, j);
			if (index1 < 0) index1 = Math.abs(index1 + 1);
			
			int index2 = Arrays.binarySearch(roulette, k);
			if (index2 < 0) index2 = Math.abs(index2 + 1);
			
			parents.add(population.get(index1));
			parents.add(population.get(index2));
		}
		return parents;
	}

	private void calcRankBasedFitness() {
		orderPopulation();
		roulette = new int[population.size()];
		total_fit = 0;
		for (int i = 0; i < population.size(); i++) {
			population.get(i).setFitness(population.size()-i);
			total_fit += population.get(i).getFitness();
			roulette[i] = total_fit;
		}
	}

	private void orderPopulation() {
		Collections.sort(population, new Comparator<ARMAChromosome>() {
	         public int compare(ARMAChromosome o1, ARMAChromosome o2) {
	            double cost1 = o1.getRmse();
	            double cost2 = o2.getRmse();
	            return (cost1 > cost2 ? 1 : (cost1 < cost2 ? -1 : 0));
	         }
	      });
	}
	
	public static void sort(List<ARMAChromosome> chromosomes) {
		Collections.sort(chromosomes, new Comparator<ARMAChromosome>() {
	         public int compare(ARMAChromosome o1, ARMAChromosome o2) {
	            double cost1 = o1.getRmse();
	            double cost2 = o2.getRmse();
	            return (cost1 > cost2 ? 1 : (cost1 < cost2 ? -1 : 0));
	         }
	      });
	}

	public ARMAChromosome getElitism() {
		return elitism;
	}

	public void setElitism(ARMAChromosome elitism) {
		this.elitism = new ARMAChromosome(elitism);
	}
	
	/*
	 private void nextGeneration(int generation) {
		// parent selection
		int popsize = population.size();
		List<ARMAChromosome> parents = selectParents(popsize);
		
		// crossover
		List<Integer> removedIndex = new ArrayList<Integer>();
		List<ARMAChromosome> childs = new ArrayList<ARMAChromosome>();
		Map<String, Boolean> hash = new HashMap<String, Boolean>();
		for (int i = 0; i < parents.size()/2; i++) {
			//if (Utils.nextDouble() <= Configuration.LOW_CROSSOVER_RATE) {
				ARMAChromosome parent1 = new ARMAChromosome(parents.get(2*i));
				ARMAChromosome parent2 = new ARMAChromosome(parents.get(2*i+1));
				ARMAChromosome.crossover(parent1, parent2);
				parent1.parent = 2*i + "_" + (2*i+1);
				parent2.parent = 2*i + "_" + (2*i+1);
				
				if (hash.get(Double.toString(parent1.getRmse())) == null) {
					childs.add(parent1);
					hash.put(Double.toString(parent1.getRmse()), true);
				}
				
				if (hash.get(Double.toString(parent2.getRmse())) == null) {
					childs.add(parent2);
					hash.put(Double.toString(parent2.getRmse()), true);
				}
		}
		
		for (int i = removedIndex.size()-1; i > -1; i--) {
			population.remove(removedIndex.get(i).intValue());
		}
		
		// mutation, and update fitness
		sort(childs);
		//double sampleVariance = Utils.calcSampleVariance(ts);
		for (ARMAChromosome child : childs) {
			if (Utils.nextDouble() <= Configuration.LOW_MUTATION_RATE) {
				ARMAChromosome.mutate(child);
				child.parent += ("_mutate");
			}
			child.setRmse(ts);
		}
		
		// survivor selection
		//sort(population);
		List<ARMAChromosome> newpop = new ArrayList<ARMAChromosome>();
		int k = (int) (0.4*popsize);
		if (k + childs.size() < popsize) {
			k = popsize - childs.size();
		}
		
		System.out.println(population.size() + " " + generation + " HERE "
				+ childs.size() + " " + k + " " + Math.min(popsize, k + childs.size()));
		for (int i = k; i < Math.min(popsize, k + childs.size()); i++) {
			childs.get(i-k).mutatation = false;
			if (i >= population.size()) {
				population.add(childs.get(i-k));
			} else {
				population.set(i, childs.get(i-k));
			}
		}
		
		sort(population);
		double prev = population.get(0).getRmse();
		for (int i = 1; i < popsize; i++) {
			if (Double.compare(prev, population.get(i).getRmse()) == 0) {
				//System.out.println("HERE");
			}
		}
	}
	 */
}
