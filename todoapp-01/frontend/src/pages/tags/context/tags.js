import React, { createContext, useContext, useEffect, useMemo, useReducer } from 'react';
import { State, Reducer } from './state';
import ActionFactory from './actions';

export const TagContext = createContext();

export const TagDispatch = createContext();

/**
 * @param {object} props props received.
 * @returns {React.Component} returns react component.
 */
export function TagProvider(props) {
	const { children } = props;
	const [value, dispatch] = useReducer(Reducer, props, State);
	const actions = useMemo(() => ActionFactory(dispatch), [dispatch]);

	useEffect(() => {
		actions.OnTagLoad();
	}, [actions]);

	return (
		<TagContext.Provider value={value}>
			<TagDispatch.Provider value={actions}>{children}</TagDispatch.Provider>
		</TagContext.Provider>
	);
}

/**
 * @returns {object[]} returns state and actions.
 */
export function useTagProvider() {
	const state = useContext(TagContext);
	const actions = useContext(TagDispatch);

	return [state, actions];
}
