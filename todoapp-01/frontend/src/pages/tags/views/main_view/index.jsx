import React, { useEffect, useState } from 'react';
import { Segment } from 'semantic-ui-react';
import TagList from '../../components/tags_list';
import { useTagProvider } from '../../context/tags';
import { TagsSorter } from 'helpers/sort';

const emptyArray = [];

function TagsView(props) {
	const { show } = props;
	const [dataSorted, setDataSorted] = useState(emptyArray)
	const [state] = useTagProvider();
	const { data } = state;

	useEffect(()=> {
		const parseData = data.tags.map(tag => 
			({
				name: tag.name,
				count: tag.tasks.length
			}));
		const newDataSorted = TagsSorter(show, parseData);
		setDataSorted(newDataSorted);
	}, [show, state])

	return (
	<>
		<Segment textAlign="center" loading = {state.loading} >
			{!state.error ? (<>
			{dataSorted.length > 0 && <> <TagList value={dataSorted} />
			</>}
			</>)
			: <h3>An error ocurred when loading the data, please reload the page.</h3> }
		</Segment>
	</>
	);
}

export default TagsView;