package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Scanner;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;



public class _1A_ChargingChaos implements Runnable {

	public static void Parse() {
		
		//
		ExecutorService svc = Executors.newFixedThreadPool(2);
		//
		
		
		
		InputStream is  = _1A_ChargingChaos.class.getResourceAsStream("/resources/_1A_ChargingChaos");
		Scanner scanner = new Scanner(is);
		
		int caseNo = 1;
		int totalCases = scanner.nextInt();
		for( ; caseNo <= totalCases; caseNo++) {
			int N = scanner.nextInt();
			int L = scanner.nextInt();
			scanner.nextLine();
			String[] plugsArr = scanner.nextLine().split(" ");
			String[] devicesArr = scanner.nextLine().split(" ");
			
			//LinkedList<Integer> ans = solveBig(L, plugsArr, devicesArr);
			
			// 
			_1A_ChargingChaos solver = new _1A_ChargingChaos();
			solver.L = L; solver.plugsArr = plugsArr; solver.devicesArr = devicesArr;
			solver.caseNo = caseNo;
			//(new Thread(solver)).start();
			svc.execute(solver);
			/*System.out.print("Case #"+ caseNo + ": ");
			System.out.println(ans == null || ans.isEmpty() ? "NOT POSSIBLE" : ans.get(0) );*/
		}
		scanner.close();
	}

	@Override
	public void run() {
		System.out.println("solve case no " + caseNo);
		// TODO Auto-generated method stub
		LinkedList<Integer> ans = solveBig(L, plugsArr, devicesArr);
		System.out.print("Case #"+ caseNo + ": ");
		System.out.println(ans == null || ans.isEmpty() ? "NOT POSSIBLE" : ans.get(0) );
		
	}
	int L; String[] plugsArr; String[] devicesArr; int caseNo;
	
	private LinkedList<Integer> solveBig(int L, String[] plugsArr, String[] devicesArr) {
		
		ArrayList<Long> plugs = new ArrayList<Long>();
		ArrayList<Long> devices = new ArrayList<Long>();
		
		for(String s : plugsArr) plugs.add(Long.parseLong(s,2));
		for(String s : devicesArr) devices.add(Long.parseLong(s,2));
		long MASK = (1 << L) - 1;
		
		// we can achieve a speed up by looking at the number of bits diff 
		int N = plugs.size();
		HashSet<Integer> set = new HashSet<Integer>();
		
		LinkedList<LinkedList<Integer>> targets = new LinkedList<LinkedList<Integer>>();
		for(int i = 0; i < N; i++) targets.add(new LinkedList<Integer>());
		for(long plug : plugs) {
			int index = 0;
			for(long device : devices) { 
				int bitCount = Long.bitCount(plug ^ device);
				targets.get(index++).add(bitCount);
				set.add(bitCount);
			}
		}
		HashSet<Integer> goodSet = new HashSet<Integer>();
		for(int item : set) {
			for(int i = 0; i < targets.size(); i++) {
				if(!targets.get(i).contains(item)) break;
				if(i == targets.size() - 1) goodSet.add(item);
			}
		} 
		// end speed-up
		
		
		for(int numOfBits : goodSet) {
			LinkedList<Long> flips = new LinkedList<Long>();
			nCk(numOfBits, 0, L, 0, flips);
			//System.out.println("num of bits: " + numOfBits);
			for(long bitCombination : flips) {
				LinkedList<Long> newPlugs = new LinkedList<Long>();
				for(long oldPlug : plugs) {
					newPlugs.add( (oldPlug^bitCombination) & MASK); // XOR: flip selected bits 
				}
				newPlugs.removeAll(devices);
				if (newPlugs.isEmpty()) {
					LinkedList<Integer> ans = new LinkedList<Integer>();
					ans.add(numOfBits);
					return ans;
				}
			}
		}
		
		LinkedList<Integer> ans = new LinkedList<Integer>();
		return ans;
	}
	
	private static void nCk(int ammo, int pos, int n, long res, LinkedList<Long> list) {
		if(ammo == 0) {
			list.add(res);
		} else if (ammo + pos > n) {
			return;
		} else if (ammo > 0 && pos < n) {
			nCk(ammo - 1, pos + 1, n, res + (1 << pos), list);
			nCk(ammo, pos + 1, n, res, list);
		}
	}
	
	private static LinkedList<Integer> solveSmall(int L, String[] plugsArr, String[] devicesArr) {
		
		ArrayList<Integer> plugs = new ArrayList<Integer>();
		ArrayList<Integer> devices = new ArrayList<Integer>();
		
		for(String s : plugsArr) plugs.add(Integer.parseInt(s,2));
		for(String s : devicesArr) devices.add(Integer.parseInt(s,2));
		
		int allPossibilities = (int) Math.pow(2, L);
		int MASK = (1 << L) - 1;
		LinkedList<Integer> ans = new LinkedList<Integer>();
		
		for(int selectedBits = 0; selectedBits < allPossibilities; selectedBits++) {
			LinkedList<Integer> newPlugs = new LinkedList<Integer>();
			for(int oldPlug : plugs) {
				newPlugs.add( (oldPlug^selectedBits) & MASK); // XOR: flip selected bits 
			}
			
			newPlugs.removeAll(devices);
			
			if (newPlugs.isEmpty()) ans.add(Integer.bitCount(selectedBits));
		}
		
		Collections.sort(ans);
		return ans;
	}

	


	
	
}
