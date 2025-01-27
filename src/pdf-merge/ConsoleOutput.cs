using System;

namespace pdf_merge;

public class ConsoleOutput {

    public static void Success(string message) {
        Console.WriteLine($"SUCCESS: {message}");
    }

    public static void Warning(string message) {
        Console.WriteLine($"WARNING: {message}");
    }

    public static void Error(string message) {
        Console.WriteLine($"ERROR: {message}");
    }

    public static void ProgressBar(int current, int target, string message) {
        throw new NotImplementedException();
    }

    public static void Print(string message) {
        Console.WriteLine(message);
    }
}
