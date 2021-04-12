import { actions } from './enum';
import noop from 'helpers/noop';
import Session from './session';

/**
 * @param {object} dispatch dispatch object
 * @param {object} payload User profile
 */
function OnStart(dispatch, payload) {
	dispatch({ type: actions.SetSession, payload });
}

/**
 * @param {object} dispatch dispatch object
 */
function OnChange(dispatch) {
	dispatch({ type: actions.HasChanged, payload: {} });
}

/**
 * @param {object} dispatch dispatch object
 */
function OnCloseSession(dispatch) {
	const session = new Session();
	dispatch({ type: actions.Logout, payload: session });
}

/**
 * @param {Function} dispatch callback.
 * @returns {object} callback actions.
 */
export default function ActionFactory(dispatch = noop) {
	return {
		start: (payload) => OnStart(dispatch, payload),
		refresh: () => OnChange(dispatch),
		closeSession: () => OnCloseSession(dispatch),
	};
}
