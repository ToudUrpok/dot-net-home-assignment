import { $API } from '../../../../shared/api/APIInstance'
import { Post } from '../types/post'

export const fetchPosts = async () => {
    const response = await $API.get<Post[]>('/Post')
        
    if (!response.data || Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }
        
    return response.data
}