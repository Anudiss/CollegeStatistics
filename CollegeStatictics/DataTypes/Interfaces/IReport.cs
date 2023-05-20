using System.Windows;

namespace CollegeStatictics.DataTypes.Interfaces;

public interface IReport : IContent
{
    public FrameworkElement View => Generate();

    protected FrameworkElement Generate();
}
