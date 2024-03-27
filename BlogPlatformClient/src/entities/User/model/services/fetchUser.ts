import { createAsyncThunk } from '@reduxjs/toolkit'
import { User } from '../types/user'
import { actions as userActions } from '../slice/userSlice'
import { $authAPI } from '../../../../shared/api/authorizedAPIInstance'

export const fetchUser = createAsyncThunk<User, object | undefined, { rejectValue: Error }>(
    'user/fetchUser',
    async (_, thunkAPI) => {
        try {
            const response = await $authAPI.get<User>('/User')
            
            if (response.status === 401) {
                thunkAPI.dispatch(userActions.logOut())
            }

            if (!response.data || Math.floor(response.status / 100) !== 2) {
                throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
            }

            return response.data
        } catch (err) {
            return thunkAPI.rejectWithValue(err as Error)
        }
    }
)