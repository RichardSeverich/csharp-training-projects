import React, { useState } from 'react';
import { Menu } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

import { HOME_PAGE } from 'helpers/constants';

/**
 *
 * @returns {object} Menu of nav - Navbar
 */
function MenuPages() {
	const PAGES = [
		{ name: 'Home', link: '/' },
		{ name: 'Projects', link: '/projects' },
		{ name: 'Tasks', link: '/view-tasks' },
		{ name: 'Tags', link: '/tags' },
	];
	const [currentPage, setPage] = useState(HOME_PAGE);

	let menuItems = PAGES.map((item) => (
		<Menu.Item
			key={item.name}
			name={item.name}
			active={currentPage === item.name}
			content={item.name}
			onClick={() => setPage(item.name)}
			as={Link}
			to={item.link}
		/>
	));

	return (
		<>
			<Menu pointing>{menuItems}</Menu>
		</>
	);
}

export default MenuPages;
