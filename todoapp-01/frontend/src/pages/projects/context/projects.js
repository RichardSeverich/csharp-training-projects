import React, { createContext, useContext, useEffect, useMemo, useReducer } from 'react';
import { State, Reducer } from './state';
import ActionFactory from './actions';

export const ProjectContext = createContext();

export const ProjectDispatch = createContext();

/**
 * @param {object} props props received.
 * @returns {React.Component} returns react component.
 */
export function ProjectProvider(props) {
	const { children } = props;
	const [value, dispatch] = useReducer(Reducer, props, State);
	const actions = useMemo(() => ActionFactory(dispatch), [dispatch]);

	useEffect(() => {
		actions.onProjectLoad();
	}, [actions]);

	return (
		<ProjectContext.Provider value={value}>
			<ProjectDispatch.Provider value={actions}>{children}</ProjectDispatch.Provider>
		</ProjectContext.Provider>
	);
}

/**
 * @returns {object[]} returns state and actions.
 */
export function useProjectProvider() {
	const state = useContext(ProjectContext);
	const actions = useContext(ProjectDispatch);

	return [state, actions];
}
