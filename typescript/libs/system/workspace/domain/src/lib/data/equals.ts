import { RelationEndType, RoleType } from '@allors/system/workspace/meta';
import { IObject } from '../iobject';
import { IUnit } from '../types';
import { ParameterizablePredicateBase } from './parameterizable-predicate';

export interface Equals extends ParameterizablePredicateBase {
  kind: 'Equals';
  relationEndType?: RelationEndType;
  value?: IUnit;
  object?: IObject;
  objectId?: number;
  path?: RoleType;
}
