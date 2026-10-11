using System;
using static System.Console;
using static System.Math;
using static System.DateTime;


// .NET 6以降で暗黙的に実行されるglobal using
// global using global::System;
// global using global::System.Collections.Generic;
// global using global::System.IO;
// global using global::System.Linq;
// global using global::System.Net.Http;
// global using global::System.Threading;
// global using global::System.Threading.Tasks;

{
    // Hello World
    {
        Console.WriteLine("Hello, C#!");
        int a = 10;
        int b = 20;
        int result = a + b;
        Console.WriteLine($"計算結果: {result}");
    }

    // 文字列
    {
        // 逐次リテラル
        Console.WriteLine(
            @"one
            two
            three"
        );
        Console.WriteLine();

        // 生文字列
        Console.WriteLine("""
            "生文字列"は
            このように書けます。
        """);
        Console.WriteLine();

        // 生文字列で"""を使いたいなら, """"で囲む. $$で{{}}で文字列補完する
        var x = 3;
        Console.WriteLine($$""""
            変数xは{x}と書けば、{{x}}と出力されます.
            生文字列は"""で囲みます。
        """");
        Console.WriteLine();
    }

    // Date
    {
        Console.WriteLine($"計算結果: {Sqrt(3 * 3 + 4 * 4)}"); // 5
        Console.WriteLine($"タイムスタンプ: {Now}");
    }

    // Tuple
    {
        // タプル型のエイリアス
        // using Member = (int no, string name, int age); // C#12以降

        var m1 = (1, "tiny", 5);
        var m2 = (2, "tank", 4);
        Console.WriteLine($"m1: {m1}, m2: {m2}");
    }

    // 型推論
    {
        // 型推論
        var i = 123;
        var str = "TinyTank";
        var a = new[] { 0, 1, 2 };
        var list = new List<int>();

        // 定数
        const float PI = 3.14159f;
        const int CELSIUS = 273;
    }

    // キャスト
    {
        float x = 1f;
        long y = 2;
        int z = 3;
        y = z; // 拡張
        x = z; // 拡張

        int a = 123;
        long b = a; // 拡張
        short d = (short)a; // 縮小
        Console.WriteLine($"a: {a} {typeof(int)}, d: {d} {typeof(short)}");

        int xx = 123456789;
        float yy = xx; // 拡張
        decimal zz = (decimal)yy;

        // 浮動小数点数から整数へのキャストでは有効数字が落ちる場合がある
        WriteLine($"xx: {xx} {typeof(int)}, yy: {yy} {typeof(float)}, zz: {zz} {typeof(decimal)}");
    }

    // 一次元配列
    {
        // 1次元配列
        int[] array1;
        array1 = new int[5];
        WriteLine($"array1: {array1}");
        
        // いずれも同じ
        int[] array2 = new int[] { 2, 4, 6 };
        int[] array3 = { 2, 4, 6 };
        var array4 = new[] { 2, 4, 6 };
        
        array1[0] = 5;
        WriteLine(array2[2] + array1[0]); // 6 + 5 = 11
        WriteLine(array4[2]); // 6
    }

    // 文字列配列
    {
        var week = new[] {
            "日", "月", "火", "水", "木", "金", "土"
        };
        WriteLine(week[2]); // 火

        // ^i　末尾からi-1番目
        WriteLine(week[^3]); // 木

        // i..j i番目からj-1番目
        WriteLine(string.Join(",", week[2..5]));

        // 5番目(金)から末尾の一つ前(金)まで
        WriteLine(string.Join(",", week[5..^1]));

        // 5番目以降(金, 土) ... week[5..^0]と同一
        WriteLine(string.Join(",", week[5..^0]));

        // 末尾から2つ目(0から数えて)以降全て (木,金,土)
        WriteLine(string.Join(",", week[^3..]));

        // 全て
        WriteLine(string.Join(",", week[..]));
    }

    // 多次元配列とジャグ配列
    {
        // 2次元
        int[,] matrix = { {1,2,3}, {4,5,6}, {7,8,9} };
        WriteLine($"matrix: {matrix}");

        // 3次元
        float[,,] tensor = { { {1,2}, {3,4} }, { {5,6}, {7,8} } };
        WriteLine($"tensor: {tensor}");

        // ジャグ配列
        int[][] jagArray = new int[2][];
        jagArray[0] = new int[] { 1, 2 };
        jagArray[1] = new int[] { 1, 2, 3 };
        WriteLine($"jagArray: {jagArray}");
    }


    // Enum
    {
        Random random = new Random();
        Signal[] values = Enum.GetValues<Signal>();
        Signal s = values[random.Next(values.Length)];
        
        switch (s) {
            case Signal.BLUE:
            WriteLine("前進");
                break;
            case Signal.RED:
                WriteLine("停止");
                break;
            case Signal.YELLOW:
                WriteLine("注意");
                break;
        }
    }

    // 使用しない変数/引数を破棄する
    {  
        static (string, int) hex2int(string s)
        {
            return (s, Convert.ToInt32(s,16));
        }
        
        (_, int x) = hex2int("CD");
        WriteLine(x);
    }

    // switchパターンマッチング
    {
        var val = 123;
        string ans = val switch // switch式
        {
            0 => "zero",
            var n => $"{n}", // 0以外のとき
        };

        WriteLine(ans);
    }

    // パターンマッチングでデータを識別する
    {
        object obj = "abeshi";
        WriteLine(
            obj switch
            {
                // int型 かつ 10未満
                int n and < 10 => n,

                // string型かつプロパティLengthが3より大きい
                string { Length: > 3 } => "abeshi",

                // 破棄パターン
                _ => "その他",
            }
        );

        // リストパターン
        List<int> numbers = [1,2,3,10];
        WriteLine(
            numbers switch
            {
                { Count: 0 } => "empty",
                [1, .., 10] x => $"{x[0]}で始まり{x[^1]}で終わる",
                _ => "other"
            }
        );

        // タプルパターン
        (int id, int age) m1 = (1, 48);
        (int id, int age) m2 = (2, 50);
        WriteLine(
            (m1.age, m2.age) switch
            {
                // いずれかが50以上なら"50割引"を返す
                ( >= 50, _) => "50割引",
                (_, >= 50) => "50割引",
                _ => "通常価格",
            }
        );
    }

    // 複数のデータ型をまとめて定義する
    {
        (double bmi, double ideal) BMI(float height, float weight)
        {
            var h = Math.Pow(height/100, 2);
            return (weight/h, h*22);
        }

        (var bmi, var w1) = BMI(164, 76);
        WriteLine($"BMI:{bmi:F2}, {w1:F2}kg");

        (_, var w2) = BMI(164, 76);
        WriteLine($"{w2:F2}kg");

        (int a, int b) tuple1 = (1, 2);
        WriteLine(tuple1);

        var tuple2 = (a:1, b:2); // 名前付きタプル
        WriteLine(tuple2);

        var tuple3 = (3, 4);
        WriteLine($"{tuple3.Item1}, {tuple3.Item2}");

        int x = 3;
        var tuple4 = (x, 4); // 型推論
        WriteLine($"tuple4.x: {tuple4.x}");

        // タプルの比較
        WriteLine($"tuple1 == tuple2 : {tuple1 == tuple2}");
        WriteLine($"tuple3 != tuple4 : {tuple3 != tuple4}");
    }

    {
        // オーバーフロー
        byte b = byte.MaxValue;
        WriteLine($"max byte: {b}");
        b += 10;
        WriteLine($"max byte + 10 -> overflow: {b}");

        sbyte sb = sbyte.MaxValue;
        WriteLine($"max sbyte: {sb}");
        sb += 1;
        WriteLine($"max sbyte + 1 -> overflow: {sb}");
        sb += 1;
        WriteLine($"max sbyte + 2 -> overflow: {sb}");

        // 浮動小数点のオーバーフロー
        // 最大値を超える -> inf
        // 最小値を超える -> zero
        checked
        {
            float f1 = float.MaxValue;
            float f2 = float.MinValue;
            float f3 = 1e-20f;
            WriteLine($"max float(f1): {f1}");
            WriteLine($"min float(f2): {f2}");
            WriteLine($"f3: {f3}");
            WriteLine($"f1 * 2 = {f1 * 2}");
            WriteLine($"f3 / f1 = {f3/f1}");
        }
    }

    // 同期イテレータ・同期ジェネレータ
    {
        var names = "日月火水木金土";
        IEnumerable<string> DaysWeek()
        {
            for (int i = 0; i < names.Length; i++)
            {
                yield return names.Substring(i, 1); // 文字列から1文字返す
                if (i == (names.Length - 1))
                {
                    yield break; // 最後はカンマ不要
                }
                yield return ",";
            }
        }

        foreach (var dw in DaysWeek())
        {
            Write(dw);
        }
        WriteLine();
    }

    // 非同期イテレータ・非同期ジェネレータ
    {
        HttpClient client = new();

        // 非同期でWebAPIを3回実行する
        async IAsyncEnumerable<string> GenerateAdvice()
        {
            for (int i = 0; i < 3; i++)
            {
                // JSON文字列を返すWebAPIを呼び出す
                var response = await client.GetAsync("https://api.adviceslip.com/advice");
                yield return await response.Content.ReadAsStringAsync();
            }
        }

        // WebAPIの取得結果を表示する
        await foreach (var advice in GenerateAdvice())
        {
            WriteLine(advice);
        }
    }
}


enum Signal { BLUE, YELLOW, RED }

// 読み取り専用変数
class BasicReadonly
{
    readonly int val;

    BasicReadonly(int arg)
    {
        this.val = arg;
    }

    void Method()
    {
        // val = 123; // エラー
    }
}


// 動的型つけ変数
class BasicDynamic_sub
{
    // objはどんな型でもコンパイル可能
    public void Sample(dynamic obj)
    {
        obj.Method(); // obj型にMethodがあるかどうかは実行時に検査される
    }
}

class BasicDynamic
{
    public void Method() {}

    static void Main(string[] args)
    {
        var tmp1 = 1; // int型
        dynamic tmp2 = 1; // dynamic型
        var cls1 = new BasicDynamic();
        var cls2 = new BasicDynamic_sub();
        cls2.Sample(cls1);
        // cls2.Sample(cls2); // コンパイル可能だが実行時例外が出る
    }
}





