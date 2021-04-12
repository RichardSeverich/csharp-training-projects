import {
	getData,
	saveData,
	getDataWithToken,
	postDataWithToken,
	deleteData,
	updateData,
	updateStatus,
} from '../api';
import 'regenerator-runtime/runtime.js';
// eslint-disable-next-line jest/no-mocks-import
import mockTask from '../__mocks__/mockTask';

const fetchMock = require('fetch-mock');
fetchMock.config.overwriteRoutes = true;

fetchMock
	.get('https://localhost:44392/api/tasks', mockTask)
	.post('https://localhost:44392/api/tasks', mockTask[0]);
/**
 *
 * @param {string} target body from the response.
 * @returns {object} json object of the body.
 */
function toJSON(target) {
	return JSON.parse(target);
}
describe('API', () => {
	describe('check get method is recovering the correct data', () => {
		it('Will get a list with the amount of tasks in db', async () => {
			const response = await getData('tasks');
			expect(response.length).toEqual(2);
		});
		it('fetch the task 1, description', async () => {
			const response = await getData('tasks');
			expect(response[0].description).toEqual('Research about imaginary numbers');
		});

		it('fetch the task 1, start', async () => {
			const response = await getData('tasks');
			expect(response[0].start).toEqual('2021-02-23T13:50:54Z');
		});

		it('fetch the task 1, uuid', async () => {
			const response = await getData('tasks');
			expect(response[0].uuid).toEqual('494c7fbc-0fde-4230-a15c-d5bb903f8292');
		});

		it('fetch the task 2, description', async () => {
			const response = await getData('tasks');
			expect(response[1].description).toEqual('Do my sum exercises today');
		});

		it('fetch the task 2, start', async () => {
			const response = await getData('tasks');
			expect(response[1].start).toEqual('2020-12-15T00:00:00Z');
		});

		it('fetch the task 2, uuid', async () => {
			const response = await getData('tasks');
			expect(response[1].uuid).toEqual('6486c57d-6251-41b9-b87d-0de13ae54781');
		});
	});

	describe('check save method is saving correctly', () => {
		afterAll(() => fetchMock.restore());
		it('save data correctly', async () => {
			const response = await saveData('tasks', mockTask[0]);
			expect(response.status).toBe(200);
		});

		it('save data status Ok', async () => {
			const response = await saveData('tasks', mockTask[0]);
			expect(response.statusText).toBe('OK');
		});

		it('body should have the task saved, task 1 description', async () => {
			const response = await saveData('tasks', mockTask[0]);
			expect(toJSON(response.body).description).toBe('Research about imaginary numbers');
		});

		it('body should have the task saved, task 2 description', async () => {
			fetchMock.post('https://localhost:44392/api/tasks', mockTask[1]);

			const response = await saveData('tasks', mockTask[1]);
			expect(toJSON(response.body).description).toBe('Do my sum exercises today');
		});

		it('return a status 400 when something fails', async () => {
			fetchMock.post('https://localhost:44392/api/tasks', 400);
			const response = await saveData('tasks', mockTask[0]);
			expect(response.status).toBe(400);
		});

		it('return a bad request', async () => {
			fetchMock.post('https://localhost:44392/api/tasks', 400);
			const response = await saveData('tasks', mockTask[0]);
			expect(response.statusText).toBe('Bad Request');
		});
	});

	describe('check get data with token', () => {
		it('should return the list of tasks when the token is passed', async () => {
			const token = 'Basic dummy-token';
			fetchMock.mock('https://localhost:44392/api/tasks', 403).mock(
				{
					url: 'https://localhost:44392/api/tasks',
					headers: {
						Authorization: `Bearer ${token}`,
					},
				},
				mockTask
			);
			const response = await getDataWithToken('tasks', token);
			expect(response.length).toEqual(2);
		});

		it('should return the task 1, description when the token is passed', async () => {
			const token = 'Basic dummy-token';
			fetchMock.mock('https://localhost:44392/api/tasks', 403).mock(
				{
					url: 'https://localhost:44392/api/tasks',
					headers: {
						Authorization: `Bearer ${token}`,
					},
				},
				mockTask
			);
			const response = await getDataWithToken('tasks', token);
			expect(response[0].description).toEqual('Research about imaginary numbers');
		});

		it('should return the task 2, uuid when the token is passed', async () => {
			const token = 'Basic dummy-token';
			fetchMock.mock('https://localhost:44392/api/tasks', 403).mock(
				{
					url: 'https://localhost:44392/api/tasks',
					headers: {
						Authorization: `Bearer ${token}`,
					},
				},
				mockTask
			);
			const response = await getDataWithToken('tasks', token);
			expect(response[1].uuid).toEqual('6486c57d-6251-41b9-b87d-0de13ae54781');
		});
	});
	describe('post data with token', () => {
		it('should post correctly when given passed the token', async () => {
			const data = mockTask[0];
			const token = 'Basic dummy-token';
			fetchMock.post('https://localhost:44392/api/tasks', 200, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await postDataWithToken('tasks', data, token);
			expect(response.status).toEqual(200);
		});

		it('should return a 401 error when no token is sent', async () => {
			const data = mockTask[0];
			const token = '';
			fetchMock.post('https://localhost:44392/api/tasks', 401, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await postDataWithToken('tasks', data, token);
			expect(response.status).toEqual(401);
			expect(response.statusText).toEqual('Unauthorized');
		});
	});

	describe('Check delete method is working properly', () => {
		it('should delete correctly when token is passed', async () => {
			const uuid = mockTask[0].uuid;
			const token = 'Basic dummy-token';
			fetchMock.delete(`https://localhost:44392/api/tasks/${uuid}`, 200, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await deleteData(`tasks/${uuid}`, token);
			expect(response.status).toEqual(200);
			expect(response.statusText).toEqual('OK');
		});

		it('should receive a 401 status when token is not passed', async () => {
			const uuid = mockTask[0].uuid;
			const token = '';
			fetchMock.delete(`https://localhost:44392/api/tasks/${uuid}`, 401, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await deleteData(`tasks/${uuid}`, token);
			expect(response.status).toEqual(401);
			expect(response.statusText).toEqual('Unauthorized');
		});
	});
	describe('Check if the updateData method works correctly', () => {
		it('Should update correctly when token is passed', async () => {
			const uuid = mockTask[1].uuid;
			const data = mockTask[1];
			data.description = 'New Description';
			const token = 'Basic dummy-token';
			fetchMock.put(`https://localhost:44392/api/tasks/${uuid}`, 200, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await updateData(`tasks/${uuid}`, data, token);
			expect(response.status).toEqual(200);
			expect(response.statusText).toEqual('OK');
		});

		it('Should update correctly and return the new task when token is passed', async () => {
			const uuid = mockTask[1].uuid;
			const data = mockTask[1];
			data.description = 'New Description';
			const token = 'Basic dummy-token';
			fetchMock.put(`https://localhost:44392/api/tasks/${uuid}`, data, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await updateData(`tasks/${uuid}`, data, token);
			expect(toJSON(response.body).description).toEqual(data.description);
		});

		it('Should return a 401 status when the token is not passed', async () => {
			const uuid = mockTask[1].uuid;
			const data = mockTask[1];
			data.description = 'New Description';
			const token = '';
			fetchMock.put(`https://localhost:44392/api/tasks/${uuid}`, 401, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await updateData(`tasks/${uuid}`, data, token);
			expect(response.status).toEqual(401);
			expect(response.statusText).toEqual('Unauthorized');
		});
	});
	describe('Update Status should work correctly', () => {
		it('Should return a good response when the token is passed', async () => {
			const uuid = mockTask[1].uuid;
			const data = {
				status: 'In Progress',
			};
			const token = 'Basic dummy-token';
			fetchMock.patch(`https://localhost:44392/api/tasks/${uuid}`, 200, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await updateStatus(`tasks/${uuid}`, data, token);
			expect(response.status).toEqual(200);
			expect(response.statusText).toEqual('OK');
		});

		it('Should return a 401 response when the token is not passed', async () => {
			const uuid = mockTask[1].uuid;
			const data = {
				status: 'In Progress',
			};
			const token = '';
			fetchMock.patch(`https://localhost:44392/api/tasks/${uuid}`, 401, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
			const response = await updateStatus(`tasks/${uuid}`, data, token);
			expect(response.status).toEqual(401);
			expect(response.statusText).toEqual('Unauthorized');
		});
	});
});
