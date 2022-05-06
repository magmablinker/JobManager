import { toast } from '@zerodevx/svelte-toast'

export const success = message => toast.push(message, {
    theme: {
        '--toastBackground': 'green',
        '--toastColor': 'white',
        '--toastProgressBackground': 'white'
    }
});
  
export const warning = message => toast.push(message, { 
    theme: {
        '--toastBackground': 'orange',
        '--toastColor': 'white',
        '--toastProgressBackground': 'orange'
    }
});

export const failure = message=> toast.push(message, { 
    theme: {
        '--toastBackground': 'red',
        '--toastColor': 'white',
        '--toastProgressBackground': 'white'
    }
});