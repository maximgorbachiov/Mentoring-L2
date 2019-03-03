using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadTasks.Task6
{
    public class ConcurrentCollection
    {
        private List<int> collection = new List<int>();
        private AutoResetEvent addOpSynchObj = new AutoResetEvent(true);
        private AutoResetEvent logOpSynchObject = new AutoResetEvent(false);

        public void AddElement(int element)
        {
            addOpSynchObj.WaitOne();
            Console.WriteLine($"Add operation: element = {element}");
            collection.Add(element);
            logOpSynchObject.Set();
        }

        public void LogElements()
        {
            logOpSynchObject.WaitOne();
            Console.WriteLine("Collection: ");
            for (int i = 0; i < collection.Count; i++)
            {
                Console.Write($"[{i}] = {collection[i]} ");
            }
            Console.WriteLine();
            addOpSynchObj.Set();
        }
    }
}
