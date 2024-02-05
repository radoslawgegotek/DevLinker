export type Result = {
  error: string,
  errors: { [key: string]: string }
  isSuccess: boolean
}

export type ResultValue<TValue = any> = Result & { valueResult: TValue };
