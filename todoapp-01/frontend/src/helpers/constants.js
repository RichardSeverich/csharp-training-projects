export const HOME_PAGE = 'tasks';

export const statusValues = ['Pending', 'In Progress', 'Completed'];
export const priorities = ['High', 'Medium', 'Low'];

export const statusOptions = [
	{ key: 'h', text: 'Pending', value: 'Pending' },
	{ key: 'm', text: 'In Progress', value: 'In Progress' },
	{ key: 'l', text: 'Completed', value: 'Completed' },
];

export const priorityOptions = [
	{ key: 'h', text: 'High', value: 'High' },
	{ key: 'm', text: 'Medium', value: 'Medium' },
	{ key: 'l', text: 'Low', value: 'Low' },
];

export const idRoot = '00000000-0000-0000-0000-000000000000';

export const emptyTask = {
	description: '',
	depends: '',
	due: '',
	projectUuid: '00000000-0000-0000-0000-000000000000',
	tags: [],
	priority: 'Medium',
	status: 'Pending',
};
