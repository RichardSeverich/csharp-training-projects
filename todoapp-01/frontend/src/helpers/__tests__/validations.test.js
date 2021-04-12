/* eslint-disable max-len */
import { ValidateName, ValidateLastname } from '../validations';
import { ValidatePassword, ValidatePasswordFormat, ValidatePasswordLength } from '../validations';
import { ValidateEmail, ValidateEmailFormat } from '../validations';
import { ValidateUsername, ValidateUsernameLength } from '../validations';

describe('helpers/validations', () => {
	describe('Testing methods for validations', () => {
		describe('Validations password methods.', () => {
			test('passing a password with correct format should return true', () => {
				const password = 'qweaSd13';
				const expectValidation = ValidatePasswordFormat(password);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a password without upper letter should return false', () => {
				const password = 'qweasd13';
				const expectValidation = ValidatePasswordFormat(password);
				expect(expectValidation).toBeFalsy();
			});

			test('passing a password without lower letter should return false', () => {
				const password = 'QWEASD13';
				const expectValidation = ValidatePasswordFormat(password);
				expect(expectValidation).toBeFalsy();
			});

			test('passing a password without number should return false', () => {
				const password = 'qweasdot';
				const expectValidation = ValidatePasswordFormat(password);
				expect(expectValidation).toBeFalsy();
			});

			test('passing a password with length equal than 8 should return true', () => {
				const password = 'qweasdtt';
				const expectValidation = ValidatePasswordLength(password);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a password with length greater than 8 should return true', () => {
				const password = 'qweasdttest';
				const expectValidation = ValidatePasswordLength(password);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a password with length less than 8 should return false', () => {
				const password = 'qweasdt';
				const expectValidation = ValidatePasswordLength(password);
				expect(expectValidation).toBeFalsy();
			});

			test('ValidatePassword should return an object with state true and a message when password is empty', () => {
				const password = '';
				const message = 'The password is required';
				const expectValue = ValidatePassword(password);
				expect(expectValue.state).toBeTruthy();
				expect(expectValue.message).toStrictEqual(message);
			});

			test('ValidatePassword should return an object with state true and a message when the length is less than 8', () => {
				const password = 'asd';
				const message = 'The min length is 8 characters.';
				const expectValue = ValidatePassword(password);
				expect(expectValue.state).toBeTruthy();
				expect(expectValue.message).toStrictEqual(message);
			});

			test("ValidatePassword should return an object with state true and a message when password don't have required format.", () => {
				const password = 'asdfghjka';
				const message =
					'The password requires at least one uppercase letter, one lowercase letter and one number.';
				const expectValue = ValidatePassword(password);
				expect(expectValue.state).toBeTruthy();
				expect(expectValue.message).toStrictEqual(message);
			});

			test('ValidatePassword should return an object with state false and a message empty when password is valid.', () => {
				const password = 'Asdfghjka1';
				const message = '';
				const expectValue = ValidatePassword(password);
				expect(expectValue.state).toBeFalsy();
				expect(expectValue.message).toStrictEqual(message);
			});
		});

		describe('Validations username methods.', () => {
			test('passing a username with length greater than 4 should return false', () => {
				const username = 'Charly';
				const expectValidation = ValidateUsernameLength(username);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a username with length equal than 4 should return false', () => {
				const username = 'Char';
				const expectValidation = ValidateUsernameLength(username);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a username with length less than 4 should return false', () => {
				const username = 'Cha';
				const expectValidation = ValidateUsernameLength(username);
				expect(expectValidation).toBeFalsy();
			});

			test('ValidateUsername should return an object with state true and a message when username is empty', () => {
				const username = '';
				const message = 'The username is required.';
				return ValidateUsername(username).then((expectValue) => {
					expect(expectValue.state).toBeTruthy();
					expect(expectValue.message).toStrictEqual(message);
				});
			});

			test('ValidatePassword should return an object with state true and a message when the length is less than 4', () => {
				const username = 'asd';
				const message = 'The min length is 4 characters.';
				return ValidateUsername(username).then((expectValue) => {
					expect(expectValue.state).toBeTruthy();
					expect(expectValue.message).toStrictEqual(message);
				});
			});
		});

		describe('Validations email methods.', () => {
			test('passing a email with correct format should return true', () => {
				const email = 'example@mysite.com';
				const expectValidation = ValidateEmailFormat(email);
				expect(expectValidation).toBeTruthy();
			});

			test('passing a email without at(@) should return false', () => {
				const email = 'examplemysite.com';
				const expectValidation = ValidateEmailFormat(email);
				expect(expectValidation).toBeFalsy();
			});

			test('passing a email without extension before dot should return false', () => {
				const email = 'example@mysite.';
				const expectValidation = ValidateEmailFormat(email);
				expect(expectValidation).toBeFalsy();
			});

			test('ValidateEmail should return an object with state true and a message when email is empty', () => {
				const email = '';
				const message = 'The email is required.';
				return ValidateEmail(email).then((expectValue) => {
					expect(expectValue.state).toBeTruthy();
					expect(expectValue.message).toStrictEqual(message);
				});
			});

			test('ValidateEmail should return an object with state true and a message when there is an email invalid', () => {
				const email = 'examplemysite.com';
				const message = 'This is not a valid email.';
				return ValidateEmail(email).then((expectValue) => {
					expect(expectValue.state).toBeTruthy();
					expect(expectValue.message).toStrictEqual(message);
				});
			});
		});

		describe('Validations name method.', () => {
			test('ValidateName should return a state true and a message when name is empty', () => {
				const name = '';
				const message = 'The name is required.';
				const expectValue = ValidateName(name);
				expect(expectValue.state).toBeTruthy();
				expect(expectValue.message).toStrictEqual(message);
			});

			test('ValidateName should return a state false and a message empty when name is not empty', () => {
				const name = 'Charly';
				const message = '';
				const expectValue = ValidateName(name);
				expect(expectValue.state).toBeFalsy();
				expect(expectValue.message).toStrictEqual(message);
			});
		});

		describe('Validations lastname method.', () => {
			test('ValidateLastname should return a state true and a message when lastname is empty', () => {
				const lastname = '';
				const message = 'The lastname is required.';
				const expectValue = ValidateLastname(lastname);
				expect(expectValue.state).toBeTruthy();
				expect(expectValue.message).toStrictEqual(message);
			});

			test('ValidateLastname should return a state false and a message empty when lastname is not empty', () => {
				const lastname = 'Meneces';
				const message = '';
				const expectValue = ValidateLastname(lastname);
				expect(expectValue.state).toBeFalsy();
				expect(expectValue.message).toStrictEqual(message);
			});
		});
	});
});
