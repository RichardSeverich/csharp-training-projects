import React from 'react';
import { Header, Modal, Button, Divider, Grid, Segment } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { processDate } from 'helpers/date';
import { idRoot } from 'helpers/constants';

/**
 * @param {object} props props sent to fill data
 * @returns {object} Modal of task
 */
function TaskView(props) {
	let open = props.open;
	const { value } = props;
	let tags = '';
	if (value.tags === undefined || value.tags.length === 0) {
		tags = 'No tags';
	} else {
		value.tags.map((t) => (tags += ' ' + t.tag.name + ','));
	}

	const date = value.due ? (value.due.includes('-') ? processDate(value.due) : value.due) : '';
	const start = value.start
		? value.start.includes('-')
			? processDate(value.start)
			: value.start
		: '';
	const end = value.end ? (value.end.includes('-') ? processDate(value.end) : value.end) : '';

	const newTo = {
		pathname: `/tasks/${value.id}/edit-task`,
		value,
	};

	return (
		open ? <Modal
			closeIcon
			open={open}
			onClose={() => {
				props.closeModal();
			}}
			//onOpen={() => setOpen(true)}
		>
			<Header icon="clipboard outline" content={value.description} />
			<Modal.Content>
				<Segment>
					<Grid columns={2} relaxed="very">
						<Grid.Column width={3}>
							<h3>Priority:</h3>
							<h3>Project:</h3>
							<h3>Due date:</h3>
							<h3>Start date:</h3>
							<h3>End date:</h3>
							<h3>Status:</h3>
							<h3>Tags:</h3>
						</Grid.Column>
						<Grid.Column>
							<h3>{value.priority}</h3>
							<h3>{value.projectUuid === idRoot ? 'No projects' : value.projectUuid}</h3>
							<h3>{date ? date : 'There is no a due date for this task'}</h3>
							<h3>{start ? start : 'There is no a start date for this task'}</h3>
							<h3>{end ? end : "This task haven't been finished yet"}</h3>
							<h3>{value.status}</h3>
							<h3>{tags.endsWith(',') ? tags.substring(0, tags.length - 1) : tags}</h3>
						</Grid.Column>
					</Grid>
					<Divider hidden={true} vertical />
				</Segment>
			</Modal.Content>
			<Modal.Actions>
				<Button color="blue">
					<Link to={newTo} style={{ color: 'white' }}>
						Edit
					</Link>
				</Button>
			</Modal.Actions>
		</Modal> : null
	);
}

export default TaskView;
