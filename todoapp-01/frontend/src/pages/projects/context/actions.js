import { ActionTypes } from '../enum';
import noop from 'helpers/noop';
import { deleteData, getDataWithToken, postDataWithToken, updateData } from 'api/api';
import { idRoot } from 'helpers/constants';
import { getToken } from 'helpers/helper';

/**
 * @param {Function} dispatch function.
 * @param {object} payload new project.
 */
function OnProjectAdd(dispatch, payload) {
	postDataWithToken('projects', payload, getToken())
		.then(() => {
			OnProjectLoad(dispatch);
		})
		.catch((error) => {
			dispatch({ type: ActionTypes.ProjectError, payload: true });
			console.error(error);
		});
}

/**
 * @param {Function} dispatch function.
 * @param {object} payload project changed.
 */
function OnProjectChange(dispatch, payload) {
	updateData('projects/' + payload.uuid, payload.data, getToken())
		.then(() => {
			OnProjectLoad(dispatch);
		})
		.catch((error) => {
			dispatch({ type: ActionTypes.ProjectError, payload: true });
			console.error(error);
		});
}

/**
 * @param {Function} dispatch function.
 * @param {string} payload project uuid.
 */
function OnProjectDelete(dispatch, payload) {
	deleteData('projects/' + payload, getToken())
		.then(() => {
			OnProjectLoad(dispatch);
		})
		.catch((error) => {
			dispatch({ type: ActionTypes.ProjectError, payload: true });
			console.error(error);
		});
}

/**
 * @param {Function} dispatch function.
 */
export function OnProjectLoad(dispatch) {
	OnProjectLoading(dispatch, true);
	setTimeout(
		() =>
			getDataWithToken('projects', getToken())
				.then((projects) => {
					const initPath = [projects.find((p) => p.uuid === idRoot)];
					OnProjectSelect(dispatch, initPath);
					OnProjectLoading(dispatch, false);
					dispatch({ type: ActionTypes.ProjectLoad, payload: { projects } });
				})
				.catch((error) => {
					OnProjectLoading(dispatch, false);
					dispatch({ type: ActionTypes.ProjectError, payload: true });
					console.error(error);
				}),
		1000
	);
}

/**
 * @param {Function} dispatch function.
 * @param {boolean} payload is a state.
 */
function OnProjectLoading(dispatch, payload) {
	dispatch({ type: ActionTypes.ProjectLoading, payload });
}

/**
 * @param {Function} dispatch function.
 * @param {object[]} payload selected projects.
 */
function OnProjectSelect(dispatch, payload) {
	dispatch({ type: ActionTypes.ProjectSelect, payload });
}

/**
 * @param {Function} dispatch function.
 * @returns {object} callback functions.
 */
export default function ActionFactory(dispatch = noop) {
	return {
		onProjectAdd: (payload) => OnProjectAdd(dispatch, payload),
		onProjectChange: (payload) => OnProjectChange(dispatch, payload),
		onProjectDelete: (payload) => OnProjectDelete(dispatch, payload),
		onProjectLoad: () => OnProjectLoad(dispatch),
		onProjectLoading: (payload) => OnProjectLoading(dispatch, payload),
		onProjectSelect: (payload) => OnProjectSelect(dispatch, payload),
	};
}
