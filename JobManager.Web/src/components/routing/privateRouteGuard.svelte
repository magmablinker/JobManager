<script>
    import { useNavigate, useLocation } from "svelte-navigator";
    import { currentUser } from "../../service/store";
  
    const navigate = useNavigate();
    const location = useLocation();
  
    $: if (!$currentUser) {
      navigate("/login", {
        state: { from: $location.pathname },
        replace: true,
      });
    }
  </script>
  
  {#if $currentUser}
    <slot />
  {/if}
  