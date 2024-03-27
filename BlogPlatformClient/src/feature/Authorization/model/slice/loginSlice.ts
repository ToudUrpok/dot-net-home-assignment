import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { LoginState } from '../types/loginState'
import { StateSchema } from '../../../../app/store/StateSchema'
import { login } from '../services/login'

const initialState: LoginState = {
    isLoading: false
}

const loginSlice = createSlice({
    name: 'login',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(login.pending, (state) => {
            state.error = undefined
            state.isLoading = true
        })
        builder.addCase(login.fulfilled, (state) => {
            state.isLoading = false
        })
        builder.addCase(login.rejected, (state, action) => {
            state.isLoading = false
            state.error = action.error.message
        })
    }
})

export const selectLoginState = (state: StateSchema): LoginState | undefined => state.login
export const selectLoginIsLoading = (state: StateSchema): boolean => state.login?.isLoading ?? false
export const selectLoginError = (state: StateSchema): string | undefined => state.login?.error

export const { reducer, actions } = loginSlice
