// <copyright file="IMetaObject.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Meta
{
    using System.Collections.Generic;

    /// <summary>
    ///     Base interface for Meta objects.
    /// </summary>
    public interface IMetaPopulation
    {
        IReadOnlyList<IUnit> Units { get; }

        IReadOnlyList<IInterface> Interfaces { get; }

        IReadOnlyList<IClass> Classes { get; }

        IReadOnlyList<IRelationType> RelationTypes { get; }

        IReadOnlyList<IMethodType> MethodTypes { get; }

        IReadOnlyDictionary<string, IMetaIdentifiableObject> MetaObjectByTag { get; }

        IReadOnlyList<IComposite> Composites { get; }

        IReadOnlyDictionary<string, IComposite> CompositeByLowercaseName { get; }

        IMetaIdentifiableObject FindByTag(string tag);

        IComposite FindByName(string name);
    }
}
