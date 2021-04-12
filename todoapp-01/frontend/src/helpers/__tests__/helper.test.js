import { getToken, thereIsAnError } from '../helper';

describe('helper/helper', () => {
	describe('Testing thereIsAnError method', () => {
		test('Method should return false if there is an empty object.', () => {
			const errors = {};
			const expectedResult = thereIsAnError(errors);
			expect(expectedResult).toBeFalsy();
		});

		test('Method should return true if there is some field with state true.', () => {
			const errors = {
				Name: {
					state: false,
					message: '',
				},
				Lastname: {
					state: true,
					message: '',
				},
			};
			const expectedResult = thereIsAnError(errors);
			expect(expectedResult).toBeTruthy();
		});

		test('Method should return false if there is all fields with state false.', () => {
			const errors = {
				Name: {
					state: false,
					message: '',
				},
				Lastname: {
					state: false,
					message: '',
				},
			};
			const expectedResult = thereIsAnError(errors);
			expect(expectedResult).toBeFalsy();
		});
	});

	describe('Testing getToken method', () => {
		test('Method should return empty string if in local storage is not any profile', () => {
			const expectedResult = getToken();

			expect(expectedResult).toStrictEqual('');
		});

		// eslint-disable-next-line max-len
		test('Method should return empty string and remove user profile if there was not expired', () => {
			const profile = {
				email: 'joel@gmail.com',
				expires: Date.now() + 1 * 60 * 1000,
				token: 'My token',
				userId: 2,
				username: 'JoelWL',
			};
			localStorage.setItem('profile', JSON.stringify(profile));

			const expectedResult = getToken();

			expect(expectedResult).toStrictEqual('My token');
		});

		test('Method should return empty string and remove user profile if it is expired', () => {
			const profile = {
				email: 'joel@gmail.com',
				expires: 1614376138909,
				token: 'My token',
				userId: 2,
				username: 'JoelWL',
			};
			localStorage.setItem('profile', JSON.stringify(profile));

			const expectedResult = getToken();

			expect(expectedResult).toStrictEqual('');
		});
	});
});
