#include <iostream>
#include <string>
#include <fstream>
#include <stdio.h>
#include <direct.h> // http://stackoverflow.com/questions/143174/how-do-i-get-the-directory-that-a-program-is-running-from
#include "2012R1B.h"
#include "Istr.h"
#include "DivFreed2.h"

using namespace std;

int main() {
	//SafetyInNumbers s;
	//s.solve();
	Istr istr;
	istr.solve2();

	DivFreed2 d;
	d.solve();

	getchar(); // pause 
}