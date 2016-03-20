#include "Istr.h"
using namespace std;

void Istr::solve2() {
	int two = count("aba",1);
	int a21 = count("abacaba", 0);
	int a14 = count("abacaba", 1);
}

int Istr::count(std::string s, int k) {
	map<char, int> map;
	int len = s.length();
	for (int i = 0; i < len; i++) {
		char c = s.at(i);
		if (map.find(c) == map.end()) {
			map[c] = 1;
		}
		else {
			map[c]++;
		}
	}

	vector<int> vec;
	for (auto iter = map.begin(); iter != map.end(); iter++) {
		vec.push_back(iter->second);
	}
	int lastIndex = vec.size() - 1;
	while (k > 0) {
		sort(vec.begin(), vec.end());
		if (vec[lastIndex] == 0) break;
		vec[lastIndex]--;
		k--;
	}
	int sum = 0;
	for each (int i in vec)
	{
		sum += (int) pow(i, 2);
	}
	return sum;
}