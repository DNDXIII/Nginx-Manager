import React from 'react';

export const EntityName=({ record })=> {
    return <span>Upstream{record ? `: ${record.name}` : ''}</span>;
};
