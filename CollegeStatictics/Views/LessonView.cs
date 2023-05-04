using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.Views
{
    public class LessonView : ItemDialog<Lesson>
    {


        public LessonView(Lesson? item) : base(item)
        {
        }
    }
}
