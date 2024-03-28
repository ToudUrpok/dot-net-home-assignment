import { createAsyncThunk } from '@reduxjs/toolkit'
import { $API } from '../../../../shared/api/APIInstance'
import { AppDispatch, RootState } from '../../../../app/store/store'

export interface LoginUserProps {
    email: string
    password: string
}

export const loginUser = createAsyncThunk<
    string, 
    LoginUserProps, 
    { rejectValue: Error, dispatch: AppDispatch, state: RootState }
>(
    'user/loginUser',
    async (loginData, thunkAPI) => {
        try {
            const response = await $API.post<string>('/Authorization', loginData)
            
            if (!response.data || Math.floor(response.status / 100) !== 2) {
                throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
            }

            return response.data
        } catch (err) {
            return thunkAPI.rejectWithValue(err as Error)
        }
    }
)