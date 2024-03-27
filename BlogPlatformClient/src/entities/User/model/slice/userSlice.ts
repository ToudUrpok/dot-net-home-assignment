import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { StateSchema } from '../../../../app/store/StateSchema'
import { User } from '../types/user'
import { USER_AUTH_TOKEN } from '../../../../shared/const/localStorage'
import { fetchUser } from '../services/fetchUser'
import { UserState } from '../types/userState'

const initialState: UserState = {
    isLoading: false
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setData: (state, action: PayloadAction<User>) => {
            state.data = action.payload
        },
        setAuthToken: (state, action: PayloadAction<string>) => {
            state.authToken = action.payload
        },
        initAuth: (state) => {
            const token = localStorage.getItem(USER_AUTH_TOKEN)
            if (token) {
                state.authToken = token
            }
        },
        logOut: (state) => {
            state.authToken = undefined
            state.data = undefined
            localStorage.removeItem(USER_AUTH_TOKEN)
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUser.pending, (state) => {
            state.error = undefined
            state.isLoading = true
        })
        builder.addCase(fetchUser.fulfilled, (state, action: PayloadAction<User>) => {
            state.isLoading = false
            state.data = action.payload
        })
        builder.addCase(fetchUser.rejected, (state, action) => {
            state.isLoading = false
            state.error = action.error.message
        })
    }
})

export const selectUserData = (state: StateSchema): User | undefined => state?.user?.data
export const selectUserAuthToken = (state: StateSchema): string | undefined => state?.user?.authToken
export const { reducer, actions } = userSlice
