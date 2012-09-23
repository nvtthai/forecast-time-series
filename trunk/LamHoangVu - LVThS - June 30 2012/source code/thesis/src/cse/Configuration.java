package cse;

public class Configuration {
	public static int MAX_AR_ORDER = 13;
	public static int MAX_MA_ORDER = 13;
	
	public static int META_POP_SIZE = 100;
	public static int META_MAX_GENERATION = 500;
	public static int LOW_POP_SIZE = 100;
	public static int LOW_MAX_GENERATION = 5000;
	
	public static double LOW_CROSSOVER_RATE = 0.8; //0.67;
	public static double LOW_MUTATION_RATE = 0.3; //0.33;
	
	public static double META_CROSSOVER_RATE = 0.8;
	public static double META_MUTATION_RATE = 0.2;
}
