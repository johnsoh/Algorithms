package srm679_div2;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

public class ListeningSongs {

	public static void run() {
		listen(new int[]{60,60,60}, new int[] {60,60,60},5,2);
	}
	
	public static int listen(int[] durations1, int[] durations2, int minutes, int T) {
		List<Integer> list1 = new LinkedList<Integer>();
		List<Integer> list2 = new LinkedList<Integer>();
		
		for(int i : durations1) list1.add(i);
		for(int i : durations2) list2.add(i);
		
		Collections.sort(list1);
		Collections.sort(list2);
		
		minutes = minutes * 60; // convert to seconds
		
		if (list1.size() < T || list2.size() < T) return -1;
		
		for(int i = 0; i < T; i++) {
			minutes = minutes - list1.remove(0) - list2.remove(0); // take out smallest
		}
		
		if(minutes < 0) return -1;
		
		int count = T*2; // its one for each band
		list1.addAll(list2);
		Collections.sort(list1);
		
		while(list1.size() > 0) {
			minutes -= list1.remove(0);
			if (minutes < 0) {
				return count;
			}
			count++;
		}
		return count;
	}
	
}
