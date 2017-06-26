import React from 'react';
import PropTypes from 'prop-types';

const LargeTextField = ({ source, record = {} }) => (
    <pre>{record[source]}</pre>
);

LargeTextField.propTypes = {
    label: PropTypes.string,
    record: PropTypes.object,
    source: PropTypes.string.isRequired,
};

export default LargeTextField;