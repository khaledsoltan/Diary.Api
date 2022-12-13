import { Link } from './Link';

export interface Diary{
  Id: string;
  DiaryName: string;
  CreatedDate: Date;
  links: Link[];
}
