from typing import List, Dict
from dataclasses import dataclass

@dataclass
class PrintJob:
    id: str
    volume: float
    priority: int
    print_time: int

@dataclass
class PrinterConstraints:
    max_volume: float
    max_items: int

def optimize_printing(print_jobs: List[Dict], constraints: Dict) -> Dict:
    jobs = [PrintJob(**job) for job in print_jobs]
    limit = PrinterConstraints(**constraints)

    jobs.sort(key=lambda x: (x.priority, x.volume))

    batches = []
    current_batch = []
    current_volume = 0.0
    
    for job in jobs:
        if (len(current_batch) < limit.max_items and 
            current_volume + job.volume <= limit.max_volume):
            current_batch.append(job)
            current_volume += job.volume
        else:
            if current_batch:
                batches.append(current_batch)
            current_batch = [job]
            current_volume = job.volume
            
    if current_batch:
        batches.append(current_batch)

    print_order = []
    total_time = 0
    
    for batch in batches:
        batch_ids = [job.id for job in batch]
        print_order.extend(batch_ids)
        total_time += max(job.print_time for job in batch)

    return {
        "print_order": print_order,
        "total_time": total_time
    }

# Тестування
def test_printing_optimization():
    constraints = {
        "max_volume": 300,
        "max_items": 2
    }

    # Тест 1: Моделі однакового пріоритету
    test1_jobs = [
        {"id": "M1", "volume": 100, "priority": 1, "print_time": 120},
        {"id": "M2", "volume": 150, "priority": 1, "print_time": 90},
        {"id": "M3", "volume": 120, "priority": 1, "print_time": 150}
    ]

    # Тест 2: Моделі різних пріоритетів
    test2_jobs = [
        {"id": "M1", "volume": 100, "priority": 2, "print_time": 120},
        {"id": "M2", "volume": 150, "priority": 1, "print_time": 90},
        {"id": "M3", "volume": 120, "priority": 3, "print_time": 150}
    ]

    # Тест 3: Перевищення обмежень об'єму (кожна модель в окрему партію, якщо велика)
    test3_jobs = [
        {"id": "M1", "volume": 250, "priority": 1, "print_time": 180},
        {"id": "M2", "volume": 200, "priority": 1, "print_time": 150},
        {"id": "M3", "volume": 180, "priority": 2, "print_time": 120}
    ]

    for i, test_jobs in enumerate([test1_jobs, test2_jobs, test3_jobs], 1):
        print(f"\nТест {i}:")
        result = optimize_printing(test_jobs, constraints)
        print(f"Порядок друку: {result['print_order']}")
        print(f"Загальний час: {result['total_time']} хвилин")

if __name__ == "__main__":
    test_printing_optimization()