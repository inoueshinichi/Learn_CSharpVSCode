using System;


{
   Console.WriteLine("Hello, C#!");

    int a = 10;
    int b = 20;

    int result = a + b;

    Console.WriteLine($"計算結果: {result}"); 
}

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



