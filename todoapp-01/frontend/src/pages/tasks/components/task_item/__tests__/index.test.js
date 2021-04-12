import React from 'react';
import TaskItem from '../.';
import { render, fireEvent } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';
// eslint-disable-next-line jest/no-mocks-import
import taskMock from '../__mocks__/task';
import AppProvider from 'context/application/app';

/**
 * @param {object} target mocked onClick function
 * @returns {object[]} amount of times the event was triggered.
 */
function getCalls(target) {
	return target.mock.calls;
}

describe('pages/tasks/components/task_item', () => {
	describe('Html', () => {
		it('Should render', () => {
			const { container } = render(<TaskItem value={{ ...taskMock }} />);
			expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
		});

		it('Should render correctly', () => {
			const { container } = render(<TaskItem value={{ ...taskMock }} />);
			expect(container.firstChild).toMatchSnapshot();
		});
	});

	describe('Interactions', () => {
		it('Should trigger event onClick', () => {
			const mockOnClick = jest.fn();
			const { getByTitle } = render(
				<AppProvider>
					<Router>
						<TaskItem value={{ ...taskMock }} onDelete={mockOnClick} />
					</Router>
				</AppProvider>
			);
			let button = getByTitle('Delete task');
			fireEvent.click(button);
			expect(getCalls(mockOnClick).length).toBeGreaterThanOrEqual(1);
		});
	});
});
