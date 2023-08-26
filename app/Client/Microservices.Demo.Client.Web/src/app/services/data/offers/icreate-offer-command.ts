import { IQuestionAnswer } from "../../../models/iquestion-answer";

export interface ICreateOfferCommand {
  ProductCode: string;
  PolicyFrom: Date;
  PolicyTo: Date;
  SelectedCovers: string[];
  Answers: IQuestionAnswer[];
}
