// <copyright file="IDomainDerivation.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the IDomainDerivation type.</summary>

namespace Allors.Database.Derivations;

using System.Collections.Generic;
using Allors.Database.Data;
using Allors.Database.Meta;

public interface IPattern
{
    IEnumerable<Node> Tree { get; }

    IComposite OfType { get; }

    IComposite ObjectType { get; }
}
