import {
    Reducer,
    ReducersMapObject,
    configureStore
} from '@reduxjs/toolkit'
import { userReducer } from '../../entities/User'
import { createReducerManager } from './reducerManager'
import { StateSchema } from './StateSchema'

export function setupStore (initialState?: ReducersMapObject<StateSchema>, asyncReducers?: ReducersMapObject<StateSchema>) {
    const staticReducers: ReducersMapObject<StateSchema> = {
        ...asyncReducers,
        user: userReducer
    }

    const reducerManager = createReducerManager(staticReducers)

    const store = configureStore({
        reducer: reducerManager.reduce as Reducer<ReducersMapObject<StateSchema>>,
        devTools: import.meta.env.DEV,
        preloadedState: initialState
    })

    // @ts-expect-error
    store.reducerManager = reducerManager

    return store
}
