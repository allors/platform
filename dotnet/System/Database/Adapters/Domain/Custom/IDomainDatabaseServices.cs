// <copyright file="Subscriber.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the Default type.</summary>
//------------------------------------------------------------------------------------------------

namespace Allors.Database;

using Allors.Database.Meta;

public interface IDomainDatabaseServices : IDatabaseServices
{
    M M { get; }
}
