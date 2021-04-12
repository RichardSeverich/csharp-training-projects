import React from 'react';
import './index.css';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} Return icon of priority
 */
function PriorityIcon(props) {
	let priority = props.priority;

	if (priority !== 'Medium' && priority !== 'High' && priority !== 'Low') {
		priority = 'Medium';
	}
	let priorityClass = 'priority priority-' + priority;

	return (
		<div className={priorityClass}>
			<h2>{priority[0]}</h2>
		</div>
	);
}

export default PriorityIcon;
