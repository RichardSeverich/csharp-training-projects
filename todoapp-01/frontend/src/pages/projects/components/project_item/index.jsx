import React, { useState } from 'react';
import { Button, Card, Header, Icon, Segment } from 'semantic-ui-react';
import ProjectEdit from '../../views/modal_form';
import DeleteConfirm from '../confirm_delete';


function ProjectItem(props) {
    const { value, onSelect, onEdit, onDelete, notAllow } = props;
    const [openE, setOpenE] = useState(false);
    const [openD, setOpenD] = useState(false);

    const content = <>
                        <Icon name='folder' /> {value.name}
                        <ProjectEdit 
                            title="Edit Project"
                            type="edit"
                            value={value}
                            open={openE}
                            onSubmit={onEdit}
                            button={<Button basic icon>
                                        <Icon name='edit'/>
                                    </Button>}
                            notAllow={notAllow}
                        />
                        <Button
                            basic
                            icon
                            onClick={() => setOpenD(true)}
                        >
                            <Icon name='trash'/>
                        </Button>
                        <DeleteConfirm
                            uuid={value.uuid}
                            open={openD}
                            setOpen={setOpenD}
                            onConfirm={onDelete}
                        />
                    </>

    return(<><Card
                className={'project-item'}
                color='red'
                id={value.uuid}
                header={content}
                onDoubleClick={() => onSelect(value)}
            />
            
            </>);
}

export default ProjectItem;