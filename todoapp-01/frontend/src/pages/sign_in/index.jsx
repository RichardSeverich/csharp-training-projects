import React, { useEffect, useState } from 'react';

import Header from 'components/header';
import { Header as Head, Container, Form, Button, Segment, Card, Message } from 'semantic-ui-react';
import { Link, Redirect } from 'react-router-dom';
import { saveData } from 'api/api';
import { useAppProvider } from 'context/application/app';

const initUser = {
  username: "",
  password: ""
}

/**
 *@returns {object} Sing in page
 */
function SignIn() {
  const [, actions] = useAppProvider();
  const [state, setState] = useState(initUser);
  const [logging, setLogging] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    if (logging) {
      saveData('authenticate', state).then(
        response => response.json()
      ).then(
        profile => {
          setLogging(false);
          if (profile.token) {
            actions.start(profile);
            window.location.reload(false);
          } else {
            setError(true);
          }
        }
      ).catch(
        error => {
          setLogging(false);
          setError(true);
        }
      );  
    }
  }, [logging]);

	const handleOnChange = (key, value) => {
    setError(false);
		setState({ ...state, [key]: value });
	};

	return (
		<div>
      <Header/>
      <Segment loading={logging}>
        <Container>
          <div>
            <Head textAlign="center" as='h1'>
              Sign In ToDoApp
            </Head>
          </div>
          {error && <Message
            onDismiss={()=>{setError(false)}}
            content='Incorrect username or password.'
            negative
          />}
            <Form id='sign-in' onSubmit={() => setLogging(true)}>
              <Form.Input
                label="Username *" 
                placeholder="Username"
                value={state.username}
                onChange={(e, { value }) => handleOnChange('username', value)}
              />
              <Form.Input
                type="password"
                label="Password *"
                placeholder="Password"
                value={state.password}
                onChange={(e, { value }) => handleOnChange('password', value)}
              />
			    	<Button form="sign-in" content="Sign In" color="blue" />
            </Form>
        </Container>
      </Segment>
      <Container textAlign='center' fluid>
        <Card centered>
          New to TodoApp? <Link to={'/sign-up'}>Create an Account</Link>
        </Card>
      </Container>
		</div>
	);
}

export default SignIn;
