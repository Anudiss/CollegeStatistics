using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeStatictics.DataTypes.Interfaces;

public interface IReport : IContent
{
    public FrameworkElement View => Generate();

    public FrameworkElement Generate();
}
