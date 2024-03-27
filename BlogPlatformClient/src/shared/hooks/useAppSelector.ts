import { StateSchema } from '../../app/store/StateSchema'
import { TypedUseSelectorHook, useSelector } from 'react-redux'

export const useAppSelector: TypedUseSelectorHook<StateSchema> = useSelector