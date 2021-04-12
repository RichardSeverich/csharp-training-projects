/**
 * @param {object} errors occurred errors.
 * @returns {boolean} confirmation if there was any error.
 */
export function thereIsAnError(errors) {
	for (let field in errors) {
		if (errors[field].state === true) {
			return true;
		}
	}

	return false;
}

/**
 * @returns {string} user's token.
 */
export function getToken() {
	const profile = localStorage.getItem('profile');
	const profileParsed = JSON.parse(profile);
	const pathname = window.location.pathname;
	let expired = profileParsed ? parseInt(profileParsed.expires) < Date.now() : true;

	if (expired && pathname !== '/sign-in' && pathname !== '/sign-up') {
		localStorage.removeItem('profile');
		alert('Your session has expired! Sign in again');
		window.location.href = '/sign-in';
	}

	const token = !expired ? profileParsed.token : '';

	return token;
}
