// <copyright file="PreparedExtents.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Configuration
{
    using System;
    using System.Collections.Concurrent;
    using Allors.Database.Data;
    using Allors.Database.Meta;
    using Allors.Database.Services;

    public class PreparedExtents : IPreparedExtents
    {
        public PreparedExtents(M m)
        {
            this.M = m;
            this.ExtentById = new ConcurrentDictionary<Guid, IExtent>();
        }

        public M M { get; }

        public ConcurrentDictionary<Guid, IExtent> ExtentById { get; }

        public static Guid OrganizationByName => new Guid("5D8D1C36-4ABD-4969-BCDC-4B6FA2454D65");

        public IExtent Get(Guid id)
        {
            if (id == OrganizationByName)
            {
                return new Extent(this.M.Organization) { Predicate = new Equals(this.M.Organization.Name) { Parameter = "name" } };
            }

            this.ExtentById.TryGetValue(id, out var extent);
            return extent;
        }
    }
}
