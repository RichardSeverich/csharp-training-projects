// <copyright file="ProjectService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Logic.Services
{
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class ProjectService implements IProjectService
    /// </summary>
    public class ProjectService : IProjectService<TodoAppAPIResponse<Project>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectRepository">Instance of a ProjectRepository</param>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        public ProjectService(IRepository<Project, PostOptions, Guid> projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Deletes a project using its uuid
        /// </summary>
        /// <param name="id">Unique identifier of the project to be deleted</param>
        /// <returns ProjectResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<Project>> DeleteAsync(Guid id)
        {
            var existingProject = await _projectRepository.GetByPrimaryKeyAsync(id);

            if (existingProject == null)
                return new TodoAppAPIResponse<Project>("Project not found.");

            try
            {
                await CascadeDelete(existingProject);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<Project>(existingProject);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<Project>($"An error ocurred when saving the project: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes in cascade a project and its children projects
        /// </summary>
        /// <param name="id">Instance of the project to be deleted</param>
        private async Task CascadeDelete(Project project)
        {
            var childrenProjects = await _projectRepository.GetChildren(project.Uuid) as List<Project>;
            if(childrenProjects.Count == 0)
            {
                _projectRepository.Delete(project);
            }
            else
            {
                foreach(var children in childrenProjects)
                {
                    await CascadeDelete(children);
                    _projectRepository.Delete(project);
                }
            }

        }

        /// <summary>
        /// Find a project passing its uuid
        /// </summary>
        /// <param name="id">Unique identifier of the project</param>
        /// <returns>Instance of the project</returns>
        public async Task<Project> FindAsync(Guid id)
        {
            return await _projectRepository.GetByPrimaryKeyAsync(id);
        }

        /// <summary>
        /// Get a list with all the available projects in the database
        /// </summary>
        /// <return>List of available projects</returns>
        public async Task<IEnumerable<Project>> ListAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        /// <summary>
        /// Adds a new project to the Database
        /// </summary>
        /// <param name="project">Instance of the new project to be added</param>
        /// <returns ProjectResponse="response">Response based on the success of the operation</returns>
        public async Task<TodoAppAPIResponse<Project>> SaveAsync(Project project)
        {
            project.Uuid = Guid.NewGuid();

            try
            {
                await _projectRepository.AddAsync(project);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<Project>(project);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<Project>($"An error ocurred when saving the project: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing project in the database
        /// </summary>
        /// <param name="id">Unique identifier of the project</param>
        /// <param name="project">Project with the attributes to be updated</param>
        /// <returns ProjectResponse="response">Response based on the success of the operation</returns>
        public async Task<TodoAppAPIResponse<Project>> UpdateAsync(Guid id, Project project)
        {
            var existingProject = await _projectRepository.GetByPrimaryKeyAsync(id);

            if (existingProject == null)
                return new TodoAppAPIResponse<Project>("Project not found.");

            existingProject.Name = project.Name;

            try
            {
                _projectRepository.Update(existingProject);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<Project>(existingProject);
            }
            catch (Exception ex)
            {
                return new TodoAppAPIResponse<Project>($"An error ocurred when updating the project: {ex.Message}");
            }
        }

        /// <summary>
        /// property Instance of ProjectRepository
        /// property Instance of UnitOfWork
        /// </summary>
        private IRepository<Project, PostOptions, Guid> _projectRepository;
        private IUnitOfWork _unitOfWork;

    }
}
