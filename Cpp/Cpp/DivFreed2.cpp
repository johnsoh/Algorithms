
#include "DivFreed2.h"
using namespace std;


void DivFreed2::solve() {
	int three = count(2, 2);
	int one = count(9, 1);
	int a15 = count(3, 3);
}

int DivFreed2::count(int n, int k) {
	int sum = 0;
	for (int i = 1; i <= k; i++) {
		sum += count(n - 1, k, i);
	}
	return sum;
}

int DivFreed2::count(int n, int k, int prev) {
	if (n == 0) return 1;
	int sum = 0;
	for (int i = 1; i <= k; i++) {
		if (prev <= i || prev % i != 0) {
			sum += count(n - 1, k, i);
		}
	}
	return sum;
}