namespace CollegeStatictics.ViewModels.Attributes
{
    public class EntitySelectorFormElementAttribute : FormElementAttribute
    {
        public string ItemContainerName { get; }

        public string? FilterPropertyName { get; set; } = null;
        
        public EntitySelectorFormElementAttribute(string itemContainerName)
        {
            ItemContainerName = itemContainerName;
            ElementType = ElementType.EntitySelectorBox;
        }
    }
}
