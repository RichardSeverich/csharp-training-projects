import React from 'react';

import Header from 'components/header';
import MenuPages from 'components/menu';
import { Container } from 'semantic-ui-react';

/**
 * @returns {object} Page unknown.
 */
function Unknown() {
	return (
		<div>
			<Header />
			<MenuPages />
			<Container>
				<h2>404 Page Not Found</h2>
			</Container>
		</div>
	);
}

export default Unknown;
