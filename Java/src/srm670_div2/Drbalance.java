package srm670_div2;

public class Drbalance {
	
	// https://community.topcoder.com/stat?c=problem_statement&pm=14060&rd=16550
	
	public static int lesscng(String s, int k)
	{
		if (s.length() == 0) return 0;
		
		int max = s.length();
		int firstNegativePosition = 0;
		int negativePrefixes = 0;
		int[] chart = new int[max];
		
		chart[0] = s.charAt(0) == '+' ? 1 : -1;
		if (chart[0] < 0) negativePrefixes++;
		for(int i = 1; i < max; i++) {
			int thisPrefix = chart[i-1] + s.charAt(i) == '+' ? 1 : -1;
			chart[i] = thisPrefix;
			if (thisPrefix < 0) negativePrefixes++;
		}
		
		// possible optimization: create chart without positive numbers
		// might not really help as we will keep generating positive numbers every iteration
		
		for( ; firstNegativePosition < max || chart[firstNegativePosition] > 0; firstNegativePosition++) {}
		
		int rounds = 0;
		while(negativePrefixes > k) {
			
			rounds++;
			boolean hasFoundFirstNegative = false;
			
			for(int i = firstNegativePosition; i < max; i++) {
				chart[i] += 2;
				if (chart[i] > 0) {
					negativePrefixes--;
				} else if (!hasFoundFirstNegative) {
					hasFoundFirstNegative = true;
					firstNegativePosition = i;
				}
			}
			
		}
		
		return rounds;
	}
}
