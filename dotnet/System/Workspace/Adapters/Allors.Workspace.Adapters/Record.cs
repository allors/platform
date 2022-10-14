// <copyright file="DatabaseRecord.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters
{
    using Allors.Workspace.Meta;

    public abstract class Record
    {
        protected Record(Class @class, long id, long version)
        {
            this.Class = @class;
            this.Id = id;
            this.Version = version;
        }

        public Class Class { get; }

        public long Id { get; }

        public long Version { get; }

        public abstract object GetRole(RoleType roleType);

        public abstract bool IsPermitted(long permission);
    }
}
