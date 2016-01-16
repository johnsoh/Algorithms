package Main;

import java.io.File;
import java.io.FileNotFoundException;
import java.net.URL;
import java.util.LinkedList;
import java.util.Scanner;

public class SrmReader {

	Scanner scanner;
	private boolean atStart;
	int round;
	
	public SrmReader(String s) {
		// http://stackoverflow.com/questions/573679/open-resource-with-relative-path-in-java
		// See E-rich's response. add resources directory to the build path and then access 
		URL url = Tests.class.getClass().getResource(s);
		File file = new File(url.getPath());
		atStart = true;
		round = 0;
		try {
			scanner = new Scanner(file);
			
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
	}
	
	// retrieve inputs 
	
	public int getInt() { eol(); return scanner.nextInt();}
	public double getDouble() { eol(); return scanner.nextDouble();}
	public long getLong() {eol(); return scanner.nextLong();}
	
	public String getNextString() { 
		eol(); 
		String temp = scanner.next();
		return temp.replaceAll("\"", "");
	}
	
	public String getNextLine() { 
		if(!atStart) 
			scanner.nextLine(); 
		atStart = true;
		return scanner.nextLine(); 
	}
	
	public int[] getIntArray() {
		String[] strArray = getStringArray();
		int[] intArray = new int[strArray.length];
		for (int i = 0; i < strArray.length; i++) {
			intArray[i] = Integer.parseInt(strArray[i]);
		}
		return intArray;
	}
	
	public double[] getDoubleArray() {
		String[] strArray = getStringArray();
		double[] doubleArray = new double[strArray.length];
		for (int i = 0; i < strArray.length; i++) {
			doubleArray[i] = Double.parseDouble(strArray[i]);
		}
		return doubleArray;
	}
	
	public String[] getStringArray() {
		if(!atStart) {
			scanner.nextLine();
		}
		atStart = true; 
		boolean atLastLine = false;
		LinkedList<String> list = new LinkedList<String>(); 
		
		while(!atLastLine) {
			String str = scanner.nextLine();
			if(str.endsWith("}")) atLastLine = true;
			str = str.replace("{","").replace("}", "");
			String[] strArray = str.split(",");
			for(String element : strArray) {
				if(!element.isEmpty()) list.add(element);
			}
		}
		
		return list.toArray(new String[list.size()]);
	}
	
	// answering
	
	public SrmReader answer() {
		if(!atStart) {
			scanner.nextLine();
		}
		atStart = true; 
		scanner.next(); 
		return this;
	}
	
	public void check(Object a, Object b) {
		boolean res = a.equals(b);
		String s = "Case " + round++ + ": " + res;
		s += " (Expected: " + a + " Actual: " + b + ")";
		System.out.println(s);
	}
	
	// misc & helpers
	
	public boolean hasNext() {
		return scanner.hasNext();
	}
	
	private void eol() {
		atStart = false;
	}
	
	public void finalize() throws Throwable {
		try {
			scanner.close();
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			super.finalize();
		}
	}
}
