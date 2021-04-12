import { ActionTypes } from '../enum';

const initState = {
	path: [
		{
			uuid: '00000000-0000-0000-0000-000000000000',
			name: 'My projects',
			parent: null,
			tasks: [],
		},
	],
	projects: [],
	tasks: [],
};

/**
 * @param {object} props props received.
 * @returns {object} returns state.
 */
export function State(props) {
	return { ...props, error: false, loading: false, data: initState };
}

/**
 * @param {object} state current state.
 * @param {object} action current action.
 * @returns {object} returns new state.
 */
export function Reducer(state, action) {
	const { data } = state;
	const { type, payload } = action;

	switch (type) {
		case ActionTypes.ProjectAdd:
			return { ...state, data: { ...payload } };
		case ActionTypes.ProjectChange:
			return { ...state, data: { ...payload } };
		case ActionTypes.ProjectDelete:
			return { ...state, data: { ...payload } };
		case ActionTypes.ProjectError:
			return { ...state, error: payload };
		case ActionTypes.ProjectLoad:
			return { ...state, data: { ...state.data, ...payload } };
		case ActionTypes.ProjectLoading:
			return { ...state, loading: payload };
		case ActionTypes.ProjectSelect:
			return { ...state, data: { ...data, path: payload } };
		default:
			return state;
	}
}
