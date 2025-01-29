using NUnit.Framework;

namespace pdf_merge.Tests;

public class ConsoleOutputTests {
    [Test]
    public void SuccessPrintsMessageToConsole() {
        StringWriter output = Setup();

        string msg = "Test Message";
        ConsoleOutput.Success(msg);

        Restore();

        string outputString = output.ToString();

        Assert.That(outputString.StartsWith("SUCCESS"));
        Assert.That(outputString.Contains(msg));
    }

    [Test]
    public void WarningPrintsMessageToConsole() {
        StringWriter output = Setup();

        string msg = "Test Message";
        ConsoleOutput.Warning(msg);

        Restore();

        string outputString = output.ToString();

        Assert.That(outputString.StartsWith("WARNING"));
        Assert.That(outputString.Contains(msg));
    }

    [Test]
    public void ErrorPrintsMessageToConsole() {
        StringWriter output = Setup();

        string msg = "Test Message";
        ConsoleOutput.Error(msg);

        Restore();

        string outputString = output.ToString();

        Assert.That(outputString.StartsWith("ERROR"));
        Assert.That(outputString.Contains(msg));
    }

    [Test]
    public void PrintPrintsMessageToConsole() {
        StringWriter output = Setup();

        string msg = "Test Message";
        ConsoleOutput.Print(msg);

        Restore();

        string outputString = output.ToString();

        Assert.That(outputString.StartsWith(msg));
    }

    [Test]
    public void ProgressBarPrintsMessageToConsole() {
        StringWriter output = Setup();

        string msg = "Test Message";
        ConsoleOutput.ProgressBar(8, 10, msg);

        Restore();

        string outputString = output.ToString();

        Assert.That(outputString.StartsWith(msg));
        Assert.That(outputString.Contains("80%"));
    }

    /// <summary>
    /// Set up Console to output to StringWriter object and not Std Out
    /// </summary>
    /// <returns>StringWriter object that console has been set to write to</returns>
    private StringWriter Setup() {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        return sw;
    }

    /// <summary>
    /// Restores Console to output to output to Std Out
    /// </summary>
    private void Restore() {
        var stdOut = new StreamWriter(Console.OpenStandardOutput());
        stdOut.AutoFlush = true;
        Console.SetOut(stdOut);
    }
}
