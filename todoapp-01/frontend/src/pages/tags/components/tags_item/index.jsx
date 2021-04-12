import React from 'react';
import { Label } from 'semantic-ui-react';

const colors = [
  'red', 'orange', 'yellow',
  'olive', 'green', 'teal',
  'blue', 'violet', 'purple',
  'pink',
]

function TagItem(props) {
    const { value } = props;
    const color = colors[Math.floor(Math.random() * (10))];

    return(<Label tag color={color}>
      {`${value.name} (${value.count})`}
    </Label>
    );
}

export default TagItem;