using System;
namespace AConsoleTest
{
    public interface ITestClass
    {
        bool Test();
    }
    public abstract class BaseTestClass : ITestClass
    {
        public virtual bool Test() => false;
    }
}
