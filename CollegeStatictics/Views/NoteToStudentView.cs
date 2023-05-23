using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.Views
{
    [ViewTitle("Заметка к студенту")]
    public class NoteToStudentView : ItemDialog<NoteToStudent>
    {
        public NoteToStudentView(NoteToStudent? item) : base(item)
        {
        }
    }
}
