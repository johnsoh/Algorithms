package srm671_div2;

/*
 * Given a W x H grid and M squares, what is the biggest rectangle you can form?
 */

public class BearPaints {

	public static long maxArea(int W, int H, long M) {
		long maxSize = 1;
		for(long sideW = 1; sideW <= W; sideW++) {
			long candidateH = Math.min(M / sideW, H);
			long candidateSize = sideW * candidateH;
			maxSize = Math.max(maxSize, candidateSize);
		}
		return maxSize;
	}
	
}
