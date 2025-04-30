export interface UserLogged {
    id: string
    unique_name: string
    role: string
    nbf: number
    exp: number
    iat: number
}