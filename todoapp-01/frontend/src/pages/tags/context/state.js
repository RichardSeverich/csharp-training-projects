import { ActionTypes } from '../enum';

const initState = {
	tags: [],
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
		case ActionTypes.TagAdd:
			return { ...state, data: { ...payload } };
		case ActionTypes.TagError:
			return { ...state, error: payload };
		case ActionTypes.TagLoad:
			return { ...state, data: { ...state.data, ...payload } };
		case ActionTypes.TagLoading:
			return { ...state, loading: payload };
		case ActionTypes.TagSelect:
			return { ...state, data: { ...data, selected: payload } };
		default:
			return state;
	}
}
