import Setting from './apisettings.json';

/**
 * @param {string} endpoint request path.
 * @param {string} token Token for authentication
 * @returns {Promise} response of request.
 */
export function deleteData(endpoint, token) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		method: 'DELETE',
		headers: {
			Authorization: `Bearer ${token}`,
		},
	});
}

/**
 * @param {string} endpoint request path.
 * @returns {Promise} response of request.
 */
export function getData(endpoint) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url).then((response) => response.json());
}

/**
 * @param {string} endpoint request path.
 * @param {string} token Token for authentication
 * @returns {Promise} response of request.
 */
export function getDataWithToken(endpoint, token) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		headers: {
			Authorization: `Bearer ${token}`,
		},
	}).then((response) => response.json());
}

/**
 * @param {string} endpoint request path.
 * @param {object} data body of request.
 * @returns {Promise} response of request.
 */
export function saveData(endpoint, data) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		method: 'POST',
		body: JSON.stringify(data),
		headers: {
			'Content-type': 'application/json',
		},
	});
}

/**
 * @param {string} endpoint request path.
 * @param {object} data body of request.
 * @param {string} token Token for authentication
 * @returns {Promise} response of request.
 */
export function postDataWithToken(endpoint, data, token) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		method: 'POST',
		body: JSON.stringify(data),
		headers: {
			'Content-type': 'application/json',
			Authorization: `Bearer ${token}`,
		},
	});
}

/**
 * @param {string} endpoint request path.
 * @param {object} data body of request.
 * @param {string} token Token for authentication
 * @returns {Promise} response of request.
 */
export function updateData(endpoint, data, token) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		method: 'PUT',
		body: JSON.stringify(data),
		headers: {
			'Content-type': 'application/json',
			Authorization: `Bearer ${token}`,
		},
	});
}

/**
 * @param {string} endpoint request path.
 * @param {object} status body of request.
 * @param {string} token Token for authentication
 * @returns {Promise} response of request.
 */
export function updateStatus(endpoint, status, token) {
	const url = `${Setting.host}/${Setting.api}/${endpoint}`;

	return fetch(url, {
		method: 'PATCH',
		body: JSON.stringify(status),
		headers: {
			'Content-type': 'application/json',
			Authorization: `Bearer ${token}`,
		},
	});
}
