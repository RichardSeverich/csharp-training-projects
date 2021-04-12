import { ActionTypes } from '../enum';
import noop from 'helpers/noop';
import { getDataWithToken, postDataWithToken } from 'api/api';
import { getToken } from 'helpers/helper';

/**
 * @param {Function} dispatch function.
 * @param {string} payload is a tag name.
 */
function OnTagAdd(dispatch, payload) {
	const newTag = { name: payload };
	postDataWithToken('tags', newTag, getToken())
		.then(() => OnTagLoad(dispatch))
		.catch((error) => {
			console.error(error);
		});
}

/**
 * @param {Function} dispatch function.
 */
function OnTagLoad(dispatch) {
	OnTagLoading(dispatch, true);
	setTimeout(
		() =>
			getDataWithToken('tags', getToken())
				.then((tags) => {
					OnTagLoading(dispatch, false);
					dispatch({ type: ActionTypes.TagLoad, payload: { tags } });
				})
				.catch((error) => {
					OnTagLoading(dispatch, false);
					dispatch({ type: ActionTypes.TagError, payload: true });
					console.error(error);
				}),
		1000
	);
}

/**
 * @param {Function} dispatch function.
 * @param {boolean} payload is a state.
 */
function OnTagLoading(dispatch, payload) {
	dispatch({ type: ActionTypes.TagLoading, payload });
}

/**
 * @param {Function} dispatch function.
 * @param {object[]} payload selected tags.
 */
function OnTagSelect(dispatch, payload) {
	dispatch({ type: ActionTypes.TagSelect, payload });
}

/**
 * @param {Function} dispatch function.
 * @returns {object} callback functions.
 */
export default function ActionFactory(dispatch = noop) {
	return {
		OnTagAdd: (payload) => OnTagAdd(dispatch, payload),
		OnTagLoad: () => OnTagLoad(dispatch),
		OnTagLoading: (payload) => OnTagLoading(dispatch, payload),
		OnTagSelect: (payload) => OnTagSelect(dispatch, payload),
	};
}
