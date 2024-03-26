import { useQuery } from '@tanstack/react-query'
import { $API } from '../../../../shared/api/APIInstance'
import { Post } from '../types/post'

export const usePosts = () => {
    return useQuery({ queryKey: ['posts'], queryFn: fetchPosts })
}

const fetchPosts = async () => {
    try {
        const response = await $API.get<Post[]>('/Post')
        
        if (!response.data) {
            switch (response.status) {
                case 404:
                    return []
                case 400:
                default:
                    console.log(response)
            }
        }

        return response.data
    } catch (error) {
        console.log(error)
    }
}