#pragma once

using namespace std; 

class _2013_Qualification_A {
public:
	void parse();
	string gridCheck(char grid[][4], int xStart, int yStart, int xDelta, int yDelta);
	string solve(char grid[][4]);
};
