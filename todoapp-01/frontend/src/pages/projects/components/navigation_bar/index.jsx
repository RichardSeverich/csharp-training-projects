import React from 'react';
import { Breadcrumb } from 'semantic-ui-react';
import './index.css';


function NavigationBar(props) {
	const { value, onClick } = props;
	let index = 0;
	const content = value.length ? (value.map(project => 
				<Breadcrumb.Section key={project.uuid} link onClick={() => onClick(project)} >{project.name}</Breadcrumb.Section>
	)) : ( 'No data' );
	
	const newContent = [];
	if (value.length){
		content.forEach(element => {
			newContent.push(element);
			newContent.push(<Breadcrumb.Divider key={index++} />)
		});
	}
    return (
		<Breadcrumb className="projects-bar">
			{newContent? newContent : content}
		</Breadcrumb>
	);
}

export default NavigationBar;
