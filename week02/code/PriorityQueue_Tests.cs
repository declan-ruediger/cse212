using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario:  Add VIP 1 (Priority 5), Tim (Priority 4), and Bob (Priority 0), then dequeue all of them. 
    // Expected Result: VIP 1, Tim, Bob
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Tim", 4);
        priorityQueue.Enqueue("Bob", 0);
        priorityQueue.Enqueue("VIP 1", 5);

        string[] expectedResults = ["VIP 1", "Tim", "Bob"];

        foreach (string result in expectedResults)
        {
            Assert.AreEqual(priorityQueue.Dequeue(), result);
        }
    }

    [TestMethod]
    // Scenario: Queue VIPs 1, 2 and 3 (Priority 5), as well as Tim and Tracy (Priority 4), and Sam (Priority 1). Put them in an order that breaks up nodes with the same priority. Dequeue all, but after three dequeues add Lil' B (Priority 10)
    // Expected Result: VIP 3, VIP 1, VIP 2, Lil' B, Tracy, Tim, Sam
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("VIP 3", 5);
        priorityQueue.Enqueue("Tracy", 4);
        priorityQueue.Enqueue("VIP 1", 5);
        priorityQueue.Enqueue("Sam", 1);
        priorityQueue.Enqueue("VIP 2", 5);
        priorityQueue.Enqueue("Tim", 4);

        string[] expectedResults = ["VIP 3", "VIP 1", "VIP 2", "Lil' B", "Tracy", "Tim", "Sam"];
        
        for (int i = 0; i < expectedResults.Length; i++)
        {
            Assert.AreEqual(priorityQueue.Dequeue(), expectedResults[i]);

            if (i == 2)
            {
                priorityQueue.Enqueue("Lil' B", 10);
            }
        }
    }

    // Add more test cases as needed below.
}