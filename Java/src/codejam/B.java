package codejam;

import java.io.InputStream;
import java.util.Scanner;

public class B {

	public static void Parse() throws InterruptedException {
		
		InputStream is  = B.class.getResourceAsStream("/resources/B");
		Scanner scanner = new Scanner(is);
		int totalCases = scanner.nextInt();
		
		for(int caseNo = 1; caseNo <= totalCases; caseNo++) {
			String ans = "";
			System.out.print("Case #"+ caseNo + ": ");
			System.out.println(ans);
		}
		scanner.close();
	}
}
