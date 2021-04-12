import Session from './session';
import { actions } from './enum';

/**
 * @param {object} props received properties.
 * @returns {object} props updated with the session
 */
export function State(props) {
	const session = new Session();
	const hasChanged = false;
	return {
		...props,
		session,
		hasChanged,
	};
}

/**
 * @param {object} state previous state.
 * @param {object} payload user's profile.
 * @returns {object} state updated with the session.
 */
function mergeSession(state, payload) {
	if (state.session.hasSession()) {
		return state;
	}

	const session = new Session({ profile: payload });

	return {
		...state,
		session,
	};
}

/**
 * @param {object} state previous state.
 * @param {object} action current action
 * @returns {object} New state.
 */
export function Reducer(state, action) {
	const { type, payload } = action;

	switch (type) {
		case actions.SetSession:
			return mergeSession(state, payload);
		case actions.HasChanged:
			return { ...state, hasChanged: !state.hasChanged };
		case actions.CloseSession:
			return { ...state, session: payload };
		default:
			return state;
	}
}
