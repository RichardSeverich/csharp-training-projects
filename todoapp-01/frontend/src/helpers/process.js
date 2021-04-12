import { idRoot } from './constants';

/**
 * @param {object} task a task.
 * @returns {object} processed task.
 */
export function processTaskEdit(task) {
	let tags = processTagsEdit(task.tags);
	let project = task.projectUuid;
	let newTask = { ...task, tags, project };
	for (let prop in newTask) {
		newTask[prop] = !newTask[prop] ? '' : newTask[prop];
	}
	return newTask;
}

/**
 * @param {object} task a task.
 * @returns {object} processed task.
 */
export default function processTask(task) {
	const projectUuid = task.projectUuid ? task.projectUuid : idRoot;
	let tags = processTags(task.tags);
	let newTask = { ...task, tags, projectUuid };
	return newTask;
}

/**
 * @param {object[]} data list of the tags.
 * @returns {object[]} new list tags.
 */
export function processTagsEdit(data) {
	let newTags = data.map((tag) => {
		return tag.tag.id;
	});
	return newTags;
}

/**
 * @param {object[]} data list of the tags.
 * @returns {object[]} new list tags.
 */
export function processTags(data) {
	let newTags = data.map((tag) => {
		return { Id_Tag: tag };
	});
	return newTags;
}

/**
 * @param {object} search search values.
 * @returns {object} search values without innecesary fields.
 */
export function processSearch(search) {
	let searchValue = { entry: search.entry };
	if (search.description) {
		searchValue.description = search.description;
	}

	if (search.project) {
		searchValue.project = search.project;
	}

	if (search.priority) {
		searchValue.priority = search.priority;
	}

	if (search.status[1] === '') {
		searchValue.status = [search.status[0]];
	} else {
		searchValue.status = search.status;
	}

	if (search.tags > 0) {
		searchValue.tags = search.tags;
	}

	return searchValue;
}

/**
 * @param {object[]} data list of values.
 * @param {string} attribute primary key.
 * @returns {object[]} list of values processed for select.
 */
export function processOptionsObjects(data, attribute) {
	const positiveOptions = processPositiveOptionsObjects(data, attribute);
	const negativeOptions = processNegativeOptionsObjects(data, attribute);

	return [...positiveOptions, ...negativeOptions];
}

/**
 * @param {object[]} data list of values.
 * @param {string} attribute primary key.
 * @returns {object[]} list of values processed, positive items.
 */
export function processPositiveOptionsObjects(data, attribute) {
	const processedOptions = data.map((option) => {
		return { key: option[attribute], text: '=' + option.name, value: option[attribute] };
	});

	return processedOptions;
}

/**
 * @param {object[]} data list of values.
 * @param {string} attribute primary key.
 * @returns {object[]} list of values processed, negative items.
 */
export function processNegativeOptionsObjects(data, attribute) {
	const processedOptions = data.map((option) => {
		return {
			key: 'N!' + option[attribute],
			text: '!=' + option.name,
			value: 'N!' + option[attribute],
		};
	});

	return processedOptions;
}

/**
 * @param {string[]} data list of values.
 * @returns {object[]} list of values processed for select.
 */
export function processOptionsStrings(data) {
	const positiveOptions = processPositiveOptionsStrings(data);
	const negativeOptions = processNegativeOptionsStrings(data);

	return [...positiveOptions, ...negativeOptions];
}

/**
 * @param {string[]} data list of values.
 * @returns {object[]} list of values processed, positive items.
 */
export function processPositiveOptionsStrings(data) {
	const processedOptions = data.map((option) => {
		return { key: option, text: '=' + option, value: option };
	});

	return processedOptions;
}

/**
 * @param {string[]} data list of values.
 * @returns {object[]} list of values processed, negative items.
 */
export function processNegativeOptionsStrings(data) {
	const processedOptions = data.map((option) => {
		return { key: 'N!' + option, text: '!=' + option, value: 'N!' + option };
	});

	return processedOptions;
}
