package srm677_div2;

import java.util.Collections;
import java.util.Iterator;
import java.util.Set;
import java.util.TreeSet;

import Main.SrmReader;

// You are given two ints: L and R. Compute and return the number of 
// palindromic primes between L and R, inclusive

public class PalindromePrime {

	public static Set<Integer> primes = new TreeSet<Integer>();
	
	public static int count(int L, int R) {
		primes.add(2);
		int sum = 0;
		while(L <= R) {
			if (isPrime(L) && isPalindrome(L)) sum++;
			L++;
		}
		return sum;
	}
	
	public static boolean isPalindrome(int candidate) {
		StringBuilder sb = new StringBuilder(candidate);
		return sb.reverse().toString() == sb.toString();
	}
	
	public static boolean isPrime(int candidate) {
		
		// if candidate is less than max, do check and return 
		int currMaxPrime = Collections.max(primes);
		if (candidate <= currMaxPrime) return primes.contains(candidate);
		
		// otherwise, from max to candidate, check if each is a prime.
		while(++currMaxPrime <= candidate) {
			Iterator<Integer> iter = primes.iterator(); // primes has size 1 at least
			while(currMaxPrime % iter.next() != 0 && iter.hasNext());
			if (!iter.hasNext()) primes.add(currMaxPrime);;
		}
		return primes.contains(candidate);
	}
	
	public static void Test() {
		
		int ans = count(100,150);
		SrmReader.checkStatic(2, ans);
		ans = count(1, 9);
		SrmReader.checkStatic(4, ans);
		
		ans = count(929, 929);
		SrmReader.checkStatic(1, ans);
		
		ans = count(1, 1);
		SrmReader.checkStatic(0, ans);
		
		ans = count(1, 1000);
		SrmReader.checkStatic(20, ans);
	}
}
