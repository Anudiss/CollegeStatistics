using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        public string PropertyPath { get; }

        public UniqueAttribute(string propertyPath) =>
            PropertyPath = propertyPath;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            /*var property = validationContext.ObjectInstance.GetType().GetProperty(PropertyPath);*/

            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            Type genericType = validationContext.ObjectInstance.GetType().BaseType!.GetGenericArguments()[0];
            MethodInfo genericMethod = method.MakeGenericMethod(genericType);

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            var property = genericType.GetProperty(PropertyPath);

            foreach (dynamic item in values.Local)
                if (property!.GetValue(item) == value)
                    return new ValidationResult("Группа с таким номер уже есть");

            return ValidationResult.Success;
        }
    }
}
