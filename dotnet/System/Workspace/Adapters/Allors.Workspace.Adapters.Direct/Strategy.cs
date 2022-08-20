// <copyright file="Object.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters.Direct
{
    using Meta;

    public sealed class Strategy : Adapters.Strategy
    {
        internal Strategy(Adapters.Workspace session, Class @class, long id) : base(session, @class, id) => this.DatabaseOriginState = new DatabaseOriginState(this, session.WorkspaceConnection.DatabaseConnection.GetRecord(this.Id));

        internal Strategy(Adapters.Workspace session, Adapters.DatabaseRecord databaseRecord) : base(session, databaseRecord) => this.DatabaseOriginState = new DatabaseOriginState(this, databaseRecord);
    }
}