import React from 'react';
import ReactDOM from 'react-dom';
import 'semantic-ui-css/semantic.css';
import './index.css';
import App from './App';
import AppProvider from './context/application/app';
import { BrowserRouter as Router } from 'react-router-dom';

ReactDOM.render(
	<Router basename="/" forceRefresh={false}>
		<AppProvider>
			<App />
		</AppProvider>
	</Router>,
	document.getElementById('root')
);
