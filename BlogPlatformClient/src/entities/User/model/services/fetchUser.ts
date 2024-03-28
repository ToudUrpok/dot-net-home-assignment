import { createAsyncThunk } from '@reduxjs/toolkit'
import { User } from '../types/user'
import { selectUserAuthToken, actions as userActions } from '../slice/userSlice'
import { $API } from '../../../../shared/api/APIInstance'
import { AppDispatch, RootState } from '../../../../app/store/store'

export const fetchUser = createAsyncThunk<
    User, 
    undefined, 
    { rejectValue: Error, dispatch: AppDispatch, state: RootState }
>(
    'user/fetchUser',
    async (_, thunkAPI) => {
        try {
            const token = selectUserAuthToken(thunkAPI.getState())
            if (!token) {
                throw new Error()
            }

            const response = await $API.get<User>('/User', {
                headers: {
                    authorization: `Bearer ${token}`
                }
            })

            if (!response.data || Math.floor(response.status / 100) !== 2) {
                throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
            }

            return response.data
        } catch (err) {
            thunkAPI.dispatch(userActions.logOut())
            return thunkAPI.rejectWithValue(err as Error)
        }
    }
)