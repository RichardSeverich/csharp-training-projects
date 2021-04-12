import React from 'react';
import { Card, Button, Icon, Container } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import './index.css';

/**
 * @returns {React.Component} Card with a confirmation message
 */
function ConfirmationSave() {
	return (
		<Container>
			<Card className="confirmation">
				<Icon className="confirmation-icon" name="thumbs up outline" size="massive" />
				<Card.Content>
					<Card.Header>Operation completed successfully</Card.Header>
					<Card.Description>
						Do you want to save a task or return to task list?
					</Card.Description>
				</Card.Content>
				<Card.Content>
					<div className="buttons-container">
						<Button content="Save a task" to={'/tasks/add-task'} as={Link} />
						<Button content="Return to list" to={'/view-tasks'} as={Link} />
					</div>
				</Card.Content>
			</Card>
		</Container>
	);
}

export default ConfirmationSave;
