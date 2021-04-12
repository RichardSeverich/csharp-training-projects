import React, { useEffect, useState } from 'react';
import { Form, Accordion, Icon } from 'semantic-ui-react';

import { priorityOptions } from 'helpers/constants';
import SelectTags from '../select_tags';
import { idRoot } from 'helpers/constants';

/**
 * @param {object} props - props sent to fill data
 * @returns {object} New task or edit task form
 */
function TaskForm(props) {
	const { value, onSubmit, onEdit } = props;
	const [error, setError] = useState(false);
	const [showOptions, setShowOptions] = useState(false);
	const [state, setState] = useState(value);


	useEffect(() => {
		const project = value.project === idRoot ? '' : value.project;
		const due = value.due ? value.due.substring(0, 16) : '';
		setState({ ...state, project, due });
	}, []);

	const handleOnChange = (key, value) => {
		setState({ ...state, [key]: value });
	};

	const handleSubmit = () => {
		if (state.description !== '') {
			const newTask = {
				...state,
			};
		
			if (props.type !== 'edit') {
				onSubmit(newTask);
			} else {
				onEdit(value.id, { ...state, entry: value.entry });
			}
			props.open(false);
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
						<Form.Input
							label="Project:"
							placeholder="Subdirectories ex: daily.homework"
							name="project"
							value={state.project}
							onChange={(e, { value }) => handleOnChange('project', value)}
						/>
						<Form.Field>
							<label>Tags:</label>
							<SelectTags tags={state.tags} onChange={handleOnChange} />
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
						<Form.Input type="submit">Submit</Form.Input>
					</Accordion.Content>
				</Accordion>
			</Form>
		</>
	);
}

export default TaskForm;
