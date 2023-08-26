import { ICover } from "./icover";
import { IQuestion } from "./iquestion";

export interface IProduct {
  Code: string;
  Name: string;
  Image: string;
  Description: string;
  Covers: Array<ICover>;
  Questions: Array<IQuestion>;
  MaxNumberOfInsured: number;
}

export interface IPolicy {
  policyId: number;
  number: string;
  agentLogin: string;
  policyStatusId: number;
  creationDate: Date | null;
  productCode: string;
  productName: string;
  productImage: string;
  productDescription: string;
  maxNumberOfInsured: number;
  productStatusId: number;
}