package codejam_2014;

import java.io.InputStream;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CountDownLatch;

public class _1B_NewLotteryGame implements Runnable {
//_1B_NewLotteryGame_SmallInput
	public static void Parse() throws InterruptedException {
		
		InputStream is  = _1B_NewLotteryGame.class.getResourceAsStream("/resources/_1B_NewLotteryGame_Large");
		//InputStream is  = _1B_NewLotteryGame.class.getResourceAsStream("/resources/_1B_NewLotteryGame_SmallInput");
		Scanner scanner = new Scanner(is);
		int totalCases = scanner.nextInt();
		
		ConcurrentHashMap<Integer, String> cHm = new ConcurrentHashMap<Integer,String>();
		CountDownLatch countdown = new CountDownLatch(totalCases);
		
		for(int caseNo = 1; caseNo <= totalCases; caseNo++) {
			int A = scanner.nextInt();
			int B = scanner.nextInt();
			int K = scanner.nextInt();

			//String sum = String.valueOf(solveSmallInput(A,B,K));
			//String sum = solveLargeInput(A, B, K).toString(); //10,16,52,2411,14377
			_1B_NewLotteryGame solver = new _1B_NewLotteryGame();
			solver.A = A; solver.B = B; solver.K = K; solver.CaseNum = caseNo;
			solver.cHm = cHm; solver.countdown = countdown;
			Thread thread = new Thread(solver);
			thread.start();
			
			//System.out.print("Case #"+ caseNo + ": ");
			//System.out.println(sum);
		}
		countdown.await();
		for(int caseNo = 1; caseNo <= totalCases; caseNo++) {
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(cHm.get(caseNo));
		}
		
		
		scanner.close();
	}
/* 3: 000, 001, 010
 * 4: 000, 001, 010, 011
 * 2: 000, 001 
 * 
 * trick: if a number a is less than K, all &-ings with b will be less than k. 
 * */
	
	ConcurrentHashMap<Integer, String> cHm ;
	int A; int B; int K; int CaseNum;
	CountDownLatch countdown;
	
	@Override
	public void run() {
		// TODO Auto-generated method stub
		String ans =  solveLargeInput(A,B,K).toString();
		cHm.put(CaseNum, ans);
		countdown.countDown();
		System.out.println(countdown.getCount());
	}
	
	private static BigInteger solveLargeInput(int A, int B, int K) {
	
		//int sum = 0;
		BigInteger sum = BigInteger.ZERO;
		for (int i = 0; i < A; i++) {
			if (i < K) sum = sum.add(BigInteger.valueOf(B));
			else {
				sum = sum.add(BigInteger.valueOf(K));
				int count = 0;
				for(int j = K; j < B; j++) {
					if ((j&i) < K) count++;
				}
				sum = sum.add(BigInteger.valueOf(count));
			}
		}
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
