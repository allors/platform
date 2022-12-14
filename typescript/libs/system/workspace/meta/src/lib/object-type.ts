import { MetaObject } from './meta-object';

export interface ObjectType extends MetaObject {
  singularName: string;
  pluralName: string;
  isUnit: boolean;
  isComposite: boolean;
  isInterface: boolean;
  isClass: boolean;
}
