import React from 'react';
import TagItem from '../.';
import { render } from '@testing-library/react';

const mockTag = { name: 'Exercise', count: 2 };

describe('pages/tags/components/tags_item', () => {
	describe('Html rendering', () => {
		it('should be instance of', () => {
			let { container } = render(<TagItem value={mockTag} />);
			expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
		});

		it('should render the expected label', () => {
			let { getByText } = render(<TagItem value={mockTag} />);
			expect(getByText('Exercise (2)')).not.toEqual(null);
		});

		it('should not render another label', () => {
			let { queryByText } = render(<TagItem value={mockTag} />);
			expect(queryByText('Biology (2)')).toEqual(null);
		});
	});
});
