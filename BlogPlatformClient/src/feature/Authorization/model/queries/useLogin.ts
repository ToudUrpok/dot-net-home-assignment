import { useQuery } from '@tanstack/react-query'
import { USER_AUTH_TOKEN } from '../../../../shared/const/localStorage'

export const useLogin = () => {
    return useQuery({ queryKey: ['login'], queryFn: getAuthData })
}

const getAuthData = (): string | null => {
    const token = localStorage.getItem(USER_AUTH_TOKEN)
    return token
}