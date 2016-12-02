using System;

namespace GrowDT.Auditing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {
    }
}
