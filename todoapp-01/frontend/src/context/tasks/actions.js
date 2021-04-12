import { ActionTypes } from '../../enum';
import { editStatus } from 'helpers/actions';
import noop from 'helpers/noop';
import { deleteData, updateStatus, updateData, postDataWithToken } from 'api/api';
import filter from 'helpers/filter';
import processTask from 'helpers/process';
import { getToken } from 'helpers/helper';

/**
 * @param {Function} dispatch function.
 * @param {object} payload is a task.
 */
function OnTaskAdd(dispatch, payload) {
	const newTask = processTask(payload);
	postDataWithToken('tasks/new-task', newTask, getToken()).catch((error) => console.error(error));
}

/**
 * @param {Function} dispatch function.
 * @param {object[]} payload list objects.
 */
function OnTaskChange(dispatch, payload) {
	let [id, task] = payload;
	const newTask = processTask(task);
	updateData(`tasks/${id}`, newTask, getToken()).catch((error) => console.error(error));
}

/**
 * @param {Function} dispatch function.
 * @param {string} payload task uuid.
 */
function OnTaskDelete(dispatch, payload) {
	deleteData(`tasks/${payload}`, getToken()).catch((error) => console.error(error));
}

/**
 * @param {Function} dispatch function.
 * @param {object} payload new content.
 */
function OnTaskStatusChange(dispatch, payload) {
	let [id, body] = editStatus(payload);
	updateStatus(`tasks/${id}`, body, getToken()).catch((error) => console.error(error));
}

/**
 * @param {Function} dispatch function.
 * @param {object} payload load parameters.
 */
function OnLoad(dispatch, payload) {
	dispatch({ type: ActionTypes.TaskLoadSuccess, payload: true });
	setTimeout(
		() =>
			postDataWithToken('tasks', payload, getToken())
				.then((response) => response.json())
				.then((response) => {
					const { currentPage } = response;
					let tasks = response.data ? response.data : [];
					let tasksByStatus = filter('status', tasks);
					const currentPageS = currentPage <= response.totalPages ? currentPage : 1;
					dispatch({ type: ActionTypes.TaskLoadSuccess, payload: false });
					dispatch({
						type: ActionTypes.TaskLoad,
						payload: {
							...response,
							currentPage: currentPageS,
							data: { all: tasks, byStatus: tasksByStatus },
						},
					});
				})
				.catch((error) => {
					OnTaskLoading(dispatch, false);
					console.error(error);
				}),
		1000
	);
}

/**
 * @param {Function} dispatch function.
 * @param {boolean} payload is a state.
 */
function OnTaskLoading(dispatch, payload) {
	dispatch({ type: ActionTypes.TaskLoadSuccess, payload });
}

/**
 * @param {Function} dispatch function.
 * @param {object} payload new page content.
 */
function OnPageChange(dispatch, payload) {
	dispatch({ type: ActionTypes.PageChanged, payload });
}

/**
 * @param {Function} dispatch function.
 * @param {object} payload new search content.
 */
function OnSearchChange(dispatch, payload) {
	dispatch({ type: ActionTypes.SearchChanged, payload });
}

/**
 * @param {Function} dispatch function.
 * @returns {object} callback actions.
 */
export default function ActionFactory(dispatch = noop) {
	return {
		OnTaskAdd: (payload) => OnTaskAdd(dispatch, payload),
		OnTaskChange: (payload) => OnTaskChange(dispatch, payload),
		OnTaskDelete: (payload) => OnTaskDelete(dispatch, payload),
		OnTaskStatusChange: (payload) => OnTaskStatusChange(dispatch, payload),
		OnTaskLoading: (payload) => OnTaskLoading(dispatch, payload),
		OnLoad: (payload) => OnLoad(dispatch, payload),
		OnPageChange: (payload) => OnPageChange(dispatch, payload),
		OnSearchChange: (payload) => OnSearchChange(dispatch, payload),
	};
}
