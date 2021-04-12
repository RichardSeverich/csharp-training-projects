import React from 'react';
import TagList from '../.';
import { render } from '@testing-library/react';

const mockTagList = [
	{ name: 'Exercise', count: 2 },
	{ name: 'Biology', count: 3 },
];

describe('pages/tags/components/tags_list', () => {
	describe('Html rendering', () => {
		it('should be instance of', () => {
			const { container } = render(<TagList value={mockTagList} />);
			expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
		});

		it('should render message when there are no tags', () => {
			const { getByText } = render(<TagList value={[]} />);
			expect(getByText('There is not more tags here.')).not.toBeNull();
		});

		it('should not render message when there are tags', () => {
			const { queryByText } = render(<TagList value={mockTagList} />);
			expect(queryByText('There is not more tags here.')).toBeNull();
		});
	});
});
