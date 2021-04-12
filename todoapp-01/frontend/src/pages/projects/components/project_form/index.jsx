import React, { useState } from 'react';
import { Form } from 'semantic-ui-react';
import { useHistory } from 'react-router-dom';

const empty = {};

function ProjectForm(props) {
	const { value, notAllow, onSubmit } = props;
	const [state, setState] = useState(value);
	const [error, setError] = useState(false);

	const handleOnChange = (key, value) => {
		setState({ ...state, [key]: value });
	}

	let history = useHistory();

	const handleSubmit = () => {
		const values = notAllow.map(p => p.name !== state.name);
		if (!values.includes(false)) {
			if (props.type !== 'edit') {
				onSubmit(state);
				history.push('/projects');
			} else {
                const newValue = { uuid: state.uuid,
                                   data: {
                                        name: state.name,
                                        parent: state.parent
                                    }
                                };
				onSubmit(newValue);
				history.push('/projects');
			}
			props.open(false);
		} else {
			setError(true);
		}
	};

    return (
    <>
        <Form id="project-form" onSubmit={handleSubmit}>
            <Form.Input
                error={error && { content: 'The name is already in this project', pointing: 'below' }}
                label="Name:"
                placeholder="name"
                name="name"
                value={state.name}
                onChange={(e, { value }) => {
                    setError(false);
                    handleOnChange("name", value);
                }}
            />
		</Form>
    </>
    )
}

export default ProjectForm;