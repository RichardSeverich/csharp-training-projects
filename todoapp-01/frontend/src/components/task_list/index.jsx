import React from 'react';
import { Card } from 'semantic-ui-react';
import { useDataProvider } from 'context/tasks/tasks';

import TaskItem from 'pages/tasks/components/task_item';
import './index.css';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} A list of tasks
 */
function TaskList(props) {
	const { tasks } = props;
	const [, actions] = useDataProvider();

	let content = tasks.length ? (
		tasks.map((task, index) => <TaskItem 
				key={index}
				value={{ ...task }}
			onDelete={(id) => actions.OnTaskDelete(id)}
			onEdit={(id, newContent) => actions.OnTaskChange({id, newContent})} 
			onStatusChange={((id, newContent) => actions.OnTaskStatusChange({id, newContent}))}
			/>)
	) : (
		<label>No Tasks</label>
	);

	return <Card.Group className={'task-list'}>{content}</Card.Group>;
}

export default TaskList;
