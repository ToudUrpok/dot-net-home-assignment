import { useQuery } from '@tanstack/react-query'
import { fetchPosts } from '../api/fetchPosts'

export const usePosts = () => {
    return useQuery({ queryKey: ['posts'], queryFn: fetchPosts })
}