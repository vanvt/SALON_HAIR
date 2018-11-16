using System;
using System.Collections.Generic;
using System.Text;

namespace SALON_HAIR_CORE.Utilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
    internal sealed class NotNullAttribute : Attribute
    {
    }
}
