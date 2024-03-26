import axios from 'axios'
import { USER_AUTH_TOKEN } from '../const/localStorage'

export const $authAPI = axios.create({
    baseURL: __API_URL__,
    headers: {
        authorization: localStorage.getItem(USER_AUTH_TOKEN)
    }
})
