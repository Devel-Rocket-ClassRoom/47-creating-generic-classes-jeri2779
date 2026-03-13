# 제네릭 오브젝트 풀 (GenericPool)

## 난이도

⭐⭐⭐

## 선행 학습

- 제네릭 클래스 정의 및 사용
- 복합 제약 조건 (`where T : class, IInterface, new()`)
- 인터페이스 정의 및 구현
- `List<T>` 사용

## 문제

게임에서 자주 사용되는 오브젝트 풀 패턴을 제네릭으로 구현하세요.

### 요구사항

**`IPoolable` 인터페이스**

- `void Reset()` 메서드: 오브젝트를 초기 상태로 되돌림

**`ObjectPool<T>` 클래스** (`where T : class, IPoolable, new()`)

- 생성자에서 최대 풀 크기(maxSize)를 지정받음
- 내부에 `List<T>` 2개 사용:
  - `_available`: 비활성(사용 가능) 오브젝트 목록
  - `_active`: 활성(사용 중) 오브젝트 목록
- `Get()`: 비활성 목록에서 오브젝트를 꺼내 활성 목록으로 이동. 비활성 목록이 비어있으면 `new T()`로 생성. 
- 총 오브젝트 수(활성 + 비활성)가 최대 풀 크기 이상이면 `null` 반환
- `Return(T item)`: 오브젝트의 `Reset()`을 호출하고 활성 목록에서 비활성 목록으로 이동
- `ActiveCount` 속성: 활성 오브젝트 수 (읽기 전용)
- `AvailableCount` 속성: 비활성 오브젝트 수 (읽기 전용)

**`Bullet` 클래스** (`IPoolable` 구현)

- `bool` 타입의 `IsActive` 속성
- `int` 타입의 `X`, `Y` 속성
- `Fire(int x, int y)` 메서드: 좌표를 설정하고 `IsActive`를 `true`로 변경, 발사 정보 출력
- `Reset()` 메서드: `IsActive`를 `false`로, `X`, `Y`를 `0`으로 초기화

**테스트**

1. 최대 크기 3인 `ObjectPool<Bullet>` 생성
2. 총알 3개를 Get하여 Fire
3. 4번째 Get 시도 (null 반환 확인)
4. 총알 1개 Return 후 다시 Get하여 Fire

## 예상 실행 결과

```
=== 총알 발사 ===
총알 발사! 위치: (10, 20)
총알 발사! 위치: (30, 40)
총알 발사! 위치: (50, 60)
활성: 3, 비활성: 0

=== 풀 초과 시도 ===
풀이 가득 찼습니다!

=== 반납 후 재사용 ===
총알 반납됨
활성: 2, 비활성: 1
총알 발사! 위치: (100, 200)
활성: 3, 비활성: 0
```

## 힌트

<details>
<summary>힌트 보기</summary>

- `Get()`에서 비활성 목록이 비어있고 총 수가 최대 크기 미만이면 `new T()`로 생성
- `Return(T item)` 시 `_active.Remove(item)` 후 `_available.Add(item)`
- 복합 제약 조건: `class`는 참조 타입 제한, `IPoolable`은 `Reset()` 보장, `new()`는 기본 생성자 보장

</details>
