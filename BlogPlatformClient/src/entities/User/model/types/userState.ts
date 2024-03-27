import { User } from '../types/user'

export interface UserState {
    data?: User
    authToken?: string
    isLoading: boolean
    error?: string
}