package codejam_2014;

import java.io.InputStream;
import java.math.BigInteger;
import java.util.Scanner;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CountDownLatch;

public class _1B_NewLotteryGame implements Runnable {

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

			_1B_NewLotteryGame solver = new _1B_NewLotteryGame();
			solver.A = A; solver.B = B; solver.K = K; solver.CaseNum = caseNo;
			solver.cHm = cHm; solver.countdown = countdown;
			Thread thread = new Thread(solver);
			thread.start();
		}
		countdown.await();
		for(int caseNo = 1; caseNo <= totalCases; caseNo++) {
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(cHm.get(caseNo));
		}

		scanner.close();
	}
	
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
	
		BigInteger sum = BigInteger.ZERO;
		for (int i = 0; i < A; i++) {
			if (i < K) { 
				sum = sum.add(BigInteger.valueOf(B));
			}
			else 
			{
				sum = sum.add(BigInteger.valueOf(Math.min(B, K))); 
				// need to do min here as B might be less than K. 
				// this case would also mean that the loop below will not break before it is run
				
				int count = 0;
				for(int j = K; j < B; j++) {
					if ((j&i) < K) count++;
				}
				sum = sum.add(BigInteger.valueOf(count));
			}
		}
		return sum;
	}

	private static String solveSmallInput(int A, int B, int K) {
		int sum = 0;
		for (int a = 0; a < A; a++) {
			for (int b = 0; b < B; b++) {
				if ((a & b) < K) sum++;
			}
		}
		return Integer.toString(sum);
	}




}
