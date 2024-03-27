import { createAsyncThunk } from '@reduxjs/toolkit'
import { userActions } from '../../../../entities/User'
import { $API } from '../../../../shared/api/APIInstance'
import { USER_AUTH_TOKEN } from '../../../../shared/const/localStorage'
import { LoginFormFields } from '../types/loginFormFields'

export const login = createAsyncThunk<string, LoginFormFields, { rejectValue: Error }>(
    'login/login',
    async (loginData, thunkAPI) => {
        try {
            const response = await $API.post<string>('/Authorization', loginData)
            
            if (!response.data || Math.floor(response.status / 100) !== 2) {
                throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
            }

            localStorage.setItem(USER_AUTH_TOKEN, response.data)
            thunkAPI.dispatch(userActions.setAuthToken(response.data))

            return response.data
        } catch (err) {
            return thunkAPI.rejectWithValue(err as Error)
        }
    }
)