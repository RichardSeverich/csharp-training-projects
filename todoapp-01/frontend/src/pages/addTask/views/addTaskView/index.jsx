import React, { useState } from 'react';
import { useDataProvider } from 'context/tasks/tasks';
import { Button, Container, Divider, Segment } from 'semantic-ui-react';
import NewTask from './../../components/task_form';
import { emptyTask } from 'helpers/constants';
import { useHistory } from 'react-router-dom';

function AddTaskView(props) {
	const [, setOpen] = useState(false);
	const [, actions] = useDataProvider();
	let history = useHistory();

	/**
	 * Redirects to view tasks page
	 */
	function handleClick() {
		history.push('/view-tasks');
	}

	return (
		<div>
			<Container>
				<h1>Add a Task</h1>
				<Segment>
					<NewTask
						open={setOpen}
						type={props.type}
						value={(props.type === 'edit' && props.value) || emptyTask}
						onSubmit={actions.OnTaskAdd}
						onEdit={props.onEdit}
					/>
					<Divider horizontal />
					<Button content="Cancel" onClick={handleClick} negative />
					<Button form="new-task" content="Submit" positive />
				</Segment>
			</Container>
		</div>
	);
}

export default AddTaskView;
