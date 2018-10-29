namespace Automation.Simple.Core.Test
{
    using Automation.Simple.Core.Selenium;
    using NUnit.Framework;

    [TestFixture]
    public class DriverManagerTest
    {
        [Test]
        public void Test_DriverManager_GetInstanceShouldReturnDriverManagerInstance()
        {
            var instance = DriverManager.GetInstance();
            Assert.IsNotNull(instance, "The driver manager instance is not the same");
        }

        [Test]
        public void Test_Test_DriverManager_GetInstanceShouldReturnTheSameInstanceTwice()
        {
            var FirstInstance = DriverManager.GetInstance();
            var SecondInstance = DriverManager.GetInstance();
            Assert.AreEqual(SecondInstance, FirstInstance, "The singleton instance reference is the same");
        }
    }
}
