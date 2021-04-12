import { getData } from '../api/api';

/**
 * @param {object} state User's data.
 * @returns {object} validation response.
 */
export function ValidateFields(state) {
	return Promise.all([
		ValidateName(state.Name),
		ValidateLastname(state.Lastname),
		ValidatePassword(state.Password),
		ValidateUsername(state.Username),
		ValidateEmail(state.Email),
	])
		.then((result) => {
			let response = {
				Name: result[0],
				Lastname: result[1],
				Password: result[2],
				Username: result[3],
				Email: result[4],
			};
			return response;
		})
		.catch((error) => console.log(error));
}

/**
 * @param {string} name user's name.
 * @returns {object} name's validation and message if an error occurred.
 */
export function ValidateName(name) {
	if (!name.length) {
		return { state: true, message: 'The name is required.' };
	} else {
		return { state: false, message: '' };
	}
}

/**
 * @param {string} lastname user's last name.
 * @returns {object} last name's validation and message if an error occurred.
 */
export function ValidateLastname(lastname) {
	if (!lastname.length) {
		return { state: true, message: 'The lastname is required.' };
	} else {
		return { state: false, message: '' };
	}
}

/**
 * @param {string} username user's username.
 * @returns {object} username's validation and message if an error occurred.
 */
export async function ValidateUsername(username) {
	if (!username.length) {
		return { state: true, message: 'The username is required.' };
	}

	let isValid = ValidateUsernameLength(username);
	if (!isValid) {
		return { state: true, message: 'The min length is 4 characters.' };
	}

	isValid = await ValidateUsernameNotExist(username);
	if (!isValid) {
		return { state: true, message: 'Username already exist.' };
	}

	return { state: false, message: '' };
}

/**
 * @param {string} Username user's username.
 * @returns {boolean} username length validation.
 */
export function ValidateUsernameLength(Username) {
	const patron = /^.{4,}$/;
	return Username.match(patron);
}

/**
 * @param {string} username user's username.
 * @returns {boolean} confirmation if the username already exists.
 */
async function ValidateUsernameNotExist(username) {
	return await getData(`signup?username=${username}`);
}

/**
 * @param {string} email user's email.
 * @returns {object} email's validation and message if an error occurred.
 */
export async function ValidateEmail(email) {
	if (!email.length) {
		return { state: true, message: 'The email is required.' };
	}

	let isValid = ValidateEmailFormat(email);
	if (!isValid) {
		return { state: true, message: 'This is not a valid email.' };
	}

	isValid = await ValidateEmailNotExist(email);
	if (!isValid) {
		return { state: true, message: 'Email already exist.' };
	}

	return { state: false, message: '' };
}

/**
 * @param {string} email user's email.
 * @returns {boolean} email's validation for patron matching.
 */
export function ValidateEmailFormat(email) {
	const patron = /^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$/;
	return email.match(patron);
}

/**
 * @param {string} email user's email.
 * @returns {boolean} email's validation if email already exists
 */
async function ValidateEmailNotExist(email) {
	return await getData(`signup?email=${email}`);
}

/**
 * @param {string} password user's password.
 * @returns {object} password's validation and message if an error occurred.
 */
export function ValidatePassword(password) {
	if (!password.length) {
		return { state: true, message: 'The password is required' };
	}

	if (!ValidatePasswordLength(password)) {
		return { state: true, message: 'The min length is 8 characters.' };
	} else if (!ValidatePasswordFormat(password)) {
		return {
			state: true,
			message:
				// eslint-disable-next-line max-len
				'The password requires at least one uppercase letter, one lowercase letter and one number.',
		};
	} else {
		return { state: false, message: '' };
	}
}

/**
 * @param {string} password user's password.
 * @returns {boolean} password's validation if password matches the required patron.
 */
export function ValidatePasswordFormat(password) {
	const patron = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$/;
	return password.match(patron);
}

/**
 * @param {string} password user's password.
 * @returns {boolean} password's validation for the minimum length.
 */
export function ValidatePasswordLength(password) {
	const patron = /^.{8,}$/;
	return password.match(patron);
}
