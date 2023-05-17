using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeStatictics.ViewModels;

public class Report : ObservableValidator, IReport
{
    /*
     * Отборы, таблицы из БД, строка автосуммы
     */

    public ObservableCollection<ISelection<object>> Filters { get; } = new();

    public DataTemplate ContentTemplate => throw new NotImplementedException();

    public Report()
    {
        
    }

    public void Refresh() => OnPropertyChanged(nameof(IReport.View));

    public FrameworkElement Generate() => throw new NotImplementedException();
}
