from typing import List, Dict

def get_modified_price(length: int, prices: List[int]) -> float:
    """Допоміжна функція для розрахунку ціни зі знижкою """
    base_price = prices[length - 1]
    if length >= 4:
        return base_price * 0.9
    return float(base_price)

def rod_cutting_memo(length: int, prices: List[int]) -> Dict:
    memo = {}
    choices = {}

    def solve(n):
        if n == 0:
            return 0
        if n in memo:
            return memo[n]

        max_p = -1.0
        best_cut = n

        for i in range(1, n + 1):
            current_p = get_modified_price(i, prices) + solve(n - i)
            if current_p > max_p:
                max_p = current_p
                best_cut = i
        
        memo[n] = max_p
        choices[n] = best_cut
        return max_p

    max_profit = solve(length)
    
    cuts = []
    curr = length
    while curr > 0:
        cuts.append(choices[curr])
        curr -= choices[curr]

    return {
        "max_profit": max_profit,
        "cuts": cuts,
        "number_of_cuts": len(cuts) - 1 if cuts else 0
    }

def rod_cutting_table(length: int, prices: List[int]) -> Dict:
    dp = [0.0] * (length + 1)
    choices = [0] * (length + 1)

    for i in range(1, length + 1):
        max_p = -1.0
        for k in range(1, i + 1):
            current_p = get_modified_price(k, prices) + dp[i - k]
            if current_p > max_p:
                max_p = current_p
                choices[i] = k
        dp[i] = max_p

    cuts = []
    curr = length
    while curr > 0:
        cuts.append(choices[curr])
        curr -= choices[curr]

    return {
        "max_profit": dp[length],
        "cuts": cuts,
        "number_of_cuts": len(cuts) - 1 if cuts else 0
    }

def run_tests():
    test_cases = [
        {"length": 5, "prices": [2, 5, 7, 8, 10], "name": "Базовий випадок"},
        {"length": 3, "prices": [1, 3, 8], "name": "Оптимально не різати"},
        {"length": 4, "prices": [3, 5, 6, 7], "name": "Рівномірні розрізи"}
    ]

    for test in test_cases:
        print(f"\nТест: {test['name']}")
        print(f"Довжина: {test['length']}, Ціни: {test['prices']}")
        
        memo_res = rod_cutting_memo(test['length'], test['prices'])
        print(f"Мемоізація: {memo_res}")
        
        table_res = rod_cutting_table(test['length'], test['prices'])
        print(f"Табуляція:  {table_res}")

if __name__ == "__main__":
    run_tests()