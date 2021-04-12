import filter, { filterProjects } from '../filter';

describe('helpers/filter', () => {
	describe('Check correct functioning of filter method', () => {
		const tasks = [
			{
				description: 'task1',
				status: 'Pending',
			},
			{
				description: 'task2',
				status: 'In Progress',
			},
			{
				description: 'task3',
				status: 'Completed',
			},
		];
		const arrangedTasks = [
			{
				status: 'Pending',
				tasks: [
					{
						description: 'task1',
						status: 'Pending',
					},
				],
			},
			{
				status: 'In Progress',
				tasks: [
					{
						description: 'task2',
						status: 'In Progress',
					},
				],
			},
			{
				status: 'Completed',
				tasks: [
					{
						description: 'task3',
						status: 'Completed',
					},
				],
			},
		];
		let expected = filter('status', tasks);
		test('Should be working as expected', () => {
			expect(expected).toStrictEqual(arrangedTasks);
		});
		test('Should be of type array', () => {
			expect(expected).toBeInstanceOf(Array);
		});
		test('passing a wrong type should return a empty object', () => {
			let expectedValue = filter('Pending', tasks);
			expect(expectedValue).toStrictEqual({});
		});
	});

	describe('Testing projects filter', () => {
		test('Find all childrens of a project', () => {
			const projects = [
				{
					uuid: '00000000-0000-0000-0000-000000000003',
					name: 'my project',
					parent: '00000000-0000-0000-0000-000000000002',
				},
				{
					uuid: '00000000-0000-0000-0000-000000000002',
					name: 'my project',
					parent: '00000000-0000-0000-0000-000000000000',
				},
				{
					uuid: '00000000-0000-0000-0000-000000000001',
					name: 'my project',
					parent: '00000000-0000-0000-0000-000000000000',
				},
			];

			const filteredProjects = [
				{
					uuid: '00000000-0000-0000-0000-000000000002',
					name: 'my project',
					parent: '00000000-0000-0000-0000-000000000000',
				},
				{
					uuid: '00000000-0000-0000-0000-000000000001',
					name: 'my project',
					parent: '00000000-0000-0000-0000-000000000000',
				},
			];

			const project = {
				uuid: '00000000-0000-0000-0000-000000000000',
				name: 'my project',
			};

			const expectedValue = filterProjects(project, projects);
			expect(expectedValue).toStrictEqual(filteredProjects);
		});
	});
});
