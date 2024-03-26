import axios from 'axios'

export const $API = axios.create({
    baseURL: __API_URL__
})
