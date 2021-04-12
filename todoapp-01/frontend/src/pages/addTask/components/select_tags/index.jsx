import React, { useEffect, useState } from 'react';
import { Dropdown } from 'semantic-ui-react';
import { useTagProvider } from 'pages/tags/context/tags';

const emptyArray = [];

/**
 * @param {object} props - props sent to fill data
 * @returns {object} Return select for tags
 */
function SelectTags(props) {
	const [state, actions] = useTagProvider();
	const { data } = state;
	const [options, setOptions] = useState(emptyArray);
	const [currentValues, setCurrentValues] = useState(props.tags);

	useEffect(()=>{
		if (!state.loading) {
			const options = data.tags.map((tag) => ({ key: tag.name, text: tag.name, value: tag.id }));
			setOptions(options);
		}
	}, [data.tags, state]);

	const handleChange = (e, { value }) => {
		setCurrentValues(value);
		props.onChange('tags', value);
	};

	const handleAddition = (e, { value }) => {
		actions.OnTagAdd(value);
	};

	return (
		<Dropdown
			id="select_tags"
			options={options}
			placeholder="Add tags or choose an existent tags"
			search
			selection
			fluid
			multiple
			allowAdditions
			value={currentValues}
			onAddItem={handleAddition}
			onChange={handleChange}
		/>
	);
}

export default SelectTags;
