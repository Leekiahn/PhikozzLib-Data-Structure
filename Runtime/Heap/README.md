# Heap

배열 기반 이진 힙입니다. 값 자체를 우선순위로 삼아, 가장 작은(또는 가장 큰) 값부터 꺼냅니다.

`Heap<TElement> where TElement : IComparable<TElement>`

## API

| 멤버 | 설명 | 시간 복잡도 |
| --- | --- | --- |
| `Heap(IComparer<TElement> comparer = null)` | 생성자. `comparer` 생략 시 Min Heap | O(1) |
| `static CreateMinHeap()` | 작은 값부터 나오는 힙 생성 | O(1) |
| `static CreateMaxHeap()` | 큰 값부터 나오는 힙 생성 | O(1) |
| `Push(TElement element)` | 요소 추가 | O(log n) |
| `Pop()` | 최우선 요소를 꺼내서 반환 | O(log n) |
| `Peek()` | 최우선 요소를 꺼내지 않고 반환 | O(1) |
| `Clear()` | 모든 요소 제거 | O(n) |
| `Count` | 현재 요소 개수 | O(1) |

## 예시 (Min Heap)

```csharp
Heap<int> heap = Heap<int>.CreateMinHeap();
heap.Push(30);
heap.Push(10);
heap.Push(50);
heap.Push(5);
heap.Push(20);

heap.Pop();   // 5
heap.Pop();   // 10
heap.Peek();  // 20
heap.Clear(); // Count == 0
```

내부 배열에서 인덱스 `i`의 부모는 `(i - 1) / 2`, 왼쪽 자식은 `2i + 1`, 오른쪽 자식은 `2i + 2`입니다.

### Push: 맨 뒤에 넣고, 부모보다 작으면 위로 교환

```
Push(30)  [30]
          index 0

Push(10)  [30, 10]        → 10 < 부모[0]=30, 교환
          [10, 30]

Push(50)  [10, 30, 50]    → 50 >= 부모[0]=10, 그대로

Push(5)   [10, 30, 50, 5] → 5 < 부모[1]=30, 교환
          [10, 5, 50, 30] → 5 < 부모[0]=10, 교환
          [5, 10, 50, 30]

Push(20)  [5, 10, 50, 30, 20] → 20 >= 부모[1]=10, 그대로
```

결과:

```
index    [0] [1] [2] [3] [4]
value     5   10  50  30  20

             5
           /   \
         10     50
        /  \
      30    20
```

### Pop: 루트를 꺼내고, 맨 뒤 요소를 루트로 올린 뒤 더 작은 자식과 아래로 교환

```
Pop() → 5 반환
          [20, 10, 50, 30]  ← 맨 뒤 20을 [0]으로 이동
          → 자식 [1]=10, [2]=50 중 작은 10과 교환
          [10, 20, 50, 30]
          → 자식 [3]=30 >= 20, 그대로

Pop() → 10 반환
          [30, 20, 50]      ← 맨 뒤 30을 [0]으로 이동
          → 자식 [1]=20, [2]=50 중 작은 20과 교환
          [20, 30, 50]
          → 자식 없음, 종료
```

### Peek / Clear

```
Peek()  → 20 반환, 배열 변화 없음  [20, 30, 50]
Clear() → []                       Count == 0
```
