package srm678_div2;

import java.util.LinkedList;
import java.util.List;

public class RevengeOfTheSith {

	public static int move(int[] steps, int T, int D) {
		int n = steps.length - 1;
		int min = Integer.MAX_VALUE;
		
		List<Integer> choices = nCk(n, T);
		for(int choice : choices) {
			int sum = 0; // lets see how much this choice will cost us
			for(int pos = 0; pos < n; pos++) {
				if ((choice >> pos & 1) == 1) {
					int firstPart = steps[pos];
					int secondPart = steps[pos+1]; // guarantee ok (largest pos is n which is 1 less than max index)
					int fullLen = firstPart + secondPart;
					int halfLen = fullLen / 2;
					
					sum += (halfLen < D ? 0 : Math.pow(halfLen - 1, 2));
					halfLen += fullLen % 2 == 0 ? 0 : 1; // give back 1 if odd full Len 
					sum += (halfLen < D ? 0 : Math.pow(halfLen - 1, 2));
				} else {
					// calc against prev/
					// if last, also calc against next 
				}
			}
			min = Math.min(min, sum);
		}
		return min;
	}
	
	public static List<Integer> nCk(int n, int k) {
		List<Integer> list = new LinkedList<Integer>();
		nCkHelper(k, 0, 0, n, list);
		return list;
	}
	
	public static void nCkHelper(int ammo, int pos, int sum, int MAX, List<Integer> LIST) {
		if (ammo == 0 && pos == MAX) {
			// add to list
			LIST.add(sum);
		} else if (ammo > 0 && pos < MAX) {
			// keep summing or skip
			nCkHelper(ammo - 1, pos + 1, sum + (1 >> pos), MAX, LIST);
			nCkHelper(ammo, pos + 1, sum, MAX, LIST);
		} else {
			// the end 
			return;
		}
	}
	
	
	public static void test() {
		int ans = move(new int[] {2,3,5}, 1, 1 );
		ans = move(new int[] {2,3,5}, 2, 1 );
		ans = move(new int[] {1,2,3,4,5,6} ,1, 2  );
		ans = move(new int[] {1,1,1,1,1,1,1}, 3, 3 );
		ans = move(new int[] {1,2,3} , 2, 2);
	}
	
	
}
