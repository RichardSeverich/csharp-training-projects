import { idRoot } from './constants';

/**
 * @param {string} type Type of filter data.
 * @param {object[]} data Data will be filtered.
 * @returns {object[]} Filtered data.
 */
export default function filter(type, data) {
	switch (type) {
		case 'status':
			return filterStatus(data);
		default:
			return {};
	}
}

/**
 * @param {object[]} data Data will be filtered by status.
 * @returns {object[][]} Data filtered by status.
 */
function filterStatus(data) {
	const pending = data.filter((item) => item.status === 'Pending');
	const inProgress = data.filter((item) => item.status === 'In Progress');
	const completed = data.filter((item) => item.status === 'Completed');

	return [
		{
			status: 'Pending',
			tasks: [...pending],
		},
		{
			status: 'In Progress',
			tasks: [...inProgress],
		},
		{
			status: 'Completed',
			tasks: [...completed],
		},
	];
}

/**
 * @param {object} project a project.
 * @param {object[]} data list projects.
 * @returns {object[]} list of the projects.
 */
export function filterProjects(project, data) {
	const filteredData = data.filter((p) => p.parent === project.uuid && p.uuid !== idRoot);
	return filteredData;
}
