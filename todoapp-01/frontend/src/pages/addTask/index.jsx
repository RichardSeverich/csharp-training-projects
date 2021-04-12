import React from 'react';
import Header from 'components/header';
import MenuPages from 'components/menu';
import TaskFactoryRoutes from './taskFactoryRoutes';

/**
 * @param {object} props received properties
 * @returns {object} Page to add tasks
 */
function AddTask(props) {
	return (
		<div>
			<Header />
			<MenuPages />
			<TaskFactoryRoutes {...props} />
		</div>
	);
}

export default AddTask;
