import React, { useState, useEffect } from 'react';
import { Segment, Button, Form } from 'semantic-ui-react';
import ProjectList from '../../components/projects_list';
import ProjectsBar from '../../components/navigation_bar';
import { useProjectProvider } from '../../context/projects';
import TaskList from 'components/task_list';
import ProjectAdd from '../modal_form';
import { filterProjects } from 'helpers/filter';
import { ProjectsSorter } from 'helpers/sort';
import { useAppProvider } from 'context/application/app';

const emptyArray = [];
const emptyObject = {
	uuid: "00000000-0000-0000-0000-000000000000",
	name: "My projects",
	parent: null,
	tasks: []
};
const newValue = {
	name: '',
	parent: ''
}

const options = [
	{ key: 'asc', text: 'Order by name (ascendent)', value: 'asc' },
	{ key: 'desc', text: 'Order by name (descendent)', value: 'desc' },
];

function ProjectsView() {
	const [appState] = useAppProvider();
	const [state, actions] = useProjectProvider();
	const [currentProjects, setCurrentProjects] = useState(emptyArray);
	const [parent, setParent] = useState(emptyObject);
	const { data } = state;
	const [currentTasks, setCurrentTasks] = useState(emptyArray);
	const [show, setShow] = useState('asc');

	useEffect(() => {
		actions.onProjectLoad();
	}, [appState]);

	useEffect(() => {
		if(!state.loading && !state.error){
			const lastData = data.path.length - 1;
			const newParent = data.path[lastData];
			setParent(newParent);
			const newCurrentProjects = ProjectsSorter(show, filterProjects(newParent, data.projects));
			const newCurrentTasks = newParent.tasks.filter(task => task.status!=='Deleted');
			setCurrentTasks(newCurrentTasks);
			setCurrentProjects(newCurrentProjects);
		}
	}, [state, show]);

	function OnSelect(value) {
		let newPath = data.path;
		newPath.push(value);
		
		actions.onProjectSelect(newPath);
	}

	function moveTo(value) {
		const indexOfValue = data.path.indexOf(value);
		const newPath = data.path.slice(0, indexOfValue + 1);

		actions.onProjectSelect(newPath);
	};

    return (
		<>
			<ProjectsBar value={data.path} onClick={moveTo} />
			<br />
			<Form>
				<Form.Select
					options={options}
					placeholder="Order by..."
					name="show"
					value={show}
					onChange={(e, { value }) => {setShow(value)}}
				/>
			</Form>
			<ProjectAdd 
				close={()=>{}}
				title="Add Project"
				type="add"
				value={{...newValue, parent: parent.uuid}}
				onSubmit={actions.onProjectAdd}
				button={<Button>Add project</Button>}
				notAllow={currentProjects}
			/>
			<Segment loading = {state.loading} >
				{!state.error ? (<>
					{currentProjects.length > 0 &&
						<><h3>Projects</h3>
						<ProjectList
							value={currentProjects}
							actions={actions}
							onSelect={OnSelect}
							show={show}
						/></>}
					{currentTasks.length > 0 &&
						<><h3>Tasks</h3>
						<TaskList tasks={currentTasks} title="Tasks" />
						</>}
				</>)
				: <h3>An error ocurred while loading the data, please reload the page.</h3> }
				{(currentProjects == 0 && currentTasks == 0) &&
					<h3>There is nothing here, let's create something!</h3> }
			</Segment>
		</>
    );
}

export default ProjectsView;