import { useQuery } from '@tanstack/react-query'
import { fetchPostById } from '../api/fetchPostById'

export const usePost = (id: string) => {
    return useQuery({ queryKey: ['posts'], queryFn: () => fetchPostById(id) })
}