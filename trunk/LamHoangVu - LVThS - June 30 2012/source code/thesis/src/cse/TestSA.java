package cse;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class TestSA {

	/**
	 * @param args
	 * @throws IOException 
	 * @throws NumberFormatException 
	 */
	public static void main(String[] args) throws NumberFormatException, IOException {
		// TODO Auto-generated method stub
		long start = System.currentTimeMillis();
		BufferedReader reader = new BufferedReader(new FileReader("airpass.dat"));
		double[] ts = new double[144];
		String str;
		int i = 0;
		while ((str = reader.readLine()) != null) {
			//System.out.println(i + ": " + str);
			ts[i] = Double.valueOf(str);
			i++;
		}
		
		int trainNum = (int) (0.9*ts.length);
		double[] trainSet = new double[trainNum];
		for (i = 0; i < trainNum; i++) {
			trainSet[i] = ts[i];
			System.out.print(ts[i] + ", ");
		}
		System.out.println();
		
		GeneticEngine.ts = trainSet;
		
		SA sa = new SA(10000, 5000, 5, 0.98);
		//sa.runSA(args[1]);
		
		Utils.printFooter(System.currentTimeMillis() - start);
	}

}
