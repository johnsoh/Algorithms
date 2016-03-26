#include <string>
#include <iostream>
#include <fstream>
#include <vector>
#include "_2013_Qualification_A.h"
using namespace std;

void _2013_Qualification_A::parse() {
	string file = "_2013_Qualification_A.txt";
	ifstream in(file);
	int totalCases;
	in >> totalCases;
	for (int caseNum = 1; caseNum <= totalCases; caseNum++) {
		string line; 
		char grid[4][4];
		for (int i = 0; i < 4; i++) {
			in >> line;
			//in >> line;
			for(int j = 0; j < 4; j++) {
				grid[i][j] = line[j];
			}
		}
		string res = solve(grid);
		cout << "Case #" << caseNum << ": " << res << endl;

		//in >> line; //take out empty space
	}
}

/*
  (x-value)
0 1 2 3 4
1
2
3
4

*/

string _2013_Qualification_A::solve(char grid[][4]) {
	for (int i = 0; i < 4; i++) {
		// verticle and horizontal
		string a = gridCheck(grid, i, 0, 0, 1);
		string b = gridCheck(grid, 0, i, 1, 0);
		if (a != "0") 
			return a + " won";
		if (b != "0") 
			return b + " won";

		//gridCheck(grid, i, 0, 1, 0);

		// left and top moving top-left to bottom-right
		string c = gridCheck(grid, 0, i, 1, 1);
		string d = gridCheck(grid, i, 0, 1, 1);
		if (c != "0") 
			return c + " won";
		if (d != "0") 
			return d + " won";

		// top and right, moving top-right to bottom-left
		string e = gridCheck(grid, 3, i, -1, 1);
		string f = gridCheck(grid, 0, i, -1, 1);
		if (e != "0") 
			return e + " won";
		if (f != "0") 
			return f + " won";
	}
	// no winnders. check for draw or end game. 
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			if ('.' == grid[i][j]) return "Game has not completed";
		}
	}
	
	return "Draw";


}

string _2013_Qualification_A::gridCheck(char grid[][4], int xStart, int yStart, int xDelta, int yDelta) {
	int xTemp = xStart + xDelta;
	int yTemp = yStart + yDelta;
	int xMax = 4, yMax = 4;
	
	int oCount = 0;
	int xCount = 0;

	while (xTemp >= 0 && xTemp < xMax && yTemp >= 0 && yTemp < yMax) {
		char first = grid[yStart][xStart]; 
		char second = grid[yTemp][xTemp];
		// do processing here
		if (oCount == 0) {
			oCount = ((first == 'O' || first == 'T' )&& (second == 'O' || second == 'T')) ? 2 : 0;
		}
		else if (oCount != 0 && (second == 'O' || second == 'T')) {
			 oCount++;
		}
		else {
			oCount = 0;
		}

		if (xCount == 0) {
			xCount = ((first == 'X' || first == 'T') && (second == 'X' || second == 'T')) ? 2 : 0;
		}
		else if (xCount != 0 && (second == 'X' || second == 'T')) {
			xCount++;
		}
		else {
			xCount = 0;
		}

		if (xCount == 4) return "X";
		if (oCount == 4) return "O";
		// end processing
		yStart = yTemp;
		xStart = xTemp;
		yTemp = yStart + yDelta;
		xTemp = xStart + xDelta;
	}
	return "0";
}