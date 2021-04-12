import React, { useState } from 'react';
import { Accordion, Icon } from 'semantic-ui-react';
import TaskList from 'components/task_list';

import './index.css';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} A list of tasks
 */
function ListContainer(props) {
	const { tasks, title } = props;
	const [state, setState] = useState(true);

	return (
		<>
			<Accordion.Title active={state} onClick={() => setState(!state)}>
				<Icon name="dropdown" />
				{title}
			</Accordion.Title>
			<Accordion.Content active={state}>
				<TaskList tasks={tasks} />
			</Accordion.Content>
		</>
	);
}

export default ListContainer;
