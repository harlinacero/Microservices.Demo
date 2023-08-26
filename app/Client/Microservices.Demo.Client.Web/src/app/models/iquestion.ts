import { IChoice } from "./ichoice";

export interface IQuestion {
  QuestionType: string;
  QuestionCode: string;
  Index: number;
  Text: string;
  Choices: Array<IChoice>;
}
