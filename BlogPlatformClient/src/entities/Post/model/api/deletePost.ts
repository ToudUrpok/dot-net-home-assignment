import { $API } from '../../../../shared/api/APIInstance'

export interface DeletePostProps {
    authToken: string
    postId: number
}

export const deletePost = async ({authToken, postId}: DeletePostProps): Promise<boolean> => {
    const response = await $API.delete(`/Post/${postId}`, {
        headers: {
            authorization: `Bearer ${authToken}`
        }
    })

    if (Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }

    return true
}