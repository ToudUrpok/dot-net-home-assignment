export type {
    User
} from './model/types/user'

export type { UserState } from './model/types/userState'

export {
    reducer as userReducer,
    actions as userActions,
    selectUserState,
    selectUserData,
    selectUserAuthToken,
    selectUserIsLoading,
    selectUserError
} from './model/slice/userSlice'

export { fetchUser } from './model/services/fetchUser'
export { loginUser } from './model/services/loginUser'