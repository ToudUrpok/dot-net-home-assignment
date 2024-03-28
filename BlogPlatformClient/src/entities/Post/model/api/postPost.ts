import { $API } from '../../../../shared/api/APIInstance'
import { CreatePostFormFields } from '../types/createPostFormFields'
import { Post } from '../types/post'

export interface PostPostProps {
    authToken: string
    data: CreatePostFormFields
}

export const postPost = async ({authToken, data}: PostPostProps): Promise<Post | undefined> => {
    const response = await $API.post<Post>('/Post', data, {
        headers: {
            authorization: `Bearer ${authToken}`
        }
    })

    if (!response.data || Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }

    return response.data
}