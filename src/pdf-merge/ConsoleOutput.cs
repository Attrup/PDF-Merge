using System;
using System.Text;

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
        StringBuilder sb = new StringBuilder($"{message} [", 128);
        float status = (float)current / (float)target * 100.0f;

        for (int i = 0; i < 20; i++) {
            if (i * 5 < (int)status) {
                sb.Append("#");
            }
            else {
                sb.Append(" ");
            }
        }

        if (current == target) {
            sb.Append($"] Done!");
            Console.WriteLine(sb.ToString());
        }
        else {
            sb.Append($"] {Convert.ToInt32(status)}%");
            Console.Write(sb.ToString());
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }

    public static void Print(string message) {
        Console.WriteLine(message);
    }
}
