import React, { useState } from 'react';
import Header from 'components/header';
import MenuPages from 'components/menu';
import { Button, Segment, Confirm } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useAppProvider } from 'context/application/app';


/**
 *@returns {object} Home page
 */
function Home() {
	const [state, actions] = useAppProvider();
	const [open, setOpen] = useState(false);

	return (
		<div>
			<Header />
			<MenuPages />
			<Segment textAlign="center">
				<h1>Home Page</h1>
  			{!state.session.loggedIn ?
			  	<Button.Group>
					<Button color='teal' as={Link} to={'/sign-in'}>
						Sing In
					</Button>
					<Button.Or />
					<Button color='blue' as={Link} to={'/sign-up'} inverted>
						Sing Up
					</Button>
  				</Button.Group> : 
				<>
					<h4>Welcome {state.session.profile.username}</h4>
					<Button
						color='blue'
						inverted
						onClick={() => setOpen(true)}
					> Logout </Button>
					<Confirm
						open={open}
						onCancel={() => setOpen(false)}
						onConfirm={() => {
							state.session.end();
							actions.closeSession();
							window.location.reload(false);
							setOpen(false);
						}}
					/>
				</>}
			</Segment>
		</div>
	);
}

export default Home;
