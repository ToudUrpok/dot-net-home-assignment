import {
    EnhancedStore,
    ReducersMapObject,
    Reducer,
    UnknownAction
} from '@reduxjs/toolkit'

import { UserState } from '../../entities/User'
import { LoginState } from '../../feature/Authorization'
import { setupStore } from './store'

export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']

export interface StateSchema {
    user: UserState
    login?: LoginState
}

export type StateSchemaKey = keyof StateSchema

export interface ReducerManager {
    getReducerMap: () => ReducersMapObject<StateSchema>
    reduce: (state: StateSchema, action: UnknownAction) => ReducersMapObject<StateSchema>
    add: (key: StateSchemaKey, reducer: Reducer) => void
    remove: (key: StateSchemaKey) => void
}

export interface ManagedReduxStore extends EnhancedStore<StateSchema> {
    reducerManager: ReducerManager
}
