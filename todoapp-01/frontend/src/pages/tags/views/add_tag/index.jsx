import React, { useState } from 'react';
import { Button, Container, Divider, Segment, Input, Form } from 'semantic-ui-react';
import { Link, useHistory } from 'react-router-dom';
import { useTagProvider } from '../../context/tags';
import Header from 'components/header';
import MenuPages from 'components/menu';

/**
 * @returns {object} Page to add tags
 */
function AddTag() {
	const [, actions] = useTagProvider();
	const [tag, setTag] = useState('');
	let history = useHistory();

	const handleOnChange = (e, { value }) => {
		setTag(value);
	};

	const handleSubmit = () => {
		actions.OnTagAdd(tag);
		history.push('/tags');
	};

	return (
		<>
			<Header />
			<MenuPages />
			<Container>
				<h1>Add Tag</h1>
				<Segment>
					<Form id="tag-form" onSubmit={handleSubmit}>
						<Input
							icon="tags"
							iconPosition="left"
							label={{ tag: true, content: 'Name Tag' }}
							labelPosition="right"
							placeholder="Enter tags"
							value={tag}
							onChange={handleOnChange}
						/>
					</Form>
					<Divider horizontal />
					<Button content="Cancel" to={'/tags'} as={Link} negative />
					<Button form="tag-form" content="Submit" positive />
				</Segment>
			</Container>
		</>
	);
}

export default AddTag;
