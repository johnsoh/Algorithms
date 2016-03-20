#include <iostream>
#include <string>
#include <fstream>
#include <stdio.h>
#include <direct.h> // http://stackoverflow.com/questions/143174/how-do-i-get-the-directory-that-a-program-is-running-from
#include "2012R1B.h"
#include "_2013_Qualification_A.h"

using namespace std;

int main() {
	//SafetyInNumbers s;
	//s.solve();

	_2013_Qualification_A s;
	s.parse();

	getchar(); // pause 
}