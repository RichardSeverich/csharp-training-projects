import { Route, Switch, BrowserRouter } from 'react-router-dom';
import React from 'react';

import AddTaskView from './views/addTaskView';
import EditTaskView from './views/editTaskView';
import { processTaskEdit } from 'helpers/process';

/**
 *
 * @param {object} props properties
 * @returns {React.Component} Factory routes for the add task page
 */
function TaskFactoryRoutes(props) {
	let value = props.location.value;
	let newValue = value ? processTaskEdit(value) : null;
	return (
		//<BrowserRouter basename="/tasks">
		<Switch>
			<Route
				exact
				path="/tasks/:id/edit-task"
				render={(props) => (
					<EditTaskView {...props} title="Edit a Task" type="edit" value={newValue} />
				)}
			/>
			<Route
				strict
				path="/tasks/add-task"
				render={(props) =>(
					<AddTaskView {...props} title="Add a Task" type='add' />
				)}
			/>
		</Switch>
		//</BrowserRouter>
	);
}

export default TaskFactoryRoutes;
