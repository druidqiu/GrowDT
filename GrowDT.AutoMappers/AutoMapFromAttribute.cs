using System;

namespace GrowDT.AutoMappers
{
    public class AutoMapFromAttribute : AutoMapAttribute
    {
        internal override AutoMapDirection Direction
        {
            get { return AutoMapDirection.From; }
        }

        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
