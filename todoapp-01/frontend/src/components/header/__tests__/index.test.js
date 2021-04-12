import React from 'react';
import Header from '../.';
import { render } from '@testing-library/react';

describe('components/header', () => {
	it('should be instance of Heading, first child', () => {
		const { container } = render(<Header />);
		expect(container.firstChild).toBeInstanceOf(HTMLHeadingElement);
	});

	it('should be instance of div, second child', () => {
		const { container } = render(<Header />);
		expect(container.querySelector('.header .container')).toBeInstanceOf(HTMLDivElement);
	});
	it('should match the snapshot', () => {
		const { container } = render(<Header />);
		expect(container.firstChild).toMatchSnapshot();
	});
});
