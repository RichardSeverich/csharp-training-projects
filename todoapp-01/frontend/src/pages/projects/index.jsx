import React from 'react';

import Header from 'components/header';
import MenuPages from 'components/menu';
import ProjectsView from './views/main_view';

/**
 * @returns {object} Return page of projects available.
 */
function Projects(props) {
	return (
		<div>
			<Header />
			<MenuPages />
			<ProjectsView />
		</div>
	);
}

export default Projects;
