import { useStore } from 'react-redux'
import { ManagedReduxStore, StateSchemaKey } from '../../app/store/StateSchema'
import { useEffect } from 'react'
import { useAppDispatch } from './useAppDispatch'
import { Reducer } from '@reduxjs/toolkit'

export type ReducersList = {
    [reducerKey in StateSchemaKey]?: Reducer
}

export function useDynamicReducer (reducers: ReducersList, removeAfterUnmount?: boolean): void {
    const dispatch = useAppDispatch()
    const store = useStore() as ManagedReduxStore

    useEffect(() => {
        Object.entries(reducers).forEach(([reducerKey, reducer]) => {
            store.reducerManager.add(reducerKey as StateSchemaKey, reducer)
            dispatch({ type: `@INIT ${reducerKey} reducer` })
        })

        return () => {
            if (removeAfterUnmount) {
                Object.entries(reducers).forEach(([reducerKey]) => {
                    store.reducerManager.remove('login')
                    dispatch({ type: `@DESTROY ${reducerKey} reducer` })
                })
            }
        }
    }, [])
}
