using System;
using Castle.DynamicProxy;

namespace AOPDemo.Aspect
{
	public class LoggerAspect : IInterceptor
	{
        void IInterceptor.Intercept(IInvocation invocation)
        {
			// log before.
			//_logger.LogInformation($"before => Argument: {invocation.Arguments[0]}");

			invocation.Proceed();
			// log after.

			//_logger.LogInformation($" after => Result: {invocation.ReturnValue}");
		}
	}
}
