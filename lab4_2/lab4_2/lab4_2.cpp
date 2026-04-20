#include <iostream>
#include <vector>
#include <algorithm>
#include "Windows.h"

using namespace std;

void solveGreedy() {
    double M;
    int k;

    cout << "\n--- Завдання II: Максимальна кiлькiсть пасажирiв (ЖА) ---" << endl;
    cout << "Введiть вантажопiдйомнiсть автомобіля M (кг): ";
    cin >> M;
    cout << "Введiть кiлькiсть претендентiв k: ";
    cin >> k;

    vector<double> weights(k);
    cout << "Введiть вагу кожного претендента:" << endl;
    for (int i = 0; i < k; i++) {
        cin >> weights[i];
    }

    sort(weights.begin(), weights.end());

    int count = 0;
    double currentWeight = 0;
    vector<double> selected;

    for (int i = 0; i < k; i++) {
        if (currentWeight + weights[i] <= M) {
            currentWeight += weights[i];
            selected.push_back(weights[i]);
            count++;
        }
        else {
            break;
        }
    }

    cout << "\nРезультат:" << endl;
    cout << "Максимальна кiлькiсть пасажирiв: " << count << endl;
    if (count > 0) {
        cout << "Обрані ваги: ";
        for (double w : selected) cout << w << " ";
        cout << "\nЗагальна вага: " << currentWeight << " кг" << endl;
    }
}

int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    solveGreedy();

    return 0;
}