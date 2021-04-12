import React, { useState, useEffect } from 'react';
import Header from 'components/header';
import MenuPages from 'components/menu';
import TagView from './views/main_view';
import { Form, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useAppProvider } from 'context/application/app';
import { useTagProvider } from './context/tags';

const options = [
	{ key: 'asc n', text: 'Order by name (ascendent)', value: 'asc name' },
	{ key: 'desc n', text: 'Order by name (descendent)', value: 'desc name' },
	{ key: 'asc c', text: 'Order by number of tasks (ascendent)', value: 'asc count' },
	{ key: 'desc c', text: 'Order by number of tasks (descendent)', value: 'desc count' },
];
/**
 * @returns {object} Return page of tags available.
 */
function Tags(props) {
	const [state] = useAppProvider();
	const [, actions] = useTagProvider();
	const [show, setShow] = useState('asc name');

	useEffect(() => {
		actions.OnTagLoad();
	}, [state]);

	const handleChange = (e, { value }) => {
		setShow(value);
	};

	return (
		<div>
			<Header/>
			<MenuPages/>
				<h2>Tags available</h2>
				<Button to={'/tags/form'} as={Link}>Add Tag</Button>
				<Form>
					<Form.Select
						options={options}
						placeholder="Show or order..."
						name="show"
						value={show}
						onChange={handleChange}
					/>
				</Form>
				<TagView show={show}/>
		</div>
	);
}

export default Tags;
