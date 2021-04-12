import React from 'react';
import { useDataProvider } from 'context/tasks/tasks';
import EditTask from '../../components/task_form';
import { emptyTask } from 'helpers/constants';
import { Segment, Divider, Button, Container } from 'semantic-ui-react';
import { useHistory } from 'react-router-dom';

function EditTaskView(props){
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
				<h1>Edit a Task</h1>
				<Segment>
					<EditTask
						type={props.type}
						value={(props.type === 'edit' && props.value) || emptyTask}
						onSubmit={actions.OnTaskChange}
						onEdit={actions.OnTaskChange}
					/>
					<Divider horizontal />
					<Button content="Cancel" onClick={handleClick} negative />
					<Button form="new-task" type="Submit" content="Submit" positive />
				</Segment>
			</Container>
		</div>
	);
}

export default EditTaskView;
