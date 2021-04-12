/**
 * @param {object} root0 a object.
 * @param {string} root0.id object id.
 * @param {string} root0.newContent object content.
 * @returns {object[]} task status.
 */
export function editStatus({ id, newContent }) {
	return [id, { status: newContent }];
}
