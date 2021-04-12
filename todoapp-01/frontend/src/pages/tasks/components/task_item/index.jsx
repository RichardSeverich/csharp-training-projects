/* eslint-disable max-len */
import React, { useState } from 'react';
import { Card, Button, Icon, Label } from 'semantic-ui-react';
import { processDate } from 'helpers/date';
import TaskView from '../../views/task_modal';
import { useAppProvider } from 'context/application/app';
import './index.css';


/**
 * @param {object} props - props sent to fill data
 * @returns {object} Return a card of a task
 */
function TaskItem(props) {
	const [, actions] = useAppProvider();
	const [open, setOpen] = useState(false);
	const { value, onDelete, onEdit, onStatusChange } = props;

	const date = value.entry.includes('-') ? processDate(value.entry) : value.entry;
	const due = value.due ? processDate(value.due) : 'No due date';
	let nextStatus;
	if (value.status === 'Pending') {
		nextStatus = 'In Progress';
	} else if (value.status === 'In Progress') {
		nextStatus = 'Completed';
	} else {
		nextStatus = 'Completed';
	}
	let priority = value.priority;

	/**
	 * handleClose, set state to false, to close the modal.
	 */
	function handleClose() {
		setOpen(false);
	}

	let tagsLabel = value.tags.length !== 0 ? value.tags.map((t) => {
		return(
			<Label key={t.id_Tag}>
				{t.tag.name}
			</Label>
		)
	}) : 'No tags present'
	return (
		<Card id={value.id} className={priority} onDoubleClick={() => setOpen(true)}>
			<Card.Content>
				<Card.Meta textAlign="right">
					<Icon name="calendar plus" /> Due: {due}
				</Card.Meta>
				<Card.Header>{value.description}</Card.Header>
				<Card.Description>Task {value.status}</Card.Description>
				<Card.Meta textAlign="left">
					<Icon name="calendar plus" /> Created on: {date}
				</Card.Meta>
				<br></br>
				<Card.Meta>{tagsLabel}</Card.Meta>
			</Card.Content>
			<Card.Content extra>
				<div className={value.status !== 'Completed' ? 'ui three buttons' : 'ui two buttons'}>
					<TaskView value={value} onEdit={onEdit} open={open} closeModal={() => handleClose()} />
					{value.status !== 'Completed' && (
						<Button
							title={nextStatus === 'In Progress' ? 'Begin task' : 'Complete task'}
							inverted
							color="green"
							onClick={() => {
								onStatusChange(value.uuid, nextStatus);
								actions.refresh();
							}}
						>
							<Icon name="check circle" />
						</Button>
					)}
					<Button
						title="Delete task"
						inverted color="red"
						onClick={() => {
							onDelete(value.uuid);
							actions.refresh();
						}}
					>
						<Icon name="trash" />
					</Button>
				</div>
			</Card.Content>
		</Card>
	);
}

export default TaskItem;
