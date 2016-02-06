package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
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
			List<Character> symbols = getSymbolsList(list);
			int minChanges = calcMinChanges(words, list, symbols);
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(minChanges == -1 ? "Fegla Won" : minChanges);
		}
		scanner.close();
	}

	private static int calcMinChanges(int words, List<String> list, List<Character> symbols) {
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
						return -1;
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
		
		return sum;
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
