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
		int aMax = a.length(); 
		int bMax = b.length();
		
		// still need to do convolution-like compare
		int overlapLimit = Math.min(aMax, bMax);
		int maxOverlap = 0;
		for(int i = 1; i <= overlapLimit; i++) {
			if (a.substring(aMax - i, aMax).equals(b.substring(0, i))) maxOverlap = i; // note java.String .equals != ==
		}
		
		return a + b.substring(maxOverlap, bMax);
	}
	
	public static void Test() {
		System.out.println("=== SRM 679 Test: FourStrings");
		int ans = shortestLength("abc", "ab", "bc", "b");
		SrmReader.checkStatic(3, ans);
		
		ans = shortestLength("a", "bc", "def", "ghij");
		SrmReader.checkStatic(10, ans);
		 
		ans = shortestLength("top", "coder", "opco", "pcode");
		SrmReader.checkStatic(8, ans);
		
		ans = shortestLength("thereare","arelots","lotsof","ofcases");
		SrmReader.checkStatic(19, ans);
		
		ans = shortestLength("aba", "b", "b", "b");
		SrmReader.checkStatic(3, ans);
		
		ans = shortestLength("x","x","x","x");
		SrmReader.checkStatic(1, ans);
	}

}
