import React from 'react';
import SelectTags from '../.';
import { render } from '@testing-library/react';
import { TagProvider } from 'pages/tags/context/tags';

describe('pages/tasks/components/select_tags', () => {
	describe('html render', () => {
		it('should render', () => {
			const { container } = render(
				<TagProvider>
					<SelectTags />
				</TagProvider>
			);
			expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
		});

		it('should render properly', () => {
			const { container } = render(
				<TagProvider>
					<SelectTags />
				</TagProvider>
			);
			expect(container.firstChild).toMatchSnapshot();
		});
	});
});
