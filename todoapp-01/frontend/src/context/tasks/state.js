import { ActionTypes } from '../../enum';

/**
 * @param {object} props props received.
 * @returns {object} initial state.
 */
export function State(props) {
	return {
		...props,
		search: {
			description: '',
			priority: '',
			project: '',
			tags: [],
			entry: 'any',
			status: ['N!Deleted', ''],
		},
		currentPage: 1,
		pageSize: 5,
		totalPages: 1,
		data: { all: [], byStatus: [] },
		loading: false,
	};
}

/**
 * @param {object} state current state.
 * @param {object} action current action.
 * @returns {object} new state.
 */
export function Reducer(state, action) {
	const { type, payload } = action;

	switch (type) {
		case ActionTypes.TaskAdd:
			return { ...state, data: { ...payload } };
		case ActionTypes.TaskChange:
			return { ...state, data: { ...payload } };
		case ActionTypes.TaskDelete:
			return { ...state, data: { ...payload } };
		case ActionTypes.TaskLoad:
			return { ...state, ...payload, loading: false };
		case ActionTypes.TaskStatusChange:
			return { ...state, data: { ...payload } };
		case ActionTypes.LoadingChange:
			return { ...state, loading: payload };
		case ActionTypes.TaskLoadSuccess:
			return { ...state, loading: payload };
		case ActionTypes.PageChanged:
			return { ...state, ...payload };
		case ActionTypes.SearchChanged:
			return { ...state, search: { ...payload } };
		default:
			return state;
	}
}
