using Microsoft.Coyote;
using Microsoft.Coyote.SystematicTesting;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace CoyoteTest.LibraryExample.Tests;

[TestClass]
public sealed class TestSimpleKeyValueStore
{

    [TestMethod]
    public void TestConcurrent()
    {
        var config = Configuration.Create()
            .WithTestingIterations(1000)
            .WithVerbosityEnabled();
        var engine = TestingEngine.Create(config, ConcurrentSetAsync);
        engine.Run();
        var report = engine.TestReport;
        Console.WriteLine("Coyote found {0} bug.", report.NumOfFoundBugs);

        var bugsFound = report.NumOfFoundBugs > 0;
        if (bugsFound)
        {
            File.WriteAllText(nameof(TestConcurrent) + ".schedule", engine.ReproducibleTrace);
            Assert.Fail($"Coyote found {report.NumOfFoundBugs} bug(s).");
        }       
    }

    [Microsoft.Coyote.SystematicTesting.Test]
    public async Task ConcurrentSetAsync()
    {
        SimpleKeyValueStore instance = new();
        List<Task> tasks = new();
        foreach(var i in Enumerable.Range(0,10))
        {
            var value = "value";
            if (i == 7)
            {
                value = "7";
            }
            var task = Task.Run(()=> 
            { 
                _ = instance.Set("test", value); 
            });
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);

        Assert.AreNotEqual("7", instance.Get("test"));
    }
}
