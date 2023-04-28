using CollegeStatictics.ViewModels.Attributes;
using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntitiesGridFormElementAttribute : FormElementAttribute
    {
        public string ItemContainerName { get; }

        public EntitiesGridFormElementAttribute(string itemContainerName)
        {
            ItemContainerName = itemContainerName;
            ElementType = ElementType.EntitiesGrid;
        }
    }
}
