import { Comment } from '../../model/types/comment'
import { $API } from '../../../../shared/api/APIInstance'

export interface CreatePostData {
    postId: string
    text: string
}

export interface PostCommentProps {
    authToken: string
    data: CreatePostData
}

export const postComment = async ({authToken, data}: PostCommentProps) => {
    const response = await $API.post<Comment>('/Comment', data, {
        headers: {
            authorization: `Bearer ${authToken}`
        }
    })

    if (!response.data || Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }

    return response.data
}