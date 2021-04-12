const mockTask = [
	{
		id: 1,
		uuid: '494c7fbc-0fde-4230-a15c-d5bb903f8292',
		description: 'Research about imaginary numbers',
		priority: 'Low',
		status: 'Deleted',
		start: '2021-02-23T13:50:54Z',
		due: null,
		end: '2021-02-23T14:00:21Z',
		entry: '2020-12-12T00:00:00Z',
		depends: 0,
		projectUuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
		tags: [
			{
				id_Tag: 1,
				tag: {
					id: 1,
					name: 'Research',
					tasks: [
						{
							id_Tag: 1,
						},
						{
							id_Tag: 1,
						},
						{
							id_Tag: 1,
						},
						{
							id_Tag: 1,
						},
						{
							id_Tag: 1,
						},
					],
				},
			},
		],
	},
	{
		id: 2,
		uuid: '6486c57d-6251-41b9-b87d-0de13ae54781',
		description: 'Do my sum exercises today',
		priority: 'Medium',
		status: 'Completed',
		start: '2020-12-15T00:00:00Z',
		due: '2021-02-23T16:15',
		end: '2020-12-16T00:00:00Z',
		entry: '2020-12-15T00:00:00Z',
		depends: 0,
		projectUuid: '494c7fbc-0fde-4230-a15c-d5bb903f8294',
		tags: [
			{
				id_Tag: 4,
				tag: {
					id: 4,
					name: 'Exercise',
					tasks: [
						{
							id_Tag: 4,
						},
					],
				},
			},
		],
	},
];

export default mockTask;
