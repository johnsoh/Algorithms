package srm677_div2;

import java.util.LinkedList;
import java.util.List;

import Main.SrmReader;

public class FourStrings {
	
	public static int shortestLength(String a, String b, String c, String d) {
		
		// remove duplicates & pure substrings
		List<String> list = new LinkedList<String>();
		list.add(a); list.add(b); list.add(c); list.add(d);
		for(int round = 0; round < 4; round++) {
			String candidate = list.remove(0);
			// adding case (versus removing case in PalindromePrime. cannot use iter from back
			boolean putBack = true;
			for(String other : list) {
				if(other.contains(candidate)) {
					putBack = false; 
				}
			}
			if (putBack) list.add(candidate);
		}
		
		int maxRemaining = (int) Math.pow(2, list.size()) - 1;
		List<String> DATA = list;
		int LIMIT = DATA.size();
		return permutateOrderCapture("", -1, maxRemaining, LIMIT, DATA);
	}
	
	public static int permutateOrderCapture(String capture, int currPos, int remaining, int LIMIT, List<String> DATA) {
		if(remaining == 0) {
			return capture.length(); 
		}
		int best = Integer.MAX_VALUE;
		for(int i = 0; i < LIMIT; i++) {
			if ((remaining >> i & 1) == 0) continue;
			String optimumMergeResult = optimumMerge(capture, DATA.get(i));
			int candidate = permutateOrderCapture(optimumMergeResult, i, remaining - (1 << i), LIMIT, DATA);
			best = Math.min(best, candidate);
		}
		return best;
	}
	
	public static String optimumMerge(String a, String b)  { 
		if (a.contains(b)) return a;
		if (b.contains(a)) return b;
		
		String res1 = frontBackStringMerge(a, b);
		String res2 = frontBackStringMerge(b, a);
		return res1.length() > res2.length() ? res2 : res1;
	}

	private static String frontBackStringMerge(String a, String b) {
		String ab = "";
		int aPtr = 0;  
		int aMax = a.length(); 
		int bMax = b.length();
		int bPtr = bMax - 1; 
		
		while(aPtr < aMax && bPtr >= 0 && a.charAt(aPtr++) == b.charAt(bPtr--)) {
			ab += a.charAt(aPtr);
		}
		// need to put it back within bounds
		ab = b.substring(0, 2+bPtr) + ab + a.substring(--aPtr, aMax);
		// 2+bPtr: substring's index is exclusive of last string  
		return ab; 
	}
	
	public static void Test() {
		int ans = shortestLength("abc", "ab", "bc", "b");
		SrmReader.checkStatic(3, ans);
		
		ans = shortestLength("a", "bc", "def", "ghij");
		SrmReader.checkStatic(10, ans);
		 
		ans = shortestLength("top", "coder", "opco", "pcode");
		SrmReader.checkStatic(8, ans);
		
		ans = shortestLength("aba", "b", "b", "b");
		SrmReader.checkStatic(3, ans);
		
		ans = shortestLength("x","x","x","x");
		SrmReader.checkStatic(1, ans);
	}

}
