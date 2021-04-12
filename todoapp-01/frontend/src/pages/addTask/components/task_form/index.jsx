import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { Form, Accordion, Icon } from 'semantic-ui-react';
import { priorityOptions, idRoot } from 'helpers/constants';
import SelectTags from '../select_tags';
import SelectProject from '../select_project';
import { useAppProvider } from 'context/application/app';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} New task or edit task form
 */
function TaskForm(props) {
	const [, actions] = useAppProvider();
	const { value, onSubmit, onEdit } = props;
	const [error, setError] = useState(false);
	const [showOptions, setShowOptions] = useState(false);
	const [state, setState] = useState(value);

	useEffect(() => {
		const projectUuid = value.projectUuid === idRoot ? '' : value.projectUuid;
		const due = value.due ? value.due.substring(0, 16) : '';
		setState({ ...state, projectUuid, due });
	}, []);

	const handleOnChange = (key, value) => {
		setState({ ...state, [key]: value });
	};

	const history = useHistory();
	const handleSubmit = () => {
		if (state.description !== '') {
			const newTask = {
				...state,
			};
			if (props.type !== 'edit') {
				onSubmit(newTask);
				actions.refresh();
				history.push('/tasks/save-success');
			} else {
				onEdit([state.uuid, { ...state }]);
				actions.refresh();
				history.push('/tasks/save-success');
			}
		} else {
			setState({ ...state, depends: '' });
			setError(true);
		}
	};

	return (
		<>
			<Form id="new-task" onSubmit={handleSubmit}>
				<Form.Input
					error={error && { content: 'Please enter a description', pointing: 'below' }}
					label="Description:"
					placeholder="description"
					name="description"
					value={state.description}
					onChange={(e, { value }) => {
						setError(false);
						handleOnChange('description', value);
					}}
				/>
				<Accordion styled>
					<Accordion.Title active={showOptions} onClick={() => setShowOptions(!showOptions)}>
						<Icon name="dropdown" />
						See advanced options
					</Accordion.Title>
					<Accordion.Content active={showOptions}>
						<Form.Field>
							<label>Tags:</label>
							<SelectTags tags={state.tags} onChange={handleOnChange} />
						</Form.Field>
						<Form.Field>
							<label>Project:</label>
							<SelectProject project={state.projectUuid} onChange={handleOnChange} />
						</Form.Field>
						<Form.Select
							label="Priority:"
							options={priorityOptions}
							placeholder="Priority"
							name="priority"
							value={state.priority}
							onChange={(e, { value }) => handleOnChange('priority', value)}
						/>
						<Form.Input
							label="Due:"
							name="due"
							type="datetime-local"
							value={state.due}
							onChange={(e, { value }) => handleOnChange('due', value)}
						/>
					</Accordion.Content>
				</Accordion>
			</Form>
		</>
	);
}

export default TaskForm;
