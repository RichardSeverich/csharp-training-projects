import React from 'react';
import { Accordion, Segment } from 'semantic-ui-react';
import './index.css';
import ListContainer from '../../components/list_container';

import { useDataProvider } from 'context/tasks/tasks';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} show list of the tasks
 */
function TasksView(props) {
	const { show } = props;
	const [state] = useDataProvider();
	const { data } = state;
	let tasksList;
	if (show === 'all') {
		const { all } = data;
		tasksList = all.length ? (
			<ListContainer key="all" title="All tasks" tasks={all.filter((t) => t.status !== 'Deleted')} />
		) : (
			<h3>There is nothing here.</h3>
		);
	} else {
		const { byStatus } = data;
		tasksList = byStatus.length ? (
			byStatus.map((tasks) => (
				<ListContainer
					key={tasks.status}
					tasks={tasks.tasks.filter((t) => t.status !== 'Deleted')}
					title={tasks.status}
				/>
			))
		) : (
			<h3>There is nothing.</h3>
		);
	}

	return (
		<Segment loading={state.loading}>
			<Accordion styled>{tasksList}</Accordion>
		</Segment>
	);
}

export default TasksView;
