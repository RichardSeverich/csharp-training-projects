import { editStatus } from '../actions';

describe('Helpers/actions', () => {
	describe('Testing editStatus method', () => {
		test('editStatus should return an array of 2 elements when there is an valid input', () => {
			const newStatus = {
				id: '0000001',
				newContent: 'In Progress',
			};
			const expectedValue = editStatus(newStatus);
			expect(expectedValue).toStrictEqual(['0000001', { status: 'In Progress' }]);
		});
	});
});
