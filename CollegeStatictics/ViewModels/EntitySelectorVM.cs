using CollegeStatictics.Database;
using CollegeStatictics.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace CollegeStatictics.ViewModels
{
    public class EntitySelectorVM<T> : WindowViewModelBase where T : class
    {
        public EntitySelectorVM() {
            var values = DatabaseContext.Entities.Set<T>();
            values.Load();
        }
    }
}
