import React from 'react';
import { Header, Container } from 'semantic-ui-react';
import './index.css';

/**
 * @returns {object} Header of the pages.
 */
function header() {
	return (
		<Header className="header-nav" as="h1">
			<Container>
				<a href="/">ToDo</a>
			</Container>
		</Header>
	);
}

export default header;
