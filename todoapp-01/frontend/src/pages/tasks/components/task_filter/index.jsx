import React, { useEffect, useState } from 'react';
import { Container, Segment, Button } from 'semantic-ui-react';
import DateFilter from '../date_filter';
import ParametersFilter from '../parameters_filter';
import { useDataProvider } from 'context/tasks/tasks';

function TaskFilter(props) {
    const [, actions] = useDataProvider();
    const [searchValues, setSearchValues] = useState(props.search);
    const [search, setSearch] = useState(false);

    useEffect(() => {
        if (search) {
            setSearch(false);
            actions.OnSearchChange(searchValues);
        }
    }, [search]);

    
    const onParameterChange = (field, value) => {
        if (field === 'status') {
            setSearchValues({ ...searchValues, [field]: [searchValues.status[0], value] })
        }
        else{
            setSearchValues({ ...searchValues, [field]: value });
        }
    };

    const onDateChange = (value) => {
        setSearchValues({ ...searchValues, entry: value })
    };

    return(
        <Segment loading={search} >
            <Container fluid textAlign='center'>
                <ParametersFilter value={searchValues} onChange={onParameterChange} />
                <DateFilter value={searchValues.entry} onChange={onDateChange} />
                <Button onClick={() => setSearch(true)} icon='search' />
            </Container>
        </Segment>
    )
}

export default TaskFilter;