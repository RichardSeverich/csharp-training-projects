import React, { createContext, useMemo, useReducer, useContext } from 'react';
import ActionFactory from './actions';
import { State, Reducer } from './state';

const AppContext = createContext();
const AppDispatch = createContext();

/**
 * @param {object} props received properties
 * @returns {React.Component} application provider
 */
export default function AppProvider(props) {
	const { children } = props;
	const [value, dispatch] = useReducer(Reducer, props, State);
	const actions = useMemo(() => ActionFactory(dispatch), [dispatch]);

	return (
		<AppContext.Provider value={value}>
			<AppDispatch.Provider value={actions}>{children}</AppDispatch.Provider>
		</AppContext.Provider>
	);
}

/**
 * @returns {object[]} state and actions from the provider.
 */
export function useAppProvider() {
	const state = useContext(AppContext);
	const actions = useContext(AppDispatch);

	return [state, actions];
}
