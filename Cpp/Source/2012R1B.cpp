#include "2012R1B.h"
#include <stdio.h>
#include <direct.h>

using namespace std;

void SafetyInNumbers::solve() {
	cout << "hellow problem A" << endl;

	char path[FILENAME_MAX];
	_getcwd(path, sizeof(path));
	
	ifstream in("../2012R1B.txt");
	cin.rdbuf(in.rdbuf());
	int round;
	cin >> round;
	cout << endl << round << endl;
}