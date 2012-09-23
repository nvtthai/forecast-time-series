package cse;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class Test {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		List<Double> list = new ArrayList<Double>();
		for (int i = 0; i < 1000; i++) {
			list.add(new Random().nextGaussian());
		}
		
		Collections.sort(list);
		for (int i = 0; i < list.size(); i++) {
			System.out.println(i+1 + " " + list.get(i));
		}
	}

}
