import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from '../../../../app/store/store'
import { User } from '../types/user'
import { USER_AUTH_TOKEN } from '../../../../shared/const/localStorage'
import { fetchUser } from '../services/fetchUser'
import { UserState } from '../types/userState'
import { loginUser } from '../services/loginUser'
import { initUser } from '../services/initUser'

const initialState: UserState = {
    isLoading: false
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        initAuth: (state) => {
            const token = localStorage.getItem(USER_AUTH_TOKEN)
            if (token) {
                state.authToken = token
            }
        },
        logOut: (state) => {
            localStorage.removeItem(USER_AUTH_TOKEN)
            state.authToken = undefined
            state.data = undefined
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
        builder.addCase(loginUser.pending, (state) => {
            state.error = undefined
            state.isLoading = true
        })
        builder.addCase(loginUser.fulfilled, (state, action: PayloadAction<string>) => {
            state.isLoading = false
            state.authToken = action.payload
            localStorage.setItem(USER_AUTH_TOKEN, action.payload)
        })
        builder.addCase(loginUser.rejected, (state, action) => {
            state.isLoading = false
            state.error = action.error.message
        })
    }
})

export const selectUserState = (state: RootState): UserState => state.user
export const selectUserData = (state: RootState): User | undefined => state?.user?.data
export const selectUserAuthToken = (state: RootState): string | undefined => state?.user?.authToken
export const selectUserIsLoading = (state: RootState): boolean => state?.user?.isLoading
export const selectUserError = (state: RootState): string | undefined => state?.user?.error

export const { reducer, actions } = userSlice
