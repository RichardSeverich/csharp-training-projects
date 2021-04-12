import React, { createContext, useContext, useMemo, useReducer, useEffect } from 'react';
import { State, Reducer } from './state';
import ActionFactory from './actions';

export const TaskContext = createContext();
export const TaskDispatch = createContext();

/**
 * @param {object} props props received.
 * @returns {React.Component} something.
 */
export function TaskProvider(props) {
	const { children } = props;
	const [value, dispatch] = useReducer(Reducer, props, State);
	const actions = useMemo(() => ActionFactory(dispatch), [dispatch]);

	useEffect(() => {
		/*const payload = {
			pageNumber: value.currentPage,
			pageSize: value.pageSize,
			...value.search, 
		};
		actions.OnLoad(payload);*/
	}, [actions]);

	return (
		<TaskContext.Provider value={value}>
			<TaskDispatch.Provider value={actions}>{children}</TaskDispatch.Provider>
		</TaskContext.Provider>
	);
}

/**
 * @returns {object[]} state and actions.
 */
export function useDataProvider() {
	const state = useContext(TaskContext);
	const actions = useContext(TaskDispatch);

	return [state, actions];
}
