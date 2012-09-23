package cse;

import java.io.PrintStream;
import java.text.DecimalFormat;
import java.util.Random;

public class Utils {
	// Random generator
	private static Random random = new Random();
	private static DecimalFormat df = new DecimalFormat("#.000");
	
	public static double nextDouble() {
		return Double.valueOf( df.format( random.nextDouble() ) );
	}
	
	public static double nextDouble(double a, double b) {
		return Double.valueOf( df.format( a + nextDouble()*(b-a) ) );
	}
	
	public static int nextInt(int n) {
		return random.nextInt(n);
	}
	
	public static boolean nextBoolean() {
		return random.nextBoolean();
	}
	
	public static double round(double x) {
		//System.out.println("---------> " + x);
		return Double.valueOf( df.format(x) ).doubleValue();
		//return x;
	}
	
	public static double calcSampleVariance(double[] ts) {
		double mean = calcSampleMean(ts);
		double variance = 0;
    	for (int i = 0; i < ts.length; i++) {
    		variance += Math.pow(ts[i] - mean, 2);
    	}
    	return Utils.round( variance/ts.length ); //= binh phuong do lech chuan
    }

	public static double calcSampleMean(double[] ts) {
		double sum = 0;
    	for (int i = 0; i < ts.length; i++) {
    		sum += ts[i];
    	}
    	return Utils.round( sum/ts.length );
    }
	
	static void printFooter(long runtime) {
        //System.out.println("*************************************************");
        //System.out.println("Program has terminated successfully");
        System.out.println("Runtime: " + formatRuntime(runtime));
        //System.out.println("*************************************************");
    }
    
    static String formatRuntime(long runtime) {
    	String result = "";
    	double seconds = runtime/1000.0;
    	if (seconds > 3600) {
    		int hours = (int) (seconds/3600);
    		result = hours + " h ";
    		seconds = seconds - hours*3600;
    	}
    	if (seconds > 60) {
    		int mins = (int) (seconds/60);
    		result += mins + " m ";
    		seconds = seconds - mins*60;
    	}
    	
    	DecimalFormat nf = new DecimalFormat("0.000");
    	result += nf.format(seconds) + " s"; 
    	return result;
    }

    public static void printFooter(PrintStream ps, long runtime) {
        ps.println("Runtime: " + formatRuntime(runtime));
    }
}
