import noop from '../noop';

describe('Helpers/noop', () => {
	test('Noop shoul return undefined', () => {
		const expectedValue = noop();
		expect(expectedValue).toStrictEqual(undefined);
	});
});
