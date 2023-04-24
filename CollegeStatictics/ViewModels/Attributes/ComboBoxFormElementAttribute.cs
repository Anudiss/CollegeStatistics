namespace CollegeStatictics.ViewModels.Attributes
{
    public class EntitySelectorFormElementAttribute : FormElementAttribute
    {
        public string ItemContainerName { get; }
        
        public EntitySelectorFormElementAttribute(string itemContainerName)
        {
            ItemContainerName = itemContainerName;
            ElementType = ElementType.EntitySelectorBox;
        }
    }
}
