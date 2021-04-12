import React from 'react';
import { useLocation } from 'react-router-dom';
import './App.css';
import FactoryRoutes from 'pages/factoryRoutes';
import { TagProvider } from './pages/tags/context/tags';
import { ProjectProvider } from './pages/projects/context/projects';
import { TaskProvider } from 'context/tasks/tasks';
import { useAppProvider } from './context/application/app';

/**
 * @param {object} props properties.
 * @returns {HTMLElement} Basic Template.
 */
function App(props) {
	const [state] = useAppProvider();
	const { pathname } = useLocation();
	const { loggedIn } = state.session;
	return (
		<ProjectProvider>
			<TaskProvider {...props}>
				<TagProvider {...props}>
					<FactoryRoutes pathname={pathname} loggedIn={loggedIn} />
				</TagProvider>
			</TaskProvider>
		</ProjectProvider>
	);
}

export default App;
