package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class _1B_NewLotteryGame {
//_1B_NewLotteryGame_SmallInput
	public static void Parse() {
		
		InputStream is  = _1B_NewLotteryGame.class.getResourceAsStream("/resources/_1B_NewLotteryGame_Large");
		//InputStream is  = _1B_NewLotteryGame.class.getResourceAsStream("/resources/_1B_NewLotteryGame_SmallInput");
		Scanner scanner = new Scanner(is);
		
		int caseNo = 1;
		int totalCases = scanner.nextInt();
		for( ; caseNo <= totalCases; caseNo++) {
			int A = scanner.nextInt();
			int B = scanner.nextInt();
			int K = scanner.nextInt();

			int sum = solveLargeInput(A, B, K);
			
			// for each num 0 - A. count the num of 0s. 
			// now all these 0s doesnt quite matter if its 1 or 0. what is the power set?
			// powerset = 2^(N). i.e. 00 => 2^2 = 4. 
			// num of open bits .i.e. for each a: get all open bits from  prev 1111 in B. then from 1000 to 1010 , calc slowly. 
			
			
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(sum);
		}
		scanner.close();
	}

	private static int solveLargeInput(int A, int B, int K) {
	
		int sum = 0;
		sum += calcOneWay(K, A, B);
		sum += calcOneWay(K, B, A);
		
		return sum;
	}

	private static int calcOneWay(int K, int primaryNumberMax, int secondaryMax) {
		int[] cache = new int[30];
		for (int i = 0; i < cache.length; i++) {
			cache[i] = (int) Math.pow(2, i) - 1;
		}
		
		int sum = 0;
		int otherLimit = 0; 
		for(int testLimit : cache) {
			if(secondaryMax > testLimit) otherLimit = testLimit;
			else break;
		}     
		for (int primaryNum = 0; primaryNum < primaryNumberMax; primaryNum++) {
			int zeroCount = 0;
			int test = primaryNum;
			int pointer = 0;
			int lastSuccessfulTest = 0;
			while(test < otherLimit) {
				if ((test & (1 << pointer)) == 0) {
					zeroCount++;
					lastSuccessfulTest = test;
					test += (1 << pointer);
				}
				pointer++;
			}
			// get freePoints
			sum += (int) Math.pow(2, zeroCount);
			
			for(int secondary = lastSuccessfulTest + 1; secondary < secondaryMax; secondary++) {
				if ((secondary & primaryNum) < K) sum++;
			}
		}
		return sum;
	}
	
	private static int solveSmallInput(int A, int B, int K) {
		int sum = 0;
		for (int a = 0; a < A; a++) {
			for (int b = 0; b < B; b++) {
				if ((a & b) < K) sum++;
			}
		}
		return sum;
	}
}
