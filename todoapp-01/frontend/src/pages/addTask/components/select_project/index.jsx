import React, { useContext, useEffect, useState } from 'react';
import { Dropdown } from 'semantic-ui-react';

import { useProjectProvider } from 'pages/projects/context/projects';
 
const emptyArray = [];

/**
 * @param {object} props - props sent to fill data
 * @returns {object} Return select for tags
 */
function SelectProject(props) {
	const [state, actions] = useProjectProvider();
	const { data } = state;
	const [options, setOptions] = useState(emptyArray);
	const [currentValue, setCurrentValue] = useState(props.project);

	useEffect(()=>{
		if (!state.loading) {
			const options = data.projects.map((project) => ({ key: project.uuid, text: project.name, value: project.uuid }));
			setOptions(options);
		}
	}, [state]);

	const handleChange = (e, { value }) => {
		setCurrentValue(value);
		props.onChange('projectUuid', value);
	};

	return (
		<Dropdown
			id="select_project"
			options={options}
			placeholder="choose an existent project"
			search
			selection
			fluid
			value={currentValue}
			onChange={handleChange}
		/>
	);
}

export default SelectProject;
