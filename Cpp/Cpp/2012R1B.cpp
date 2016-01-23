#include "2012R1B.h"
#include <vector>
#define For(i,n) for(int i = 0; i < n; i++)
#define While(i) while(i-- > 0)

using namespace std;

void SafetyInNumbers::solve() {

	// check current path 
	/*char path[FILENAME_MAX];
	_getcwd(path, sizeof(path));
	cout << path << endl;*/

	ifstream in("2012R1B.txt");
	cin.rdbuf(in.rdbuf());
	int rounds, players, score;
	cin >> rounds;
	For(round, rounds) {
		cin >> players;
		cout << "apleyrs " << players << endl;
		vector<int> v;
		While (players) {
			cin >> score;
			v.push_back(score );
		}

		//solve
		find(v);
	}
}

void SafetyInNumbers::find(vector<int> v) {
	int indexes = v.size();
	int i, j;
	double audienceWeight = 0;
	For(i, indexes) audienceWeight += v[i];

	For(i, indexes) {
		// do b search
		double high = 1; double low = 0; double curr = 0.5; double prev = 2;
		bool safe = false;
		// order of conditions is important as first condition only gets smaller and 
		// second alternates between true and false fairly often. 
		while (abs(curr - prev) > 0.0000001 || !safe) {
			// is curr safe?
			double audience = curr; 
			double currScore = v[i] + audience*audienceWeight;
			audience = 1 - audience; // the audience left to distribute to the others
			For(j, indexes) {
				if (i == j) continue;
				// if curr contestant has less than the other, it is sure loose. 
				if (currScore < v[j]) {
					safe = false;
					break;
				}
				double diff = currScore - v[j];
				audience -= diff / audienceWeight;
				safe = audience <= 0; // we are safe if there is no more audience to distribute to our adversaries
			}
			
			//if (safe && audience > 0) safe = false;

			// adjust based on safety
			if (safe) high = curr;
			else low = curr;
			prev = curr;
			curr = (high + low) / 2;
		}
		cout << curr << " ";
	}
	cout << endl;
}