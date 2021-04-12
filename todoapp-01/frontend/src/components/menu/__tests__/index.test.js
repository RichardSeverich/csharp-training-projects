import React from 'react';
import MenuPages from '../.';
import { render } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';

let Menu = () => {
	return (
		<Router>
			<MenuPages />
		</Router>
	);
};

describe('components/menu', () => {
	it('should be instance of', () => {
		const { container } = render(<Menu />);
		expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
	});

	it('should match the snapshot', () => {
		const { container } = render(<Menu />);
		expect(container.firstChild).toMatchSnapshot();
	});
});
