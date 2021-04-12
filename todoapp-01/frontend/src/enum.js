const namespace = 'tasks';

export const ActionTypes = {
	LoadingChange: `${namespace}.loading.change`,
	TaskAdd: `${namespace}.task.add`,
	TaskAddSuccess: `${namespace}.task.add.success`,
	TaskAddError: `${namespace}.task.add.error`,
	TaskChange: `${namespace}.task.change`,
	TaskDelete: `${namespace}.task.delete`,
	TaskStatusChange: `${namespace}.task.change.status`,
	TaskLoad: `${namespace}.task.load`,
	TaskLoadSuccess: `${namespace}.task.load.success`,
	PageChanged: `${namespace}.task.page.change`,
	SearchChanged: `${namespace}.task.search.change`,
};
