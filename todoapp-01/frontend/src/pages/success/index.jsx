import React from 'react';

import Header from 'components/header';
import MenuPages from 'components/menu';
import ConfirmationSave from './components/confirmationSave';

function Confirmation(props) {
	return (
		<div>
			<Header />
			<MenuPages />
			<ConfirmationSave />
		</div>
	);
}

export default Confirmation;
