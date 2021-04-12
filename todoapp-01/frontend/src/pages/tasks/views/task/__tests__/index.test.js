import React from 'react';
import TaskView from '../.';
import { render } from '@testing-library/react';
import { TaskProvider } from 'context/tasks/tasks';

describe('pages/tasks/views/task', () => {
	it('should be instance of', () => {
		const { container } = render(
			<TaskProvider>
				<TaskView />
			</TaskProvider>
		);
		expect(container.firstChild).toBeInstanceOf(HTMLDivElement);
	});

	it('should render correctly', () => {
		const { container } = render(
			<TaskProvider>
				<TaskView />
			</TaskProvider>
		);
		expect(container.firstChild).toMatchSnapshot();
	});

	it('children should have instance of', () => {
		const { container } = render(
			<TaskProvider>
				<TaskView />
			</TaskProvider>
		);
		expect(container.firstChild.firstChild).toBeInstanceOf(HTMLDivElement);
	});
});
