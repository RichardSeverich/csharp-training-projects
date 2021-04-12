import React from 'react';
import { Card } from 'semantic-ui-react';
import ProjectItem from '../project_item';

function ProjectList(props) {
    const { value, onSelect, actions } = props;
    
    const content = value.length ? (value.map(project => 
        <ProjectItem 
            key={project.uuid}
            notAllow={value.filter(p => p.uuid !== project.uuid)}
            value={project}
            onSelect={onSelect}
            onEdit={actions.onProjectChange}
            onDelete={actions.onProjectDelete}
        />
        )) : 'There is not more projects here.';

    let render = value.length;
    return(
        <>
            {(!render && <h3>{content}</h3>) || <Card.Group>{content}</Card.Group>}
        </>
        );
}

export default ProjectList;