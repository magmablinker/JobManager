<script>
	import { Router, Route, Link } from "svelte-navigator";
	import { SvelteToast } from '@zerodevx/svelte-toast';
	import { setContext, onDestroy } from 'svelte';
	import HttpService from './service/httpService';
	import 'bootstrap/dist/css/bootstrap.min.css';
	import { language, currentUser } from './service/store';
	import { useNavigate } from "svelte-navigator";

	// Components
	import JobOffers from './components/jobOffers.svelte';
	import Login from './components/login.svelte';
	import EmployerPanel from './components/employerPanel.svelte';
	import PrivateRoute from './components/routing/privateRoute.svelte';

	let currentUserValue = null;

	const currentUserSubscription = currentUser.subscribe(value => currentUserValue = value);
	
	const httpService = new HttpService();

	setContext("httpService", httpService);

	let userLanguage = "en";

	const logout = () => {
		httpService.setToken(null);

		const navigate = useNavigate();

		navigate("/");
	};
</script>

<Router>
	<main>
		<nav class="navbar navbar-expand-lg navbar-light bg-light mb-2">
			<div class="container-fluid">
				<a href="#" class="navbar-brand">Job Manager</a>
				<button
					class="navbar-toggler"
					type="button"
					data-mdb-toggle="collapse"
					data-mdb-target="#navbarNavAltMarkup"
					aria-controls="navbarNavAltMarkup"
					aria-expanded="false"
					aria-label="Toggle navigation">
					<i class="fas fa-bars"></i>
				</button>
				<div class="collapse navbar-collapse" id="navbarNavAltMarkup">
					<div class="navbar-nav">
						<Link class="nav-link" to="/">Home</Link>
						{#if currentUserValue == null} 
							<Link class="nav-link" to="/login">Login</Link>							
						{/if}
						{#if currentUserValue != null}
							<button class="btn btn-primary" on:click={logout}>
								Logout
							</button>
						{/if}
					</div>
					<form class="form-inline my-2">
						<select name="language" 
							id="user-language"
							class="form-control float-right"
							bind:value={userLanguage}
							on:change="{() => language.update(() => userLanguage)}">
							<option value="en">English</option>
							<option value="de">Deutsch</option>
						</select>
					</form>
				</div>
			</div>
		</nav>
		<div class="container-fluid">
		
			<Route path="/">
				<JobOffers />
			</Route>
			<Route path="/login">
				<Login />
			</Route>
			<Route path="/panel/*">
				<PrivateRoute path="employer">
					<EmployerPanel />
				</PrivateRoute>
			</Route>
		</div>
	
		<SvelteToast />
	</main>
</Router>

<style>
	main {
		height: 100%;
	}

	.container-fluid {
		height: 90%;
	}
</style>