import { HttpClient } from '@angular/common/http';
import { WorkspaceService } from '@allors/base/workspace/angular/foundation';

import { AppClient } from './app.client';
import { Configuration } from '@allors/system/workspace/domain';
import { LazyMetaPopulation } from '@allors/system/workspace/meta-json';
import { PrototypeObjectFactory } from '@allors/system/workspace/adapters';
import { DatabaseConnection } from '@allors/system/workspace/adapters-json';
import { data } from '@allors/default/workspace/meta-json';
import { AppContext } from './app.context';

export function config(
  workspaceService: WorkspaceService,
  httpClient: HttpClient,
  baseUrl: string,
  authUrl: string
) {
  const angularClient = new AppClient(httpClient, baseUrl, authUrl);

  const metaPopulation = new LazyMetaPopulation(data);

  let nextId = -1;

  const configuration: Configuration = {
    name: 'Default',
    metaPopulation,
    objectFactory: new PrototypeObjectFactory(metaPopulation),
    idGenerator: () => nextId--,
  };

  const database = new DatabaseConnection(configuration, angularClient);
  const workspace = database.createWorkspace();
  workspaceService.workspace = workspace;

  workspaceService.contextBuilder = () => new AppContext(workspaceService);
}
