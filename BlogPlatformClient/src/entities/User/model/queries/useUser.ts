import { useQuery } from '@tanstack/react-query'
import { getUser } from '../api/getUser'

export const useUser = (authToken: string) => {
    return useQuery({ queryKey: ['user'], queryFn: () => getUser(authToken) })
}