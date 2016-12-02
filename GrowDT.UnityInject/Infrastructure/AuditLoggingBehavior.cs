using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GrowDT.Auditing;
using GrowDT.Logging;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace GrowDT.UnityInject.Infrastructure
{
    internal class AuditLoggingBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get { return true; }
        }
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (ShouldNotInterception(input))
            {
                return getNext()(input, getNext);
            }

            PreInvoke(input);

            var methodReturn = getNext()(input, getNext);
            OnException(input, methodReturn);
            PostInvoke(input);

            return methodReturn;
        }

        private bool ShouldNotInterception(IMethodInvocation input)
        {
            var disableLoggingAttr = typeof(DisableAuditingAttribute);

            return input.Target.GetType().IsDefined(disableLoggingAttr, false)
                   || input.Target.GetType().GetMethod(input.MethodBase.Name).IsDefined(disableLoggingAttr);
        }

        private void PreInvoke(IMethodInvocation input)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Pre  Invoke Method: {0}.{1}, ", input.Target.GetType().Name, input.MethodBase.Name));
            sb.Append("Arguments: ");
            for (var i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments[i] == null)
                {
                    throw new ArgumentNullException(input.Arguments.ParameterName(i));
                }
                sb.Append(string.Format("{0}: \"{1}\"; ", input.Arguments.ParameterName(i), input.Arguments[i]));
            }

            LogDebug(sb.ToString());
        }

        private void PostInvoke(IMethodInvocation input)
        {
            string msg = string.Format("Post Invoke Method: {0}.{1}", input.Target.GetType().Name, input.MethodBase.Name);
            LogDebug(msg);
        }

        private void OnException(Exception ex)
        {
            string msg = string.Format("Exception throw: {0}", ex.Message);
            LogDebug(msg);
        }

        private void OnException(IMethodInvocation input, IMethodReturn methodReturn)
        {
            if (methodReturn.Exception != null)
            {
                string msg = string.Format("Exception throw: {0}", methodReturn.Exception.Message);
                LogDebug(msg);
                LogError(msg);
            }
        }

        private void LogDebug(string message)
        {
            LoggingFactory.GetLogger().Log(LogLevel.Debug, message);
        }

        private void LogError(string message)
        {
            LoggingFactory.GetLogger().Log(LogLevel.Error, message);
        }
    }
}
