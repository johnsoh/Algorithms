package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;

public class _1B_TheRepeater {

	public static void Parse() {
	
		InputStream is  = _1B_TheRepeater.class.getResourceAsStream("/resources/_1B_TheRepeater_SmallInput");
		Scanner scanner = new Scanner(is);
		
		int caseNo = 1;
		int totalCases = scanner.nextInt();
		for( ; caseNo <= totalCases; caseNo++) {
			int words = scanner.nextInt();
			List<String> list = new ArrayList<String>();
			for (int i = 0; i < words; i++) list.add(scanner.next());
			
			// solve
			String ans = solveSmall(list);
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(ans);
		}
		scanner.close();
	}
	
	private static String solveSmall(List<String> words) {
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

	private static String calcMinChanges(List<String> list) {
		int words = list.size();
		List<Character> symbols = getSymbolsList(list);
		int[][] data = new int[words][];
		for (int word = 0; word < words; word++) {
			char[] arr = list.get(word).toCharArray();
			int count = 1;
			int pointer = 0;
			data[word] = new int[symbols.size()];
			for (int j = 1; j < arr.length; j++) {
				if(arr[j-1] != arr[j]) 
				{
					if (arr[j-1] != symbols.get(pointer)) {
						return "Fegla Won";
					}
					data[word][pointer] = count;
					pointer++;
					count = 1;					
				} 
				else if (arr.length - 1 == j) 
				{
					data[word][pointer] = count;
				}
				else
				{
					count++;
				}
			}
		}
		
		int sum = 0;
		for (int position = 0; position < symbols.size(); position++) {
			int averageCalc = 0;
			for (int word = 0; word < words; word++) {
				averageCalc += data[word][position];
			}
			averageCalc = averageCalc / words;
			
			for (int word = 0; word < words; word++) {
				sum += Math.abs(data[word][position] - averageCalc); 
			}
		}
		
		return sum == -1 ? "Fegla Won" : Integer.toString(sum);
	}

	private static List<Character> getSymbolsList(List<String> list) {
		List<Character> symbols = new ArrayList<Character>();
		char[] arr = list.get(0).toCharArray();
		if (arr.length == 1) {
			symbols.add(arr[0]);
			return symbols;
		}
		char prev = Character.MIN_VALUE;// = arr[0];
		for (int j = 0; j < arr.length; j++) {
			if(arr[j] != prev) {
				prev = arr[j];
				symbols.add(prev);
			}
		}
		return symbols;
	}
}
