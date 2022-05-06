import { writable } from 'svelte/store';

export const language = writable("en");
export const currentUser = writable(null);