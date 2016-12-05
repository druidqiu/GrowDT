using System;

namespace GrowDT.AutoMappers
{
    public class AutoMapAttribute : Attribute
    {
        public Type[] TargetTypes { get; private set; }

        internal virtual AutoMapDirection Direction
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }

        public AutoMapAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }
    }
}
