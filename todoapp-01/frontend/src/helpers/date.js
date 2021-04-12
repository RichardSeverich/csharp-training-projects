/**
 * @param {string} date A date to convert in custom format dd/mm/yyyy.
 * @returns {string} Date converted in custom format dd/mm/yyyy.
 */
export function processDate(date) {
	let processedDate = date.substring(0, 10);
	processedDate = processedDate.split('-');
	processedDate = processedDate.reverse();

	return processedDate.join('/');
}
