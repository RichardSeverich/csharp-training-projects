// <copyright file="ModelStateExtensions.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Utilities.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class ModelStateExtensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Extension method, gets the dictionary of the model state and returns a list of strings with the error messages
        /// </summary>
        /// <param name="id">Model State Dictionary with the errors</param>
        /// <returns status="update status">List of the error messages</returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(model => model.Value.Errors)
                             .Select(model => model.ErrorMessage)
                             .ToList();
        }
    }
}
