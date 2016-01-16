#include <iostream>
#include <string>
#include <fstream>
#include <stdio.h>
#include <direct.h>
#include "2012R1B.h"

using namespace std;

int main() {
	cout << "hello" <<endl ;

	// check current path 
	char path[FILENAME_MAX];
	_getcwd(path, sizeof(path));

	// redirect cin 
	ifstream in("../2012R1B.txt");
	cin.rdbuf(in.rdbuf());
	int round;
	cin >> round;
	cout << endl << round << endl;

	getchar(); // pause 
}

/*
New Issues
-> Creating new project
--> Need to create empty project THEN add existing files from source folder
--> Create project from exisiting files DOES NOT work 
-> TODO: figure maintain file structure in cpp projects 
*/