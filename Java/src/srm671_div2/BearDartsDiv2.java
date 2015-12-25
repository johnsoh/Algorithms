package srm671_div2;

import java.util.HashMap;

public class BearDartsDiv2 {
	
	public static long count(int[] w) {
		
		HashMap<Integer,Integer> duo = new HashMap<Integer,Integer>();
		duo.put(w[0]*w[1], 1);
		HashMap<Integer,Integer> trio = new HashMap<Integer,Integer>();
		long count = 0;
		
		for(int pointer = 2; pointer < w.length - 1; pointer++) {
			updateCache(pointer, w, duo, trio);
			int sumInQuestion = w[pointer + 1];
			if(trio.containsKey(sumInQuestion)) count += trio.get(sumInQuestion);
		}
		
		return count; 
	}
	
	private static void updateCache(int pointer, int[] w, HashMap<Integer,Integer> duo, HashMap<Integer,Integer> trio) {
		
		int newNumber = w[pointer];
		
		// update trio first as we do not want to use a duo-value which we do not yet have
		for(int i : duo.keySet()) {
			int key = i * newNumber;
			Integer value = trio.get(key);
			if (value == null)
				trio.put(key, duo.get(i));
			else
				trio.put(key, value + duo.get(i));
		}
		
		for(int i = 0; i <  pointer; i++) {
			int key = w[i]*newNumber;
			Integer value = duo.get(key);
			duo.put(key, 1 + (value == null ? 0 : value));
		}
		
		
	}

}
