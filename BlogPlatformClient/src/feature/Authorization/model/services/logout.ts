import { USER_AUTH_TOKEN } from "../../../../shared/const/localStorage"

export const logout = async () =>
{ 
    localStorage.removeItem(USER_AUTH_TOKEN)
}