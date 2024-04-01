import { $API } from '../../../../shared/api/APIInstance'
import { USER_AUTH_TOKEN } from '../../../../shared/const/localStorage'
import { LoginFormFields } from '../types/loginFormFields'

export const postLogin = async (data: LoginFormFields): Promise<string | undefined> => {
    const response = await $API.post('/Authorization', data)

    if (!response.data || Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }

    localStorage.setItem(USER_AUTH_TOKEN, response.data)
    return response.data
}