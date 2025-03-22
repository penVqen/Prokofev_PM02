using NUnit.Framework;

public class Tests
{
    [Test]
    public void TestShortestDistance()
    {
        double[,] graph = {
            { 0, 2.75, 0 },
            { 2.75, 0, 0.63 },
            { 0, 0.63, 0 }
        };

        Floyd floyd = new Floyd(graph);
        double result = floyd.GetShortestDistance(0, 2);

        Assert.AreEqual(3.38, result, 0.01);
    }
}
