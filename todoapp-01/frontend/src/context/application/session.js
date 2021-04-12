/**
 * @returns {object} user's profile.
 */
function readProfile() {
	const value = localStorage.getItem('profile');
	const profile = JSON.parse(value);

	return profile || {};
}

/**
 * @param {object} profile user's profile.
 */
function writeProfile(profile) {
	const value = JSON.stringify(profile);

	localStorage.setItem('profile', value);
}

export default class Session {
	constructor(props) {
		const { loggedIn = false, profile = {} } = props || {};

		if (Object.keys(profile).length) {
			let now = Date.now();
			let expires = now + 15 * 60 * 1000;
			profile.expires = expires;
			writeProfile(profile);
		}

		this.profile = readProfile();
		this.loggedIn = loggedIn || this.hasSession();
	}

	hasSession() {
		return !!Object.keys(this.profile).length;
	}

	end() {
		localStorage.removeItem('profile');
	}
}
