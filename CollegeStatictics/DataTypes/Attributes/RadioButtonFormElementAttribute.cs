using CollegeStatictics.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CollegeStatictics.ViewModels.Attributes
{
    public class RadioButtonFormElementAttribute : FormElementAttribute
    {
        public RadioButtonFormElementAttribute()
        {
            ElementType = ElementType.RadioButton;
        }
    }
}
