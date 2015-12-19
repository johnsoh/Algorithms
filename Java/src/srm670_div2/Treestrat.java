package srm670_div2;

import java.util.Arrays;

public class Treestrat {

	public static int roundcnt(int[] tree, int[] A, int[] B)
	{
		int totalNodes = tree.length + 1;
		int[][] allShortestPaths = new int[totalNodes][totalNodes];
		int INFINITE = Integer.MAX_VALUE;
		
		// set all to max
		for(int i = 0; i < totalNodes; i++) {
			Arrays.fill(allShortestPaths[0], INFINITE);
			allShortestPaths[i][i] = 0;
		}
		
		// create links for neighboring nodes
		for(int i = 1; i < totalNodes; i++) {
			int thisPosition = i + 1;
			int nextPosition = tree[i];
			allShortestPaths[thisPosition][nextPosition] = 1;
			allShortestPaths[nextPosition][thisPosition] = 1;
		}
		
		// Floyd-Warshall Algorithm
		for (int i = 0; i < totalNodes; i++) {
			for (int j = 0; j < totalNodes; j++) {
				for (int k = 0; k < totalNodes; k++) {
					int ik = allShortestPaths[i][k];
					int ijk = allShortestPaths[i][j] + allShortestPaths[j][k];
					if (ijk < ik) allShortestPaths[i][k] = ijk;
				}
			}
		}
		
		// get the easiest prey
		int min = INFINITE;
		for(int prey : A) {
			for(int hunter : B) {
				int dist = howFarCanPreyMakeHunterWalk(allShortestPaths, prey, hunter);
				if (dist < min) min = dist;
			}
		}
		
		return min;
	}
	
	// returns the maximum distance prey can make hunter walk
	private static int howFarCanPreyMakeHunterWalk(int[][] allShortestPaths, int prey, int hunter) {
		
		int totalNodes = allShortestPaths[0].length;
		int maxDistance = -1;
		
		for(int i = 0; i < totalNodes; i++) {
			int distFromPrey = allShortestPaths[prey][i];
			int distFromHunter = allShortestPaths[hunter][i];
			int preyAdvantage = distFromHunter - distFromPrey;
			
			if (preyAdvantage < 0) continue; // because prey would not go there 
			if (preyAdvantage > maxDistance) maxDistance = preyAdvantage; // if this the max prey can make hunter walk so far
		}
		
		return maxDistance;
	}
}
