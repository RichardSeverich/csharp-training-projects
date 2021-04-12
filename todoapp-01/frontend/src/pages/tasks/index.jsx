import React, { useState, useEffect } from 'react';
import { Form, Button, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import './index.css';
import TasksView from './views/task';
import Header from 'components/header';
import MenuPages from 'components/menu';
import { useDataProvider } from 'context/tasks/tasks';
import { useAppProvider } from 'context/application/app';
import Pagination from './components/pagination';
import TaskFilter from './components/task_filter';
import { processSearch } from 'helpers/process';

const options = [
	{ key: 'a', text: 'Show all', value: 'all' },
	{ key: 's', text: 'Show by Status', value: 'status' },
];

/**
 * @returns {object} Returns page of tasks.
 */
function Tasks(props) {
	const [, actions] = useDataProvider();
	const [stateApp] = useAppProvider();
	const [state] = useDataProvider();
	const [show, setShow] = useState('all');

	useEffect(() => {
		const searchValue = processSearch(state.search);
		const payload = {
			pageNumber: state.currentPage,
			pageSize: state.pageSize,
			...searchValue, 
		};
		actions.OnLoad(payload);
	}, [stateApp, state.pageSize, state.currentPage, state.search])

	const handleChange = (e, { value }) => {
		setShow(value);
	};

	return (
		<div className="main">
			<Header />
			<MenuPages />
			<TaskFilter search={state.search} />
			<Button icon to={'/tasks/add-task'} as={Link} labelPosition="left" color="grey">
				<Icon name="plus" />
				Add Task
			</Button>
			<Form>
				<Form.Select
					options={options}
					placeholder="Show by..."
					name="show"
					value={show}
					onChange={handleChange}
				/>
			</Form>
			<TasksView show={show} />
			<Pagination currentPage={state.currentPage} pageSize={state.pageSize} totalPages={state.totalPages} />
		</div>
	);
}

export default Tasks;
