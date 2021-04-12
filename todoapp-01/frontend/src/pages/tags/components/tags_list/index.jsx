import React from 'react';
import { Label } from 'semantic-ui-react';
import TagItem from '../tags_item';

function TagList(props) {
    const { value } = props;
    
    const content = value.length ? (value.map((tag, idx) => 
        <TagItem key={idx} value={tag} />
        )) : 'There is not more tags here.';

    let render = value.length;
    return(
        <div>
            {(render && <h3>{content}</h3>) || 
            <Label.Group >{content}</Label.Group>}
        </div>
        );
}

export default TagList;
