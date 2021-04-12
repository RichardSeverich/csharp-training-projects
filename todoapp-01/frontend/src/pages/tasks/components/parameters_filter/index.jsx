import React, { useState, useEffect } from 'react';
import { Dropdown, Input } from 'semantic-ui-react';
import { useProjectProvider } from 'pages/projects/context/projects';
import { useTagProvider } from 'pages/tags/context/tags';
import { priorities, statusValues } from 'helpers/constants';
import { processOptionsObjects, processOptionsStrings } from 'helpers/process';

function getAllOptions(tags, projects) {
    const prioritiesOptions = processOptionsStrings(priorities);
    const statusOptions = processOptionsStrings(statusValues);
    const tagsOptions = tags.data.tags ? processOptionsObjects(tags.data.tags, 'id') : [];
    const projectOptions = projects.data.projects ? 
                                processOptionsObjects(projects.data.projects, 'uuid') : [];

    return {
        priorities: prioritiesOptions,
        projects: projectOptions,
        status: statusOptions,
        tags: tagsOptions,
    }
}

const initStateO = {
    priorities: [],
    projects: [],
    status: [],
    tags: [],
};

function ParametersFilter(props) {
    const { value } = props;
    const [tags] = useTagProvider();
    const [projects] = useProjectProvider();
    const [allOptions, setAllOptions] = useState(initStateO);

    useEffect(() => {
        setAllOptions(getAllOptions(tags, projects));
    }, [tags, projects]);

    return (
        <>
            <Input
                placeholder='Description'
                value={value.description}
                onChange={(e, { value }) => props.onChange('description', value)}
            />

            <Dropdown
                clearable
                onChange={(e, { value }) => props.onChange('priority', value)}
                options={allOptions.priorities}
                placeholder="Priority"
                search
                selection
                value={value.priority}
            />

            <Dropdown
                clearable
                onChange={(e, { value }) => props.onChange('status', value)}
                options={allOptions.status}
                placeholder="Status"
                search
                selection
                value={value.status[1]}
            />
            
            <Dropdown
                clearable
                onChange={(e, { value }) => props.onChange('project', value)}
                options={allOptions.projects}
                placeholder="Project"
                search
                selection
                value={value.project}
            />
            
            <Dropdown
                multiple
                onChange={(e, { value }) => props.onChange('tags', value)}
                options={allOptions.tags}
                placeholder="Tags"
                search
                selection
                value={value.tags}
            />
        </>
    );
}

export default ParametersFilter;