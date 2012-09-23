package cse;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class DataSet {
	private String fileName;

	public DataSet(String fileName) {
		super();
		this.fileName = fileName;
	}
	
	public double[] loadData(int factor) throws NumberFormatException, IOException {
		BufferedReader reader = new BufferedReader(new FileReader(fileName));
		String str;
		List<Double> dList = new ArrayList<Double>();
		while ((str = reader.readLine()) != null) {
			//System.out.println(i + ": " + str);
			dList.add(Double.valueOf(str));
		}
		
		double[] ts = new double[dList.size()];
		for (int i = 0; i < dList.size(); i++) {
			ts[i] = factor*dList.get(i);
		}
		reader.close();
		return ts;
	}
}
