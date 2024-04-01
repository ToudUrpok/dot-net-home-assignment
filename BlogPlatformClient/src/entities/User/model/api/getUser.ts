import { User } from "../../model/types/user"
import { $API } from "../../../../shared/api/APIInstance"

export const getUser = async (authToken: string) => {
    const response = await $API.get<User>('/User', {
        headers: {
            authorization: `Bearer ${authToken}`
        }
    })

    if (!response.data || Math.floor(response.status / 100) !== 2) {
        throw new Error(`Status: ${response.status}\nStatus Text:${response.statusText}\nData:${response.data}`)
    }

    return response.data
}