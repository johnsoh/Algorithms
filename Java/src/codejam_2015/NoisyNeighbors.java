package codejam_2015;

import java.util.LinkedList;

/*
 * Given a R by C grid and N items, what is the minimum number of times that when 
 * the N items are arranged in the grid that items are opposite one another.  
 */

public class NoisyNeighbors {

	public static LinkedList<Integer> list;
	public static int[] binary;
	public static int LEN;
	
	public static void solve(int R, int C, int N) {
		
		// get all possibilities
		LEN = R*C;
		list = new LinkedList<Integer>();
		binary = new int[LEN];
		for (int i = 0; i < LEN; i++) binary[i] = (int) Math.pow(2, i);
		
		fillPossibilities(0, 0, N);
		
		// find best outcome
		int min = Integer.MAX_VALUE;
		for(int grid : list) {
			int res = findSadness(R,C,grid);
			min = Math.min(min,  res);
		}
		
		System.out.println(min);
	}
	
	private static void fillPossibilities(int curr, int ptr, int ammo) {
		if (ammo == 0) {
			list.add(curr); return;
		}
		
		if (ptr + ammo <= LEN) {
			fillPossibilities(curr + binary[ptr], ++ptr, ammo-1);
		}
		
		if (ptr + ammo <= LEN) {
			fillPossibilities(curr, ptr, ammo);
		}
	}
	
	// n here is the int representation of the grid
	private static int findSadness(int R, int C, int n) {
		int sadness = 0;
		
		for(int r = 0; r < R; r++) {
			for(int c = 0; c < C; c++) {
				
				// get this position
				int thisPos = r*C + c;
				int thisBit = (n & (1 << thisPos)) >> thisPos;
				
				// check down (unless last row, find (r+1)*C bit)
				if (r < R - 1) {
					int downPos = thisPos + C;
					int downBit = (n & (1 << downPos)) >> downPos;
					if ((thisBit & downBit) == 1) sadness++;
				}
				
				// check right (unless last column, find the (r*C + c) bit )
				if (c < C - 1) {
					int rightPos = thisPos + 1;
					int rightBit = (n & (1 << rightPos)) >> rightPos;
					if ((thisBit & rightBit) == 1) sadness++;
				}
			}
		}
		
		return sadness;
	}
	
}
