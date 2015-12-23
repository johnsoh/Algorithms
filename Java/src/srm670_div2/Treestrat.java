package srm670_div2;

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
		
		// get the easiest prey
		int min = INFINITE;
		for(int prey : A) {
			for(int hunter : B) {
				int dist = howFarCanPreyMakeHunterWalk(allShortestPaths, prey, hunter);
				min = Math.min(min, dist);
			}
		}
		
		return min;
	}
	
	private static int howFarCanPreyMakeHunterWalk(int[][] allShortestPaths, int prey, int hunter) {
		
		int totalNodes = allShortestPaths[0].length;
		int maxDistance = -1;
		
		for(int i = 0; i < totalNodes; i++) {
			int distFromPrey = allShortestPaths[prey][i];
			int distFromHunter = allShortestPaths[hunter][i];
			int preyAdvantage = distFromHunter - distFromPrey;
			
			if (preyAdvantage < 0) continue; // because prey would not go there 
			maxDistance = Math.max(maxDistance, preyAdvantage);
		}
		
		return maxDistance;
	}
}
