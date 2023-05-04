using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.Views
{
    public class NoteToStudentView : ItemDialog<NoteToStudent>
    {


        public NoteToStudentView(NoteToStudent? item) : base(item)
        {
        }
    }
}
