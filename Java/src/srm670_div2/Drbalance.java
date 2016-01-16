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
		
		// find negativity of all prefixes
		chart[0] = s.charAt(0) == '+' ? 1 : -1;
		if (chart[0] == -1) negativePrefixes++;
		
		for(int i = 1; i < max; i++)  
		{
			int thisPrefix = chart[i-1] + (s.charAt(i) == '+' ? 1 : -1);
			chart[i] = thisPrefix;
			if (thisPrefix < 0) negativePrefixes++;
		}
		
		// find first negative position
		for( ; firstNegativePosition < max && chart[firstNegativePosition] >= 0; firstNegativePosition++) {}
		
		// keep changing first negative positions to positive until we meet criteria
		int rounds = 0;
		while(negativePrefixes > k) {
			
			rounds++;
			boolean hasFoundFirstNegative = false;
			
			for(int i = firstNegativePosition; i < max; i++) 
			{
				if(chart[i] >= 0) continue;
				chart[i] += 2;
				if (chart[i] >= 0)
				{
					negativePrefixes--;
				}
				else if (!hasFoundFirstNegative) 
				{
					hasFoundFirstNegative = true;
					firstNegativePosition = i;
				}
			}
		}
		return rounds;
	}
}
