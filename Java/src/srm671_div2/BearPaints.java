package srm671_div2;

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
