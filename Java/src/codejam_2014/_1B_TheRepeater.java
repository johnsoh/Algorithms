package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;

public class _1B_TheRepeater {

	public static void Parse() {
	
		//InputStream is  = _1B_TheRepeater.class.getResourceAsStream("/resources/_1B_TheRepeater_SmallInput");
		InputStream is  = _1B_TheRepeater.class.getResourceAsStream("/resources/_1B_TheRepeater");
		Scanner scanner = new Scanner(is);
		
		int caseNo = 1;
		int totalCases = scanner.nextInt();
		for( ; caseNo <= totalCases; caseNo++) {
			int words = scanner.nextInt();
			List<String> list = new ArrayList<String>();
			for (int i = 0; i < words; i++) list.add(scanner.next());
			
			// solve
			String ans = solveBig(list);
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(ans);
		}
		scanner.close();
	}
	
	public static String solveSmall(List<String> words) {
		String FALSE = "Fegla Won";
		List<Character> word1 = new LinkedList<Character>();
		List<Character> word2 = new LinkedList<Character>();
		for(char c : words.get(0).toCharArray()) word1.add(c);
		for(char c : words.get(1).toCharArray()) word2.add(c);
		
		int sum = 0;
		while(word1.size() > 0 && word2.size() > 0) {
			if (word1.get(0) != word2.get(0)) {
				return FALSE;
			}
			char c = word1.get(0);
			int c1 = shredAndCountC(word1, c);
			int c2 = shredAndCountC(word2, c);
			sum += Math.abs(c1 - c2); //Math.ceil((c1+c2)/2.0);
		}
		if (word1.size() > 0 || word2.size() > 0) return FALSE; // need to check that we have processed both strings fully
		return Integer.toString(sum);
	}

	private static int shredAndCountC(List<Character> word, char c) {
		int count = 0;
		while(word.size() > 0 && word.get(0) == c) {
			word.remove(0); 
			count++;
		}
		return count;
	}  
	
	public static String solveBig(List<String> words) {
		int[][] data = new int[words.size()][];
		int charCount = getDataAndUniqueCount(words, data);
		if (charCount == -1) return "Fegla Won";
		
		// find average
		int wordCount = words.size();
		int sum = 0;
		for(int charPos = 0; charPos < charCount; charPos++) {
			
			int[] medianArray = new int[wordCount];
			for(int wordPos = 0; wordPos < wordCount; wordPos++) {
				medianArray[wordPos] = data[wordPos][charPos];
			}
			
			Arrays.sort(medianArray);
			int median = medianArray[wordCount / 2];
			
			for(int wordPos = 0; wordPos < wordCount; wordPos++) {
				sum += Math.abs(median - data[wordPos][charPos]);
			}
		}
		return Integer.toString(sum);
		
		// 1: Round off rather than truncate
		// average /= wordCount; // ERROR when (3+3+2)/3 = 2.
		// average = (int) Math.round( (double) average / (double) wordCount );
					
		// 2: median minimizes steps to add/minus numbers to the same. e.g. {1,2,8}
		// median=2. (1+6) VS average=3.6=4 (3+2+6)
	}
	
	private static int getDataAndUniqueCount(List<String> words, int[][] data) {
		
		HashSet<String> set = new HashSet<String>();
		int wordCount = words.size();
		int maxWordLen = 120;
		
		for(int i = 0; i < wordCount; i++) {
			String word = words.get(i);
			int[] row = new int[maxWordLen];
			Enhance(word, row, set);
			data[i] = row;
		}
		
		if (set.size() != 1) return -1;
		return set.iterator().next().length();
	}

	private static void Enhance(String word, int[] row, HashSet<String> set) {
		String sum = "";
		int sumPtr = -1;
		char prev = 0;
		for(char c : word.toCharArray()) {
			if (c != prev) {
				sumPtr++;
				row[sumPtr] = 1;
				sum += c;
				prev = c;
			} else {
				row[sumPtr]++;
			}
		}
		set.add(sum);
	}
}












