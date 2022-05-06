<script>
    import { getContext } from 'svelte';
    import { success, failure } from '../service/toastService';
    import jwt_decode from "jwt-decode";
    import { useNavigate } from "svelte-navigator";

    const navigate = useNavigate();

    const httpService = getContext("httpService");

    let email = "";
    let password = "";

    const login = () => {
        httpService.login({
            emailAddress: email,
            password: password
        })
        .then(response => {
            success("The login has been successful");
            httpService.setToken(response.data.jwtToken);

            let decoded = jwt_decode(response.data.jwtToken);
            let currentUserType = decoded['userType'];

            navigate(`/panel/${currentUserType.toLowerCase()}`);
        });
    };

</script>

<div class="row h-100">
    <div class="col-12 d-flex align-items-center justify-content-center">
        <div class="card">
            <div class="card-header">
                <h4>Login</h4>
            </div>
            <div class="card-body">
                <form onsubmit="event.preventDefault();">
                    <div class="form-group">
                        <label for="email">Email-Address</label>
                        <input type="email" class="form-control" name="email" bind:value={email} />
                    </div>
                    <div class="form-group">
                        <label for="username">Password</label>
                        <input type="password" class="form-control" name="password" bind:value={password} />
                    </div>
                    <div class="form-group mt-2">
                        <button class="btn btn-primary" on:click={login}>
                            Login
                        </button>
                    </div>
                </form>  
            </div>
        </div>
    </div>
</div>