package srm679_div2;

public class ForbiddenStreets {

	static int INFINITY = Integer.MAX_VALUE / 2 - 1;
	
	public static int[] find(int N, int[] A, int[] B, int[] time) {
		
		// create graph basic 
		int[][] basic = new int[N][N];
		int streets = A.length;
		for(int i = 0; i < N; i++) {
			for(int j = 0; j < N; j++) {
				basic[i][j] = j == i ? 0 : -1;
			}
		}
		for(int i = 0; i < streets; i++) {
			basic[A[i]][B[i]] = time[i];
		}
		basic = flyodWarshall(basic, N);
		
		int[] sum = new int[N];
		
		for(int i =0; i < N; i++) {
			int startBlock = A[i];
			int endBlock = B[i];
			int tempMemory = basic[startBlock][endBlock];
			// modify graph basic
			basic[startBlock][endBlock] = -1;
			basic[endBlock][startBlock] = -1;
			
			int[][] newBasic = flyodWarshall(basic, N); // create temp allShortestPaths
			sum[i] = compare(basic, newBasic, N); // compare allShortest Paths. add diff to list
		
			// un-modify graph basic
			basic[startBlock][endBlock] = tempMemory;
			basic[endBlock][startBlock] = tempMemory;
		}
		return sum;
	}
	
	// NOTE: we cannot count the steps to this as 
	// the there might be 2 routes that have the same number of 
	// steps 
	
	// use F-W to make allShortestPaths
			// ** for all F-W u need to check if connection exists
	
	private static int[][] flyodWarshall(int[][] basic, int N) {
		int[][] shortest = new int[N][N];
		// TODO : f-w algorithm 
		for(int j = 0; j < N; j++) {
			for(int i = 0; i < N; i++) {
				if (i == j) continue;
				for(int k = 0; k < N; k++) {
					int ik = basic[i][k];
					int ij = basic[i][j];
					int jk = basic[j][k];
					int ijk = ij == -1 || jk == -1? -1: ij + jk;
					if (ik == -1 || ik > ijk) {
						basic[i][k] = ijk;
					}
				}
			}
		}
		return shortest; 
	}
	
	private static int compare(int[][] basic, int[][] temp, int N) {
		int count = 0;
		for(int i = 0; i < N; i++) {
			for(int j =0; j < N; j++) {
				if (i == j || basic[i][j] == -1) continue;
				if(basic[i][j] != temp[i][j]) count++; //Guaranteed to be only longer
			}
		}
		return count;
	}
}
