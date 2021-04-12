import { merge, mergeSort, ProjectsSorter, TagsSorter } from '../sort';

describe('helpers/sort', () => {
	describe('Testing methods for sort Projects', () => {
		const unsortedList = [
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
				name: 'Maths',
				parent: '00000000-0000-0000-0000-000000000000',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
				name: 'Natural Sciences',
				parent: '00000000-0000-0000-0000-000000000000',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
				name: 'Sum',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
				name: 'Subtraction',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8296',
				name: 'Terrestrial Biology',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8297',
				name: 'Marine Biology',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
			},
		];

		const sortedList = [
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8297',
				name: 'Marine Biology',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
				name: 'Maths',
				parent: '00000000-0000-0000-0000-000000000000',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
				name: 'Natural Sciences',
				parent: '00000000-0000-0000-0000-000000000000',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
				name: 'Subtraction',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
				name: 'Sum',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
			},
			{
				uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8296',
				name: 'Terrestrial Biology',
				parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
			},
		];

		describe('Testing methods recursives for sort', () => {
			describe('Testing method merge by especified attribute', () => {
				test('Testing merge method 1.', () => {
					const leftList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
							name: 'Subtraction',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8296',
							name: 'Terrestrial Biology',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
						},
					];

					const rightList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
							name: 'Sum',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
					];

					const sortedList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
							name: 'Subtraction',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
							name: 'Sum',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8296',
							name: 'Terrestrial Biology',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
						},
					];

					const expectedSortedList = merge('name', leftList, rightList);

					expect(expectedSortedList).toStrictEqual(sortedList);
				});

				test('Testing merge method 2.', () => {
					const leftList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
							name: 'Natural Sciences',
							parent: '00000000-0000-0000-0000-000000000000',
						},
					];

					const rightList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
							name: 'Subtraction',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
					];

					const sortedList = [
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
							name: 'Natural Sciences',
							parent: '00000000-0000-0000-0000-000000000000',
						},
						{
							uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
							name: 'Subtraction',
							parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						},
					];

					const expectedSortedList = merge('name', leftList, rightList);

					expect(expectedSortedList).toStrictEqual(sortedList);
				});
			});

			describe('Testing method mergeSorted integration with merge method', () => {
				test('Testing mergeSort method 1.', () => {
					const sortedListAsc = sortedList;
					const expectedSortedList = mergeSort('name', unsortedList);

					expect(expectedSortedList).toStrictEqual(sortedListAsc);
				});
			});
		});

		describe('Testing sort ascendent', () => {
			test('ProjectsSorter case "asc"', () => {
				const sortedListAsc = sortedList;
				const expectedSortedList = ProjectsSorter('asc', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListAsc);
			});
		});

		describe('Testing sort descendent', () => {
			test('ProjectsSorter case "desc"', () => {
				const sortedListDesc = [
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8296',
						name: 'Terrestrial Biology',
						parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
					},
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
						name: 'Sum',
						parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
					},
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8295',
						name: 'Subtraction',
						parent: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
					},
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
						name: 'Natural Sciences',
						parent: '00000000-0000-0000-0000-000000000000',
					},
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8291',
						name: 'Maths',
						parent: '00000000-0000-0000-0000-000000000000',
					},
					{
						uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8297',
						name: 'Marine Biology',
						parent: '494c7fbc-0fde-4230-a15c-d5bb903f8293',
					},
				];

				const expectedSortedList = ProjectsSorter('desc', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListDesc);
			});
		});

		test('Testing when there is no supported type', () => {
			const expectedSortedList = ProjectsSorter('cat', unsortedList);

			expect(expectedSortedList).toStrictEqual(unsortedList);
		});
	});

	describe('Testing methods for sort Tags', () => {
		const unsortedList = [
			{
				name: 'Sum',
				count: 0,
			},
			{
				name: 'MyTag',
				count: 7,
			},
			{
				name: 'Today',
				count: 1,
			},
			{
				name: 'Important',
				count: 2,
			},
		];

		const sortedListN = [
			{
				name: 'Important',
				count: 2,
			},
			{
				name: 'MyTag',
				count: 7,
			},
			{
				name: 'Sum',
				count: 0,
			},
			{
				name: 'Today',
				count: 1,
			},
		];

		const sortedListC = [
			{
				name: 'Sum',
				count: 0,
			},
			{
				name: 'Today',
				count: 1,
			},
			{
				name: 'Important',
				count: 2,
			},
			{
				name: 'MyTag',
				count: 7,
			},
		];

		describe('Testing methods recursives for sort', () => {
			describe('Testing method merge by especified attribute', () => {
				test('Testing merge method 1.', () => {
					const leftList = [
						{
							name: 'Important',
							count: 2,
						},
						{
							name: 'Today',
							count: 1,
						},
					];

					const rightList = [
						{
							name: 'MyTag',
							count: 7,
						},
					];

					const sortedList = [
						{
							name: 'Important',
							count: 2,
						},
						{
							name: 'MyTag',
							count: 7,
						},
						{
							name: 'Today',
							count: 1,
						},
					];

					const expectedSortedList = merge('name', leftList, rightList);

					expect(expectedSortedList).toStrictEqual(sortedList);
				});

				test('Testing merge method 2.', () => {
					const leftList = [
						{
							name: 'Today',
							count: 1,
						},
						{
							name: 'MyTag',
							count: 7,
						},
					];

					const rightList = [
						{
							name: 'Important',
							count: 2,
						},
					];

					const sortedList = [
						{
							name: 'Today',
							count: 1,
						},
						{
							name: 'Important',
							count: 2,
						},
						{
							name: 'MyTag',
							count: 7,
						},
					];

					const expectedSortedList = merge('count', leftList, rightList);

					expect(expectedSortedList).toStrictEqual(sortedList);
				});
			});

			describe('Testing method mergeSorted integration with merge method', () => {
				test('Testing mergeSort method by name.', () => {
					const expectedSortedList = mergeSort('name', unsortedList);

					expect(expectedSortedList).toStrictEqual(sortedListN);
				});

				test('Testing mergeSort method by count.', () => {
					const expectedSortedList = mergeSort('count', unsortedList);

					expect(expectedSortedList).toStrictEqual(sortedListC);
				});
			});
		});

		describe('Testing sort ascendent', () => {
			test('TagsSorter case name "asc name"', () => {
				const sortedListNAsc = sortedListN;
				const expectedSortedList = TagsSorter('asc name', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListNAsc);
			});

			test('TagsSorter case count "asc count"', () => {
				const sortedListCAsc = sortedListC;
				const expectedSortedList = TagsSorter('asc count', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListCAsc);
			});
		});

		describe('Testing sort descendent', () => {
			test('TagsSorter case "desc name"', () => {
				const sortedListNDesc = [
					{
						name: 'Today',
						count: 1,
					},
					{
						name: 'Sum',
						count: 0,
					},
					{
						name: 'MyTag',
						count: 7,
					},
					{
						name: 'Important',
						count: 2,
					},
				];

				const expectedSortedList = TagsSorter('desc name', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListNDesc);
			});

			test('TagsSorter case "desc count"', () => {
				const sortedListCDesc = [
					{
						name: 'MyTag',
						count: 7,
					},
					{
						name: 'Important',
						count: 2,
					},
					{
						name: 'Today',
						count: 1,
					},
					{
						name: 'Sum',
						count: 0,
					},
				];

				const expectedSortedList = TagsSorter('desc count', unsortedList);

				expect(expectedSortedList).toStrictEqual(sortedListCDesc);
			});
		});

		test('Testing when there is no supported type', () => {
			const expectedSortedList = TagsSorter('cat', unsortedList);

			expect(expectedSortedList).toStrictEqual(unsortedList);
		});
	});
});
