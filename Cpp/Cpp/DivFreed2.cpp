
#include "DivFreed2.h"
using namespace std;

int dp[11][100001]; // putting this here instead of in the .h works 

void DivFreed2::solve() {
	for (int i = 0; i <= 11; i++) {
		for (int j = 0; j < 100001; j++) {
			dp[i][j] = -1;
		}
	}

	int three = count(2, 2);
	int one = count(9, 1);
	int a15 = count(3, 3);
	int a1515011 = count(2, 1234);
	int test1 = count(3, 3000);
	int test2 = count(6, 3000);
	int test3 = count(6, 5000);
	int test4 = count(6, 8000);
	int test5 = count(8, 8000);
	int a526882214 = count(10, 100000);
}

int DivFreed2::count(int n, int k) {
	//int dp[11][100001];
	// clear dp

	int sum = 0;
	for (int i = 1; i <= k; i++) {
		sum += count(n - 1, k, i);
	}
	return sum;
}

int DivFreed2::count(int n, int k, int prev) {
	if (dp[n][prev] != -1) return dp[n][prev];

	if (n == 0) return 1;
	int sum = 0;
	for (int i = 1; i <= k; i++) {
		if (prev <= i || prev % i != 0) {
			sum += count(n - 1, k, i);
		}
	}
	dp[n][prev] = sum;
	return dp[n][prev];
}