import networkx as nx

def solve_max_flow():
    edges = [
        ("Terminal 1", "Warehouse 1", 25), ("Terminal 1", "Warehouse 2", 20),
        ("Terminal 1", "Warehouse 3", 15), ("Terminal 2", "Warehouse 3", 15),
        ("Terminal 2", "Warehouse 4", 30), ("Terminal 2", "Warehouse 2", 10),
        ("Warehouse 1", "Shop 1", 15), ("Warehouse 1", "Shop 2", 10), ("Warehouse 1", "Shop 3", 20),
        ("Warehouse 2", "Shop 4", 15), ("Warehouse 2", "Shop 5", 10), ("Warehouse 2", "Shop 6", 25),
        ("Warehouse 3", "Shop 7", 20), ("Warehouse 3", "Shop 8", 15), ("Warehouse 3", "Shop 9", 10),
        ("Warehouse 4", "Shop 10", 20), ("Warehouse 4", "Shop 11", 10), ("Warehouse 4", "Shop 12", 15),
        ("Warehouse 4", "Shop 13", 5), ("Warehouse 4", "Shop 14", 10)
    ]

    def get_flow(t1_active=True):
        G = nx.DiGraph()
        for u, v, cap in edges:
            if not t1_active and u == "Terminal 1":
                G.add_edge(u, v, capacity=0)
            else:
                G.add_edge(u, v, capacity=cap)
        
        G.add_edge("Source", "Terminal 1", capacity=float('inf'))
        G.add_edge("Source", "Terminal 2", capacity=float('inf'))
        for i in range(1, 15):
            G.add_edge(f"Shop {i}", "Sink", capacity=float('inf'))
            
        return nx.maximum_flow_value(G, "Source", "Sink", flow_func=nx.algorithms.flow.edmonds_karp)

    base_flow = get_flow(t1_active=True)
    t2_only_flow = get_flow(t1_active=False)
    
    percentage = (t2_only_flow / base_flow) * 100 if base_flow > 0 else 0
    
    print(f"Базовий максимальний потік: {base_flow}")
    print(f"Потік лише через Термінал 2: {t2_only_flow}")
    print(f"Термінал 2 забезпечує {percentage:.2f}% від загального потоку")

solve_max_flow()