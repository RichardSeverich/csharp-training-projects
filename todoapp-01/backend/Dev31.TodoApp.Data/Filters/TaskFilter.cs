namespace Dev31.TodoApp.Data.Filters
{
    using Dev31.TodoApp.Interfaces;
    using Dev31.TodoApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    public class TaskFilter : IFilter<TodoTask>
    {
        public TaskFilter(PostOptions options)
        {
            _filterOptions = options;
        }
        
        public IQueryable<TodoTask> Filter(IQueryable<TodoTask> items)
        {
            items = FilterDate(items, _filterOptions.Entry);
            items = FilterDescription(items, _filterOptions.Description);
            items = FilterPriority(items, _filterOptions.Priority);
            items = FilterProject(items, _filterOptions.Project);
            items = FilterStatus(items, _filterOptions.Status);
            items = FilterTags(items, _filterOptions.Tags);

            return items;
        }

        private IQueryable<TodoTask> FilterDate(IQueryable<TodoTask> items, string date)
        {
            if (date == null)
                return items;

            return items.Where(task => string.Compare(task.Entry, date) > 0);
        }

        private IQueryable<TodoTask> FilterDescription(IQueryable<TodoTask> items, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return items;

            return items.Where(task => task.Description.Contains(content));
        }

        private IQueryable<TodoTask> FilterPriority(IQueryable<TodoTask> items, string priority)
        {
            if (priority == null)
                return items;

            if (priority.StartsWith("N!"))
            {
                return items.Where(task => task.Priority != priority.Substring(2));
            }

            return items.Where(task => task.Priority == priority);
        }

        private IQueryable<TodoTask> FilterProject(IQueryable<TodoTask> items, string project)
        {
            if (project == null)
                return items;

            if (project.StartsWith("N!"))
            {
                return items.Where(task => task.ProjectUuid.ToString() != project.Substring(2));
            }

            return items.Where(task => task.ProjectUuid.ToString() == project);
        }

        private IQueryable<TodoTask> FilterStatus(IQueryable<TodoTask> items, ICollection<string> statusC)
        {
            if (statusC == null || statusC.Count == 0)
                return items;

            var statusN = statusC.Where(status => status.StartsWith("N!")).Select(status => status.Substring(2)).ToList();
            var statusY = statusC.Where(status => !status.StartsWith("N!")).ToList();

            if (statusN.Count > 0)
            {
                items = items.Where(task => statusN.All(status => status != task.Status));
            }

            if (statusY.Count > 0)
            {
                items = items.Where(task => statusY.Any(status => status == task.Status));
            }

            return items;
        }

        private IQueryable<TodoTask> FilterTags(IQueryable<TodoTask> items, ICollection<string> tags)
        {
            if (tags == null || tags.Count == 0)
                return items;

            var tagsN = tags.Where(tag => tag.StartsWith("N!")).Select(tag => tag.Substring(2)).ToList();
            var tagsY = tags.Where(tag => !tag.StartsWith("N!")).ToList();

            if (tagsN.Count > 0)
            {
                items = items.Where(task => task.Tags.All(tag => tagsN.All(tagN => tagN != tag.Id_Tag.ToString())));
            }

            if(tagsY.Count > 0)
            {
                items = items.Where(task => task.Tags.Any(tag => tagsY.Any(tagY => tagY == tag.Id_Tag.ToString())));
            }

            return items;
        }

        private PostOptions _filterOptions;
    }
}
