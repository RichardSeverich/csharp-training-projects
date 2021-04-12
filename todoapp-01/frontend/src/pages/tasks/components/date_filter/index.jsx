import React from 'react';
import { Dropdown } from 'semantic-ui-react';

const dateOptions = [
    { key: 'AT', text: 'Any time', value: 'any' },
    { key: 'HA', text: 'Past hour', value: 'hour' },
    { key: 'DA', text: 'Past 24 hours', value: 'day' },
    { key: 'WA', text: 'Past week', value: 'week' },
    { key: 'MA', text: 'Past month', value: 'month' },
    { key: 'YA', text: 'Past year', value: 'year' },
];

function DateFilter(props) {
    return (
        <Dropdown 
            onChange={(e, {value}) => props.onChange(value)}
            options={dateOptions}
            placeholder='Date created'
            selection
            value={props.value}
        />
    )
}

export default DateFilter;