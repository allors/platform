﻿// <copyright file="IMethodType.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the RoleType type.</summary>

namespace Allors.Database.Meta;

public interface IMethodType : IMetaIdentifiableObject, IOperandType
{
    string[] AssignedWorkspaceNames { get; }

    IComposite ObjectType { get; }

    string Name { get; }

    IRecord Input { get; }

    IRecord Output { get; }
}
