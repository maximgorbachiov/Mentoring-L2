using System;
using Newtonsoft.Json;
using NLog;
using PostSharp.Aspects;

namespace LINQ2JSLibrary.Loggers
{
    [Serializable]
    public class CodeRewritingLogger : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Logger logger = LogManager.GetLogger("codeRewritingLogger");
            string className = args.Instance.ToString();
            string methodName = args.Method.Name;
            string parameters = string.Empty;

            foreach (var arg in args.Arguments)
            {
                string serializedArg = JsonConvert.SerializeObject(arg, Formatting.Indented);
                parameters += serializedArg;
            }

            logger.Info($"class {className} called {methodName} with args {parameters}");

            args.FlowBehavior = FlowBehavior.Default;
        }
    }
}
