// <copyright file="Helpers.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Utilities.Helpers
{
    using System;

    /// <summary>
    /// Class Helpers
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Gets the now date and transforms it to a string of the following format: yyyy-MM-ddTHH:mm:ssZ
        /// </summary>
        /// <returns now="date time now">String of now date in Iso format</returns>
        public static string DateToIsoString()
        {
            var now = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");

            return now;
        }
    }
}
