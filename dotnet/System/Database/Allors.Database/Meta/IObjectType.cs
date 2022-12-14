// <copyright file="IObjectType.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the IObjectType type.</summary>

namespace Allors.Database.Meta;

using System;

public interface IObjectType : IDataType, IComparable<IObjectType>
{
    string SingularName { get; }

    string AssignedPluralName { get; }

    string PluralName { get; }

    bool IsUnit { get; }

    bool IsComposite { get; }

    bool IsInterface { get; }

    bool IsClass { get; }
}
