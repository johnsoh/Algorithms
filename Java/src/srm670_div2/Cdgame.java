package srm670_div2;

import java.util.HashSet;

public class Cdgame {
	
	//https://community.topcoder.com/stat?c=problem_statement&pm=14062&rd=16550
	
	public static int rescount(int[] a, int[] b)
	{
		int sumA = 0;
		int sumB = 0;
		HashSet<Integer> products = new HashSet<Integer>();
		
		for (int i : a) sumA += i;
		for (int i : b) sumB += i;
		
		for (int swapA : a) {
			for (int swapB : b) {
				int thisSumA = sumA - swapA + swapB;
				int thisSumB = sumB - swapB + swapA;
				int thisProduct = thisSumA * thisSumB;
				products.add(thisProduct);
			}
		}
		
		return products.size();
	}

}
