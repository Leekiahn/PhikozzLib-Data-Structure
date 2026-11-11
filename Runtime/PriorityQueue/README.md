# PriorityQueue

요소와 우선순위를 따로 받는 우선순위 큐입니다. 우선순위가 가장 높은 요소부터 꺼내며, 내부적으로 `Heap`을 사용합니다.

`PriorityQueue<TElement, TPriority>`

## API

| 멤버 | 설명 | 시간 복잡도 |
| --- | --- | --- |
| `PriorityQueue(IComparer<TPriority> priorityComparer = null)` | 생성자. `priorityComparer` 생략 시 Min | O(1) |
| `static CreateMinPriorityQueue()` | 우선순위 값이 작은 것부터 나오는 큐 생성 | O(1) |
| `static CreateMaxPriorityQueue()` | 우선순위 값이 큰 것부터 나오는 큐 생성 | O(1) |
| `Enqueue(TElement element, TPriority priority)` | 요소를 우선순위와 함께 추가 | O(log n) |
| `Dequeue()` | 최우선 요소를 꺼내서 반환 | O(log n) |
| `Peek()` | 최우선 요소를 꺼내지 않고 반환 | O(1) |
| `Clear()` | 모든 요소 제거 | O(n) |
| `Count` | 현재 요소 개수 | O(1) |

## 예시 (Min PriorityQueue)

```csharp
PriorityQueue<string, int> queue = PriorityQueue<string, int>.CreateMinPriorityQueue();
queue.Enqueue("Goblin", 30);
queue.Enqueue("Orc", 10);
queue.Enqueue("Dragon", 50);
queue.Enqueue("Slime", 5);
queue.Enqueue("Bat", 20);

queue.Dequeue(); // "Slime"
queue.Dequeue(); // "Orc"
queue.Peek();    // "Bat"
queue.Clear();   // Count == 0
```

내부 힙에는 `(element, priority)` 쌍이 저장되고, **priority만 비교**합니다. 교환 과정은 [Heap 예시](../Heap/README.md#예시-min-heap)와 같습니다.

### Enqueue

```
Enqueue("Goblin", 30)  [(Goblin,30)]

Enqueue("Orc", 10)     [(Goblin,30), (Orc,10)]
                       → 10 < 부모[0]=30, 교환
                       [(Orc,10), (Goblin,30)]

Enqueue("Dragon", 50)  [(Orc,10), (Goblin,30), (Dragon,50)]
                       → 50 >= 부모[0]=10, 그대로

Enqueue("Slime", 5)    [(Orc,10), (Goblin,30), (Dragon,50), (Slime,5)]
                       → 5 < 부모[1]=30, 교환
                       [(Orc,10), (Slime,5), (Dragon,50), (Goblin,30)]
                       → 5 < 부모[0]=10, 교환
                       [(Slime,5), (Orc,10), (Dragon,50), (Goblin,30)]

Enqueue("Bat", 20)     [(Slime,5), (Orc,10), (Dragon,50), (Goblin,30), (Bat,20)]
                       → 20 >= 부모[1]=10, 그대로
```

결과:

```
index    [0]        [1]       [2]          [3]          [4]
value    (Slime,5)  (Orc,10)  (Dragon,50)  (Goblin,30)  (Bat,20)

                 (Slime,5)
                /         \
         (Orc,10)        (Dragon,50)
         /      \
  (Goblin,30)  (Bat,20)
```

### Dequeue

```
Dequeue() → "Slime" 반환
            [(Bat,20), (Orc,10), (Dragon,50), (Goblin,30)]  ← 맨 뒤 Bat을 [0]으로 이동
            → 자식 [1]=10, [2]=50 중 작은 Orc와 교환
            [(Orc,10), (Bat,20), (Dragon,50), (Goblin,30)]
            → 자식 [3]=30 >= 20, 그대로

Dequeue() → "Orc" 반환
            [(Goblin,30), (Bat,20), (Dragon,50)]            ← 맨 뒤 Goblin을 [0]으로 이동
            → 자식 [1]=20, [2]=50 중 작은 Bat과 교환
            [(Bat,20), (Goblin,30), (Dragon,50)]
            → 자식 없음, 종료
```

### Peek / Clear

```
Peek()  → "Bat" 반환, 배열 변화 없음  [(Bat,20), (Goblin,30), (Dragon,50)]
Clear() → []                          Count == 0
```
