<script>
    import { getContext } from 'svelte';

    const httpService = getContext("httpService");

    let jobOffers = [
        {
            id: "00000000-0000-0000-0000-000000000000",
            name: "",
            description: "",
            payPerHour: 0,
            employer: null,
            categories: []
        }
    ];

    httpService.getJobOffers()
        .then(response => {
            jobOffers = response.data.jobOffers;
        });
</script>

<div class="row">
    <div class="col-12">
        <div class="jumbotron">
            <h3>Job Offers</h3>
        </div>
    </div>
    {#each jobOffers as jobOffer}
        <div class="col-12">
            {jobOffer.name}
        </div>
    {/each}
</div>