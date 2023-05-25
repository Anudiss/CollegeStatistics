using CollegeStatictics.DataTypes.Interfaces;

using System.Windows;

namespace CollegeStatictics.ViewModels;

public class AttendanceReport : IReport
{
    public string? Title { get; }
    public DataTemplate ContentTemplate => (DataTemplate)Application.Current.FindResource("ReportTemplate");

    public FrameworkElement Generate()
    {
        return null;
    }
}
