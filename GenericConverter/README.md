# 제네릭 변환기 (GenericConverter)

## 난이도

⭐

## 선행 학습

- 제네릭 클래스 정의 및 사용
- 다중 타입 매개변수
- `Func<T, TResult>` 델리게이트

## 문제

입력 타입을 출력 타입으로 변환하는 제네릭 변환기 클래스를 
만드세요.

### 요구사항

**`Converter<TInput, TOutput>` 클래스**

- 생성자에서 `Func<TInput, TOutput>` 타입의 변환 함수를 받음
- `Convert(TInput input)` 메서드: 입력값을 변환하여 반환
- `ConvertAll(TInput[] inputs)` 메서드:
- 배열 전체를 변환하여 `TOutput[]` 반환

**테스트**

1. 문자열 -> 길이 변환기: `"Hello"`, `"World"`, `"C#"` 변환
2. 정수 -> 문자열 변환기: `1`, `2`, `3` 변환 (숫자에 "번" 붙이기)
3. 실수 -> 정수 변환기: `3.7`, `1.2`, `9.9` 변환 (소수점 버림)

## 예상 실행 결과

```
=== 문자열 → 길이 변환 ===
Hello → 5
전체 변환: 5, 5, 2

=== 정수 → 문자열 변환 ===
1 → 1번
전체 변환: 1번, 2번, 3번

=== 실수 → 정수 변환 ===
3.7 → 3
전체 변환: 3, 1, 9
```

## 힌트

<details>
<summary>힌트 보기</summary>

- 변환 함수를 필드에 저장: 
- `private Func<TInput, TOutput> _convertFunc;`
- `ConvertAll`에서는 결과 배열을 생성하고, 
- 각 요소에 `Convert()`를 호출하여 저장
- 소수점 버림은 `(int)` 캐스팅으로 처리

</details>
