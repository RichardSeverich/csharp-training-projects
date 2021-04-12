import React from 'react';
import { Confirm } from 'semantic-ui-react';

function DeleteConfirm (props) {
    const { uuid, open, setOpen, onConfirm } = props;

    return (
        <Confirm
            open={open}
            onCancel={() => setOpen(false)}
            onConfirm={() => {
                onConfirm(uuid);
                setOpen(false);
            }}
        />
    )
}

export default DeleteConfirm;