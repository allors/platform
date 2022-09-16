// <copyright file="RoleContains.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Adapters.Memory;

using System.Linq;
using Meta;

internal sealed class RoleContains : Predicate
{
    private readonly IObject containedObject;
    private readonly IRoleType roleType;

    internal RoleContains(ExtentFiltered extent, IRoleType roleType, IObject containedObject)
    {
        extent.CheckForRoleType(roleType);
        PredicateAssertions.ValidateRoleContains(roleType, containedObject);

        this.roleType = roleType;
        this.containedObject = containedObject;
    }

    internal override ThreeValuedLogic Evaluate(Strategy strategy) =>
        strategy.GetCompositesRole<IObject>(this.roleType).Contains(this.containedObject) ? ThreeValuedLogic.True : ThreeValuedLogic.False;
}
