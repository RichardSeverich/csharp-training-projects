import { processTaskEdit, processTagsEdit } from '../process';
import processTask, { processTags } from '../process';
import { processSearch, processOptionsStrings, processOptionsObjects } from '../process';
import { idRoot, priorities } from '../constants';

describe('Helpers/process', () => {
	describe('Testing for process a task and tags for add', () => {
		describe('Testing for processTags method', () => {
			// eslint-disable-next-line max-len
			test('processTags should return an empty array when the input is an empty array', () => {
				const data = [];
				const expectResult = processTags(data);
				expect(expectResult).toStrictEqual([]);
			});

			// eslint-disable-next-line max-len
			test("processTags should return an array with tag's id when there is not an empty array", () => {
				const data = [1, 12, 7, 4];
				const result = [
					{
						Id_Tag: 1,
					},
					{
						Id_Tag: 12,
					},
					{
						Id_Tag: 7,
					},
					{
						Id_Tag: 4,
					},
				];
				const expectResult = processTags(data);
				expect(expectResult).toStrictEqual(result);
			});
		});

		describe('Testing for processTask method', () => {
			test('processTask should return task with processed data', () => {
				const task = {
					description: 'This is my description',
					tags: [1, 2, 3, 6, 4],
					projectUuid: undefined,
					status: 'Pending',
				};

				const processedTask = {
					description: 'This is my description',
					projectUuid: '00000000-0000-0000-0000-000000000000',
					status: 'Pending',
					tags: [
						{
							Id_Tag: 1,
						},
						{
							Id_Tag: 2,
						},
						{
							Id_Tag: 3,
						},
						{
							Id_Tag: 6,
						},
						{
							Id_Tag: 4,
						},
					],
				};

				const expectResult = processTask(task);
				expect(expectResult).toStrictEqual(processedTask);
			});

			test('processTask should return task with processed data with project', () => {
				const task = {
					description: 'This is my description',
					tags: [1, 2, 3, 6, 4],
					projectUuid: '00000000-0000-0000-0000-000000000001',
					status: 'Pending',
				};

				const processedTask = {
					description: 'This is my description',
					projectUuid: '00000000-0000-0000-0000-000000000001',
					status: 'Pending',
					tags: [
						{
							Id_Tag: 1,
						},
						{
							Id_Tag: 2,
						},
						{
							Id_Tag: 3,
						},
						{
							Id_Tag: 6,
						},
						{
							Id_Tag: 4,
						},
					],
				};

				const expectResult = processTask(task);
				expect(expectResult).toStrictEqual(processedTask);
			});
		});
	});

	describe('Testing for process a task and tags for edit', () => {
		describe('Testing for processTaskEdit method', () => {
			test('processTaskEdit should return task with processed data', () => {
				const task = {
					description: 'This is my description',
					tags: [
						{
							tag: {
								id: 1,
								name: 'tag1',
							},
						},
						{
							tag: {
								id: 12,
								name: 'tag13',
							},
						},
						{
							tag: {
								id: 7,
								name: 'tag21',
							},
						},
						{
							tag: {
								id: 4,
								name: 'tag5',
							},
						},
					],
					projectUuid: '00000000-0000-0000-0000-000000000000',
					status: 'Pending',
					entry: undefined,
				};

				const processedTask = {
					description: 'This is my description',
					entry: '',
					project: '00000000-0000-0000-0000-000000000000',
					projectUuid: '00000000-0000-0000-0000-000000000000',
					status: 'Pending',
					tags: [1, 12, 7, 4],
				};

				const expectResult = processTaskEdit(task);
				expect(expectResult).toStrictEqual(processedTask);
			});
		});

		describe('Testing for processTagskEdit method', () => {
			// eslint-disable-next-line max-len
			test('processTagsEdit should return an empty array when the input is an empty array', () => {
				const data = [];
				const expectResult = processTagsEdit(data);
				expect(expectResult).toStrictEqual([]);
			});

			// eslint-disable-next-line max-len
			test("processTagsEdit should return an array with tag's id when there is not an empty array", () => {
				const data = [
					{
						tag: {
							id: 1,
							name: 'tag1',
						},
					},
					{
						tag: {
							id: 12,
							name: 'tag13',
						},
					},
					{
						tag: {
							id: 7,
							name: 'tag21',
						},
					},
					{
						tag: {
							id: 4,
							name: 'tag5',
						},
					},
				];
				const result = [1, 12, 7, 4];
				const expectResult = processTagsEdit(data);
				expect(expectResult).toStrictEqual(result);
			});
		});
	});

	describe('Testing proccesSearch', () => {
		test('processSearch should return the same object if there are any empty field', () => {
			const search = {
				description: 'new',
				priority: 'Pending',
				project: idRoot,
				tags: ['1'],
				entry: 'any',
				status: ['N!Deleted', 'Pending'],
			};
			const expectResult = processSearch(search);
			expect(expectResult).toStrictEqual(search);
		});

		test('procesSearch should return an object without field if there is empty field', () => {
			const search = {
				description: 'new',
				priority: 'Pending',
				project: idRoot,
				tags: ['1'],
				entry: 'any',
				status: ['N!Deleted', ''],
			};
			const result = {
				description: 'new',
				priority: 'Pending',
				project: idRoot,
				tags: ['1'],
				entry: 'any',
				status: ['N!Deleted'],
			};
			const expectResult = processSearch(search);
			expect(expectResult).toStrictEqual(result);
		});
	});
	describe('Testing proccesOptions', () => {
		test('proccesOptionsObjects should return processed list of options', () => {
			const tags = [
				{ id: 1, name: 'today' },
				{ id: 2, name: 'teach' },
			];
			const result = [
				{ key: 1, text: '=today', value: 1 },
				{ key: 2, text: '=teach', value: 2 },
				{ key: 'N!1', text: '!=today', value: 'N!1' },
				{ key: 'N!2', text: '!=teach', value: 'N!2' },
			];
			const expectResult = processOptionsObjects(tags, 'id');
			expect(expectResult).toStrictEqual(result);
		});

		test('procesOptionsStrings should return processed list of options', () => {
			const result = [
				{ key: 'High', text: '=High', value: 'High' },
				{ key: 'Medium', text: '=Medium', value: 'Medium' },
				{ key: 'Low', text: '=Low', value: 'Low' },
				{ key: 'N!High', text: '!=High', value: 'N!High' },
				{ key: 'N!Medium', text: '!=Medium', value: 'N!Medium' },
				{ key: 'N!Low', text: '!=Low', value: 'N!Low' },
			];
			const expectResult = processOptionsStrings(priorities);
			expect(expectResult).toStrictEqual(result);
		});
	});
});
