export type LoginUserForm = {
  username: string;
  password: string;
}

export type RegisterUserForm = {
  email: string,
  firstName: string,
  lastName: string,
  password: string,
  confirmPassword: string
}

export type UserInfo = {
  username: string,
  email: string
  claims: { [key: string]: string }
}
