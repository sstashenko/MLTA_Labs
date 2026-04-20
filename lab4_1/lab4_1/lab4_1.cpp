#include <iostream>
#include <Windows.h>
#include <vector>

using namespace std;

long long countPaths(int x, int y) {
    vector<vector<long long>> dp(x + 1, vector<long long>(y + 1, 0));

    for (int i = 0; i <= x; i++) {
        for (int j = 0; j <= y; j++) {
            if (i == 0 || j == 0) {
                dp[i][j] = 1;
            }
            else {
                dp[i][j] = dp[i - 1][j] + dp[i][j - 1];
            }
        }
    }

    return dp[x][y];
}

int main() {
    SetConsoleCP(1251); 
    SetConsoleOutputCP(1251);
    int x, y;
    cout << "Введіть координати точки B (x y): ";
    cin >> x >> y;

    if (x < 0 || y < 0) {
        cout << "Координати мають бути додатними!" << endl;
        return 1;
    }

    cout << "Кількість можливих шляхів до точки (" << x << ", " << y << "): ";
    cout << countPaths(x, y) << endl;

    return 0;
}