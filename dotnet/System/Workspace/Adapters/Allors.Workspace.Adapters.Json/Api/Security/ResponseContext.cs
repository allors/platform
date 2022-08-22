// <copyright file="RemoteResponseContext.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters.Json
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ResponseContext
    {
        private readonly WorkspaceConnection workspaceConnection;

        internal ResponseContext(WorkspaceConnection workspaceConnection)
        {
            this.workspaceConnection = workspaceConnection;

            this.MissingGrantIds = new HashSet<long>();
            this.MissingRevocationIds = new HashSet<long>();
        }

        internal HashSet<long> MissingGrantIds { get; }

        internal HashSet<long> MissingRevocationIds { get; }

        internal long[] CheckForMissingGrants(long[] value)
        {
            if (value == null)
            {
                return null;
            }

            foreach (var accessControlId in value.Where(v => !this.workspaceConnection.GrantById.ContainsKey(v)))
            {
                this.MissingGrantIds.Add(accessControlId);
            }

            return value;
        }

        internal long[] CheckForMissingRevocations(long[] value)
        {
            if (value == null)
            {
                return null;
            }

            foreach (var revocationId in value.Where(v => !this.workspaceConnection.RevocationById.ContainsKey(v)))
            {
                this.MissingRevocationIds.Add(revocationId);
            }

            return value;
        }
    }
}
