// <copyright file="IRelationEndType.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the RoleType type.</summary>

namespace Allors.Workspace.Meta
{
    /// <summary>
    ///     A <see cref="IOperandType" /> can be a <see cref="AssociationType" /> or a <see cref="RoleType" />.
    /// </summary>
    public interface IOperandType
    {
        string OperandTag { get; }
    }
}
