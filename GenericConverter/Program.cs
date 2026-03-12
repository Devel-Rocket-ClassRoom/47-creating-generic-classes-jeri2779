using System;

// README.md를 읽고 아래에 코드를 작성하세요.

Console.WriteLine("=== 문자열 → 길이 변환 ===");
Converter<string, int> converter1 = new Converter<string, int>(str => str.Length);
string[] words = { "Hello", "World", "C#" };
int[] lengths = converter1.ConvertAll(words); 
Console.WriteLine($"{words[0]} → {lengths[0]}"); 
Console.WriteLine("전체 변환: " + string.Join(", ", lengths));
Console.WriteLine();

Console.WriteLine("=== 정수 → 문자열 변환 ===");
int[] nums = { 1, 2, 3 };
string [] numStrings = new Converter<int, string>(num => $"{num}번").ConvertAll(nums);
Console.WriteLine($"{nums[0]} → {numStrings[0]} ");
Console.WriteLine("전체 변환: " + string.Join(", ", numStrings));
Console.WriteLine();

Console.WriteLine("=== 실수 → 정수 변환 ===");
double[] doubles = { 3.7, 1.2, 9.9 };
int[] intValues = new Converter<double, int>(d => (int)d).ConvertAll(doubles);
Console.WriteLine($"{doubles[0]} → {intValues[0]}");
Console.WriteLine("전체 변환: " + string.Join(", ", intValues));

class Converter<TInput, TOutput>
{
    private Func<TInput, TOutput> converting;

    public Converter(Func<TInput, TOutput> converter)
    {
        converting = converter;
    }
    public TOutput Convert(TInput input)
    {
        return converting(input);
    }

    public TOutput[] ConvertAll(TInput[] inputs)
    {
        TOutput[] results = new TOutput[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            results[i] = Convert(inputs[i]);
        }
        return results;
    }
}