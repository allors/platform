import { Composite, RelationEndType } from '@allors/system/workspace/meta';
import { Node } from '../pointer/node';

export interface Select {
  relationEndType: RelationEndType;

  ofType?: Composite;

  next?: Select;

  include?: Node[];
}

export function selectLeaf(select: Select): Select {
  if (select.next) {
    return selectLeaf(select.next);
  }

  return select;
}
