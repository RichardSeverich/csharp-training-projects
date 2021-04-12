import React, { useEffect, useState } from 'react';
import { Container, Pagination as PageManagement, Form, Input, Button, Segment } from 'semantic-ui-react';
import { useDataProvider } from 'context/tasks/tasks';

function Pagination(props) {
    const initState= {
        currentPage: props.currentPage,
        pageSize: props.pageSize,
        pageChanged: false,
    }
    const [, actions] = useDataProvider();
    const [state, setState] = useState(initState);
    const [confirm, setConfirm] = useState(true);

    useEffect(() => {
        if (state.pageChanged) {
            setState({ ...state, pageChanged: false})
            const payload = {
                currentPage: state.currentPage,
                pageSize: parseInt(state.pageSize),
            }
            actions.OnPageChange(payload);
        }
    }, [state.pageChanged])

    const handlePageChange = (e, { activePage }) => {
        setState({ ...state, currentPage: activePage, pageChanged: true });
    };
    const handleSizeChange = (e, { value }) => {
        if (value > 0) {
            setState({...state, pageSize: value })
            setConfirm(false);    
        }
    };

    const handleSubmit = () => {
        setState({ ...state, pageChanged: true })
        setConfirm(true);
    };

    return (
        <Segment className='pagination-size'>
            <Container textAlign="center" >
                <PageManagement 
                    activePage={state.currentPage}
                    onPageChange={handlePageChange}
                    totalPages={props.totalPages}
                />
            </Container>
            <Container textAlign='right'>
                <Form onSubmit={handleSubmit}>
                    <Form.Field width='2' >
                        <label>Size</label>
                        <Input
                            action
                            name='size'
                            type='number'
                            value={state.pageSize}
                            onChange={handleSizeChange}
                        >    
                            <input />
                            <Button disabled={confirm} >Ok</Button>
                        </Input>
                    </Form.Field>
                </Form>
            </Container>
        </Segment>
    )
}

export default Pagination;