import React from 'react';

const Select = props => {
    return (
        <select className="custom-select"
            id={props.id}
            name={props.name}
            onChange={props.onChange}
            disabled={props.disabled}
            required={props.required}>
            {props.values.map(v =>
                <option key={v.id}
                    value={v.id}>
                    {v.name}
                </option>
            )}
        </select>
    );
}

export { Select };