using Castle.DynamicProxy;
using Newtonsoft.Json;
using NLog;

namespace LINQ2JSLibrary.Loggers
{
    public class DynamicProxyLogger : IInterceptor
    {
        private Logger logger = LogManager.GetLogger("dynamicProxyLogger");

        public void Intercept(IInvocation invocation)
        {
            string className = invocation.InvocationTarget.ToString();
            string methodName = invocation.Method.Name;
            string parameters = string.Empty;

            foreach (var arg in invocation.Arguments)
            {
                string serializedArg = JsonConvert.SerializeObject(arg, Formatting.Indented);
                parameters += serializedArg;
            }

            this.logger.Info($"class {className} called {methodName} with args {parameters}");

            invocation.Proceed();
        }
    }
}
