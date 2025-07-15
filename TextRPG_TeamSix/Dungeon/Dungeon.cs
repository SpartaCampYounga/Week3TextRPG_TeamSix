using System;

class Program
{
    static void Main()
    {
        string text = "  Hello World!  ";
        string result;

        // ToUpper()
        result = text.ToUpper();
        Console.WriteLine("ToUpper: " + result); // "  HELLO WORLD!  "

        // ToLower()
        result = text.ToLower();
        Console.WriteLine("ToLower: " + result); // "  hello world!  "

        // Trim()
        result = text.Trim();
        Console.WriteLine("Trim: [" + result + "]"); // "[Hello World!]"

        // Replace()
        result = text.Replace("World", "C#");
        Console.WriteLine("Replace: " + result); // "  Hello C#!  "

        // Substring()
        result = text.Substring(2, 5); // 시작 인덱스 2부터 5글자
        Console.WriteLine("Substring: " + result); // "Hello"

        // Insert()
        result = text.Insert(6, "C# ");
        Console.WriteLine("Insert: " + result); // "  HellC# o World!  "

        // Remove()
        result = text.Remove(2, 5); // 인덱스 2부터 5글자 삭제
        Console.WriteLine("Remove: " + result); // "  World!  "
    }
}