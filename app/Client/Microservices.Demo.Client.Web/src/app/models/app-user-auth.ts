import { IUser } from "./iuser";

export class AppUserAuth {
  constructor() {
    this.IsAuthenticated = false;
    this.BearerToken = "";
    
;
  }
  BearerToken: string;
  IsAuthenticated: boolean;
  User!: IUser;
}
