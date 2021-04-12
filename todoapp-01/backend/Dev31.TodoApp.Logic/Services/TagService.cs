// <copyright file="TagService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Logic.Services
{
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Logic.Communication;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class TagService extends ITagService
    /// </summary>
    public class TagService : ITagService<TodoAppAPIResponse<Tag>>
    {

        /// <summary>
        /// Constructor of the Service
        /// </summary>
        /// <param name="tagRepository">Instance of a TagRepository</param>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        public TagService(IRepository<Tag, PostOptions, string> tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Deletes a project using its name as identifier
        /// </summary>
        /// <param name="name">Name of the tag to be deleted</param>
        /// <returns TagResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<Tag>> DeleteAsync(string name)
        {
            var existingTag = await _tagRepository.GetByPrimaryKeyAsync(name);
            if (existingTag == null)
                return new TodoAppAPIResponse<Tag>("Tag not found.");

            try
            {
                _tagRepository.Delete(existingTag);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<Tag>(existingTag);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<Tag>($"An error ocurred when updating the tag: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a list with all the available tags in the database
        /// </summary>
        /// <returns>List of available tags</returns>
        public async Task<IEnumerable<Tag>> ListAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        /// <summary>
        /// Add a new tag to the db
        /// </summary>
        /// <param name="tag">Instance of the new tag to be added</param>
        /// <returns TagResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<Tag>> SaveAsync(Tag tag)
        {
            try
            {
                await _tagRepository.AddAsync(tag);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<Tag>(tag);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<Tag>($"An error ocurred when saving the tag: {ex.Message}");
            }
        }

        /// <summary>
        /// property _tagRepository, instance of TagRepository
        /// </summary>
        private IRepository<Tag, PostOptions, string> _tagRepository;

        /// <summary>
        /// property _unitOfWork, instance of UnitOfWork
        /// </summary>
        private IUnitOfWork _unitOfWork;
    }
}
