import { IPerson } from "../../../models/iperson";
import { IAddress } from "../../../models/iaddress";

export interface ICreatePolicyCommand {
  OfferNumber: string;
  PolicyHolder: IPerson;
  PolicyHolderAddress: IAddress;
}
