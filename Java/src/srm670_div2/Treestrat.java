package srm670_div2;

import java.util.Arrays;

public class Treestrat {

	public static int roundcnt(int[] tree, int[] A, int[] B)
	{
		int totalNodes = tree.length + 1;
		int[][] allShortestPaths = new int[totalNodes][totalNodes];
		int INFINITE = Integer.MAX_VALUE/4;
		
		// set all to max
		for(int i = 0; i < totalNodes; i++) {
			for(int j = 0; j < totalNodes; j++) {
				allShortestPaths[i][j] = i==j ? 0 : INFINITE;
			}
		}
		
		// create links for neighboring nodes
		for(int i = 0; i < totalNodes-1; i++) {
			int thisPosition = i + 1; //node i's neighbour at tree[i-1]
			int nextPosition = tree[i];
			allShortestPaths[thisPosition][nextPosition] = 1;
			allShortestPaths[nextPosition][thisPosition] = 1;
		}
		
		// Floyd-Warshall Algorithm
		for (int j = 0; j < totalNodes; j++) {
			for (int i = 0; i < totalNodes; i++) {
				for (int k = 0; k < totalNodes; k++) {
					int ik = allShortestPaths[i][k];
					int ijk = allShortestPaths[i][j] + allShortestPaths[j][k];
					if (ijk < ik) allShortestPaths[i][k] = ijk;
				}
			}
		}
		
		// get combined effectiveness of all B: sum of hunters is greater than the parts
		int[] combinedHunter = Arrays.copyOf(allShortestPaths[B[0]], totalNodes);
		for(int hunter : B) {
			int[] hunterProfile = allShortestPaths[hunter];
			for(int i = 0; i < totalNodes; i++) {
				if (hunterProfile[i] < combinedHunter[i]) 
					combinedHunter[i] = hunterProfile[i];
			}
		}
		
		// get the easiest prey
		int min = INFINITE;
		for(int prey : A) {
			int[] preyProfile = allShortestPaths[prey];
			int maxDelayPreyCanCause = 0;
			for(int i = 0; i < totalNodes; i++) {
				int distanceFromPrey = preyProfile[i];
				int distanceFromHunter = combinedHunter[i];
				if(distanceFromPrey < distanceFromHunter && distanceFromHunter > maxDelayPreyCanCause) {
					maxDelayPreyCanCause = distanceFromHunter;
				}
			}
			if(maxDelayPreyCanCause < min) min = maxDelayPreyCanCause;
		}
		
		return min;
	}
}
