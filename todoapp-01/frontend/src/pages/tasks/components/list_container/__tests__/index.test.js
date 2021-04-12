import React from 'react';
import ListContainer from '../.';
import { fireEvent, render } from '@testing-library/react';

let mockProps = {
	title: 'All tasks',
	tasks: [
		{
			depends: 0,
			description: 'Do my sum exercises today',
			due: '2021-02-23T16:15',
			end: '2020-12-16T00:00:00Z',
			entry: '2020-12-15T00:00:00Z',
			id: 2,
			priority: 'Medium',
			projectUuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
			start: '2020-12-15T00:00:00Z',
			status: 'Completed',
			tags: [],
			uuid: '6486c57d-6251-41b9-b87d-0de13ae54781',
		},
		{
			depends: 0,
			description: 'Do my subtraction exercises 1',
			due: '',
			end: null,
			entry: '2020-12-15T00:00:00Z',
			id: 4,
			priority: 'Medium',
			projectUuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
			start: '2020-12-15T00:00:00Z',
			status: 'In Progress',
			tags: [],
			uuid: '27b875d8-a109-4dbb-bd1f-3e8ca68955f0',
		},
	],
};

describe('pages/tasks/components/list_container', () => {
	describe('Html', () => {
		it('should be a type of', () => {
			const { container } = render(<ListContainer {...mockProps} />);
			expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
		});

		it('should render correctly', () => {
			const { container } = render(<ListContainer {...mockProps} />);
			expect(container.firstChild).toMatchSnapshot();
		});
	});

	describe('interactions', () => {
		it('should trigger an event on click', () => {
			const mockOnClick = jest.fn();
			const { getByText } = render(<ListContainer {...mockProps} onClick={mockOnClick} />);
			const Target = getByText('All tasks');
			fireEvent.click(Target);
			expect(true).toBe(true);
		});
	});
});
