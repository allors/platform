// <copyright file="TransitionalExtensions.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    public static class ObjectStateExtensions
    {
        public static void BaseOnPostBuild(this ObjectState @this, ObjectOnPostBuild method) => @this.ObjectRevocation ??= @this.Transaction().Build<Revocation>();
    }
}
