import React, { useState } from 'react';
import {saveData} from 'api/api';
import { useHistory } from 'react-router-dom';

import Header from 'components/header';
import { Header as Head, Container, Form, Button, Segment } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

import { thereIsAnError } from 'helpers/helper';
import { ValidateFields } from 'helpers/validations';

const initUser = {
  Username:"",
  Password:"",
  Name:"",
  Lastname:"",
  Email:""
}

const errors = {
  Name:{
    state:false,
    message:"",
  },
  Lastname:{
    state:false,
    message:"",
  },
  Username:{
    state:false,
    message:"",
  },
  Password:{
    state:false,
    message:"",
  },
  Email:{
    state:false,
    message:""
  },
}

/**
 *@returns {object} Sing up page
 */
function SignUp() {
  const [state, setState] = useState(initUser);
  const [error, setError] = useState(errors);
  const [loading, setLoading] = useState(false);
  let history = useHistory();

	const handleOnChange = (key, value) => {
		setState({ ...state, [key]: value });
	};

  const onSubmit = () => {
    setLoading(true);
    ValidateFields(state).then(
      result => {
        setError(result);
        if (!thereIsAnError(result))
          saveData('signup', state).then(
            response => response.json()
          ).then(() => {
            alert('Your account was created, please sign-in.');
            history.push('/sign-in');
          }).catch(
            error => console.log(error)
          );
        setLoading(false);
    });    
	};

	return (
		<div>
      <Header/>
      <Segment loading={loading}>
        <Container>
          <div>
            <Head textAlign="center" as='h1'>
              <Head.Subheader>
                Join ToDoApp
              </Head.Subheader>
              Create Your Account
            </Head>
          </div>
            <Form id='new-account' onSubmit={onSubmit}>
              <Form.Input
                error={error.Username.state && error.Username.message}
                label="Username *"
                placeholder="Username"
                value={state.Username}
                onChange={(e, { value }) => handleOnChange('Username', value)}
              />
              <Form.Group widths="equal">
                <Form.Input 
                  error={error.Name.state && error.Name.message}
                  label="Name *"
                  placeholder="Name"
                  value={state.Name}
                  onChange={(e, { value }) => handleOnChange('Name', value)}
                />
                <Form.Input
                  error={error.Lastname.state && error.Lastname.message}
                  label="Lastname *"
                  placeholder="Lastname"
                  value={state.Lastname}
                  onChange={(e, { value }) => handleOnChange('Lastname', value)}
                />
              </Form.Group>
              <Form.Input
                error={error.Email.state && error.Email.message}
                label="Email Address *"
                placeholder="Email Address"
                value={state.Email}
                onChange={(e, { value }) => handleOnChange('Email', value)}
              />
              <Form.Input 
                error={error.Password.state && error.Password.message}
                type="password"
                label="Password *"
                placeholder="Password"
                value={state.Password}
                onChange={(e, { value }) => handleOnChange('Password', value)}
              />
			    	<Button content="Cancel" to={'sign-in'} as={Link} negative />
			    	<Button type="Submit" form="new-account" content="Create Account" color="blue"/>
            </Form>
        </Container>
      </Segment>
		</div>
	);
}

export default SignUp;