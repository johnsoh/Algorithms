package codejam_2014;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;
import java.util.Set;
import java.util.TreeSet;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/*
 * Given 2 sets of binary numbers A and B, you can apply onto B one binary number K. 
 * What is the minimum number of bits such that for there there is a 1-to-1 same binary number mapping?
 */

class _1A_ChargingChaos implements Runnable {

	public static void Parse() {
		
		//
		ExecutorService svc = Executors.newFixedThreadPool(2);
		//
		
		
		//InputStream is  = _1A_ChargingChaos.class.getResourceAsStream("/resources/_1A_ChargingChaos");
		InputStream is  = _1A_ChargingChaos.class.getResourceAsStream("/resources/_1A_ChargingChaos_Large");
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
			solver.run();
			//(new Thread(solver)).start();
			//svc.execute(solver);
			/*System.out.print("Case #"+ caseNo + ": ");
			System.out.println(ans == null || ans.isEmpty() ? "NOT POSSIBLE" : ans.get(0) );*/
		}
		
		scanner.close();
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		String ans = solveBig(L, plugsArr, devicesArr);
		System.out.print("Case #"+ caseNo + ": ");
		System.out.println(ans);
	}
	int L; String[] plugsArr; String[] devicesArr; int caseNo;
	
	private String solveBig(int L, String[] plugsArr, String[] devicesArr) {
		
		int N = plugsArr.length;
		ArrayList<Long> plugs = new ArrayList<Long>();
		ArrayList<Long> devices = new ArrayList<Long>();
		Set<Integer> set = new TreeSet<Integer>();
		
		for(String s : plugsArr) plugs.add(Long.parseLong(s,2));
		for(String s : devicesArr) devices.add(Long.parseLong(s,2));
		long MASK = (1 << L) - 1;
		
		
		int[][] data = new int[N+1][]; 
		for(int i = 0; i < N; i++) {
			data[i] = new int[N];
			for(int j =0; j < N; j++) {
				long xorRes = plugs.get(i) ^ devices.get(j);
				int bitCount = Long.bitCount(xorRes);
				data[i][j] = bitCount;
				set.add(bitCount);
			}
		}
		for(int number : set) {
			System.out.println("trying for num = " + number);
			int ans = findPath(number, data);
			if (ans > -1) return Integer.toString(ans);
		}
		
		if (set.size() > 0) return "NOT POSSIBLE";
		
		// doing row n col checks 
		for(int number : set) {
			boolean allRowsHaveValue = true; 
			for(int row = 0; row < N && allRowsHaveValue; row++) {
				boolean rowHasValue = false;
				for(int i = 0; i < N; i++) {
					if (data[row][i] == number) {
						rowHasValue = true;
						break;
					}
				}
				if(!rowHasValue) allRowsHaveValue =false;;
			}
			
			if(!allRowsHaveValue) continue;
			
			boolean allColumnsHaveValue = true; 
			for(int col = 0; col < N && allColumnsHaveValue; col++) {
				boolean colHasValue = false; 
				for (int i = 0; i < N; i++) {
					if(data[i][col] == number) {
						colHasValue = true;
						break;
					}
				}
				if(!colHasValue) allColumnsHaveValue = false;
			}
			
			if (allColumnsHaveValue) {
				// good . return this. 
				return Integer.toString(number);
			}
		}
		
		return "NOT POSSIBLE";
	}
	
	private int findPath(int number, int[][] data) {
		// TODO Auto-generated method stub
		int N = data.length;
		State s = new State();
		s.status = 0; 
		s.level = 0;
		List<State> queue = new LinkedList<State>();
		queue.add(s);
		int endState = (int) Math.pow(2, N) - 1;
		
		while(!queue.isEmpty()) {
			State state = queue.remove(0);
			// note need to check if this state is final here (i.e. at the end and full or at the end and not full ) 
			if (state.level == N) {
				if( state.status != endState) continue; 
				return number; 
			}
			
			for(int ptr = 0; ptr < N; ptr++) {
				int num = data[state.level][ptr];
				if (num != number) continue;
				int marker = 1 << ptr;
				if ((marker & state.status) == marker) continue;
				// now add it to the queue if we can make it this far. 
				State newState = new State();
				newState.level = state.level + 1;
				newState.status = state.status + marker;
				queue.add(newState);
				
				// try
				State s1 = new State() {{
						status = 4;
						level = 2;
				}};
			}
		}
		return -1;
	}
	
	static class State {
		public int status;
		public int level;
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
	
	private static String solveSmall(int L, String[] plugsArr, String[] devicesArr) {
		
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
		return ans == null || ans.isEmpty() ? "NOT POSSIBLE" : Integer.toString(ans.get(0));
	}

	


	
	
}
