using System;
using System.Text;

namespace pdf_merge;

public class ConsoleOutput {
    /// <summary>
    /// Prints a "Success" message to the CLI, i.e. the method argument `message` prepended with "SUCCESS"
    /// </summary>
    /// <param name="message">Message to print to console</param>
    public static void Success(string message) {
        Console.WriteLine($"SUCCESS: {message}");
    }

    /// <summary>
    /// Prints a "Warning" message to the CLI, i.e. the method argument `message` prepended with "WARNING"
    /// </summary>
    /// <param name="message">Message to print to console</param>
    public static void Warning(string message) {
        Console.WriteLine($"WARNING: {message}");
    }

    /// <summary>
    /// Prints a "Error" message to the CLI, i.e. the method argument `message` prepended with "ERROR"
    /// </summary>
    /// <param name="message">Message to print to console</param>
    public static void Error(string message) {
        Console.WriteLine($"ERROR: {message}");
    }

    /// <summary>
    /// Prints a Progress bar to the CLI as well as a message to explain the progress bar
    /// </summary>
    /// <param name="current">Current progress</param>
    /// <param name="target">Target progress / final value</param>
    /// <param name="message">Message to print along with progress bar</param>
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

    /// <summary>
    /// Prints a message to console. Equal to calling Console.WriteLine
    /// </summary>
    /// <param name="message">Message to print to console</param>
    public static void Print(string message) {
        Console.WriteLine(message);
    }
}
