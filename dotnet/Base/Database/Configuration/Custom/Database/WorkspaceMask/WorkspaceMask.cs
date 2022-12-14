// <copyright file="TreeCache.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Configuration
{
    using System.Collections.Generic;
    using Allors.Database.Domain;
    using Allors.Database.Meta;

    public class WorkspaceMask : IWorkspaceMask
    {
        private readonly Dictionary<IClass, IRoleType> masks;

        public WorkspaceMask(M m) =>
            this.masks = new Dictionary<IClass, IRoleType>();

        public IDictionary<IClass, IRoleType> GetMasks(string workspaceName) => this.masks;
    }
}
