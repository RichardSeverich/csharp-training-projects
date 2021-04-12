import React from 'react';
import TaskView from '../.';
import { render } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';

const mockTask = {
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
	tags: [{ id_Tag: 4, tag: { id: 4, name: 'Exercise', tasks: [{ id_Tag: 4 }] } }],
	uuid: '6486c57d-6251-41b9-b87d-0de13ae54781',
};

describe('pages/tasks/views/task_modal', () => {
	describe('Html rendering', () => {
		it('should render correctly', () => {
			const { container } = render(<TaskView value={mockTask} />);
			expect(container.firstChild).toMatchSnapshot();
		});

		it('should be null when open is false', () => {
			const { container } = render(
				<Router>
					<TaskView value={mockTask} open={false} />
				</Router>
			);
			expect(container.firstChild).toBeNull();
		});
	});
});
