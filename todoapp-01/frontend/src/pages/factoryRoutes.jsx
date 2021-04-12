import { Redirect, Route, Switch } from 'react-router-dom';
import React from 'react';
import Tags from 'pages/tags';
import Tasks from 'pages/tasks';
import Projects from 'pages/projects';
import AddTask from 'pages/addTask';
import AddTag from 'pages/tags/views/add_tag';
import TasksInfo from 'pages/tasks/views/task_modal';
import Unknown from 'pages/unknown';
import Home from 'pages/home';
import Confirmation from 'pages/success';
import SignIn from 'pages/sign_in';
import SingUp from 'pages/sign_up';

const PAGES = [
	{ name: 'projects', link: '/projects', component: Projects },
	{ name: 'tasks', link: '/view-tasks', component: Tasks },
	{ name: 'tags', link: '/tags', component: Tags },
	{ name: 'new task', link: '/tasks/add-task', component: AddTask },
	{ name: 'edit task', link: '/tasks/:id/edit-task', component: AddTask },
	{ name: 'new tag', link: '/tags/form', component: AddTag },
	{ name: 'tasks-info', link: '/tasks/info', component: TasksInfo },
	{ name: 'Home', link: '/', component: Home },
	{ name: 'confirmation-page', link: '/tasks/save-success', component: Confirmation },
	{ name: 'sign-in', link: '/sign-in', component: SignIn },
	{ name: 'sign-up', link: '/sign-up', component: SingUp },
];

/**
 * Switch the routes
 *
 * @returns {object} route selected
 */
function FactoryRoutes(props) {
	const { pathname, loggedIn } = props;
	const next = loggedIn && (pathname === '/sign-in' || pathname === '/sign-up') ? '/' : pathname;
	let routes = PAGES.map((page) => (
		<Route key={page.name} path={page.link} exact component={page.component} />
	));

	return (
		<>
			<Switch>
				{routes}
				<Route render={Unknown} />
			</Switch>
			{loggedIn ? <Redirect to={next} /> : pathname === '/' ? 
													<Redirect to={'/sign-in'} /> :
													<Redirect to={pathname} />
													}
		</>
	);
}

export default FactoryRoutes;
