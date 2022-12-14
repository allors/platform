// <copyright file="IConfiguration.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace
{
    using Meta;

    public interface IConfiguration
    {
        string Name { get; }

        MetaPopulation MetaPopulation { get; }

        IObjectFactory ObjectFactory { get; }
    }
}
