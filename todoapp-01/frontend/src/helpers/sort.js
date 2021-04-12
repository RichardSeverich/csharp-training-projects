/**
 * @param {string} type type for action.
 * @param {object[]} data project list to sort.
 * @returns {object[]} project list sorted.
 */
export function ProjectsSorter(type, data) {
	switch (type) {
		case 'asc':
			return sortAsc('name', data);
		case 'desc':
			return sortDesc('name', data);
		default:
			return data;
	}
}

/**
 * @param {string} type type for action.
 * @param {object[]} data tag list to sort.
 * @returns {object[]} tag list sorted.
 */
export function TagsSorter(type, data) {
	switch (type) {
		case 'asc name':
			return sortAsc('name', data);
		case 'desc name':
			return sortDesc('name', data);
		case 'asc count':
			return sortAsc('count', data);
		case 'desc count':
			return sortDesc('count', data);
		default:
			return data;
	}
}

/**
 * @param {string} attribute name attribute.
 * @param {object[]} data objets list.
 * @returns {object[]} object list sorted.
 */
export function sortAsc(attribute, data) {
	return mergeSort(attribute, data);
}

/**
 * @param {string} attribute name attribute.
 * @param {object[]} data objets list.
 * @returns {object[]} object list sorted.
 */
export function sortDesc(attribute, data) {
	return mergeSort(attribute, data).reverse();
}

/**
 * @param {string} attribute name attribute.
 * @param {object[]} data objets list.
 * @returns {object[]} object list sorted.
 */
export function mergeSort(attribute, data) {
	if (data.length < 2) {
		return data;
	}

	const middle = Math.floor(data.length / 2);
	const subLeft = mergeSort(attribute, data.slice(0, middle));
	const subRight = mergeSort(attribute, data.slice(middle));

	return merge(attribute, subLeft, subRight);
}

/**
 * @param {string} attribute name attribute.
 * @param {object[]} left a object list.
 * @param {object[]} right a object list.
 * @returns {object[]} object list sorted.
 */
export function merge(attribute, left, right) {
	let result = [];

	while (left.length > 0 && right.length > 0) {
		result.push(left[0][attribute] < right[0][attribute] ? left.shift() : right.shift());
	}

	return result.concat(left.length ? left : right);
}
