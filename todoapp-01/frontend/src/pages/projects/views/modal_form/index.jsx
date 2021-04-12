import React, { useEffect, useState } from 'react';
import ProjectForm from '../../components/project_form';
import { Modal, Button } from 'semantic-ui-react';

function ModalForm(props) {
    const { title, type, value, onSubmit, button } = props
    const [open, setOpen] = useState(false);

    useEffect(() => {
        if (props.open != undefined)
            setOpen(props.open);
    }, []);

    return <Modal
                closeIcon
                onClose={() => {
                    setOpen(false);
                }}
                onOpen={() => setOpen(true)}
                open={open}
                trigger={button}
                >
                <Modal.Header>{title}</Modal.Header>
                <Modal.Content>
                    <ProjectForm
                        open={setOpen}
                        type={type}
                        notAllow={props.notAllow}
                        value={value}
                        onSubmit={onSubmit}
                    />
                </Modal.Content>
                <Modal.Actions>
                    <Button content="Cancel" onClick={() => setOpen(false)} negative />
                    <Button form="project-form" content="Submit" type="submit" positive />
                </Modal.Actions>
            </Modal>
}

export default ModalForm;