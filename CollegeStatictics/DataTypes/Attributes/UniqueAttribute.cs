using CollegeStatictics.Database;
using CollegeStatictics.ViewModels.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Documents;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        public string PropertyPath { get; }

        public UniqueAttribute(string propertyPath) =>
            PropertyPath = propertyPath;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var entityType = validationContext.ObjectInstance.GetType().BaseType!.GetGenericArguments()[0];
            var entities = DatabaseContext.LoadEntities(entityType);

            var property = entityType.GetProperty(PropertyPath);

            // 1) prop is number and value is number
            // 2) prop is not numbe and value is number
            // 3) prop is number and value is not number
            // 4) prop is not number and value is not number

            // Extensions.Single(values.Local.Select(item => GetValue(item)), value) = ;

            // TODO: Is it right to compare strings?
            foreach (var entity in entities.Local)
            {
                if (entity == ((dynamic)validationContext.ObjectInstance).Item)
                    continue;

                if (property!.GetValue(entity).ToString() == value!.ToString())
                    return new(ErrorMessage);
            }

            /*bool GetPropertyValue(object? item) => property!.GetValue(item).ToString() == value.ToString();

            if (EntityFrameworkQueryableExtensions.CountAsync(values.Local, i => GetPropertyValue(i)) != 1)
                return new(ErrorMessage);*/

            return ValidationResult.Success;
        }
    }
}
