﻿// <copyright file="Interface.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the IObjectType type.</summary>

namespace Allors.Database.Meta;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Interface : Composite, IInterface
{
    private string[] derivedWorkspaceNames;
    private HashSet<Class> subclasses;

    private HashSet<Composite> directSubtypes;
    private Class exclusiveClass;
    private HashSet<Composite> subtypes;

    protected Interface(MetaPopulation metaPopulation, Guid id, Interface[] directSupertypes, string singularName, string assignedPluralName)
        : base(metaPopulation, id, directSupertypes, singularName, assignedPluralName) =>
        metaPopulation.OnCreated(this);

    public override IEnumerable<Class> Classes => this.subclasses;

    public override IEnumerable<Composite> Subtypes => this.subtypes;

    public override Class ExclusiveClass => this.exclusiveClass;

    public override IEnumerable<string> WorkspaceNames => this.derivedWorkspaceNames;

    public override bool ExistClass => this.subclasses.Count > 0;

    IEnumerable<IComposite> IComposite.Subtypes => this.Subtypes;

    public override bool IsAssignableFrom(IComposite objectType) =>
        this.Equals(objectType) || this.subtypes.Contains(objectType);

    internal void DeriveWorkspaceNames() =>
        this.derivedWorkspaceNames = this
            .RoleTypes.SelectMany(v => v.RelationType.WorkspaceNames)
            .Union(this.AssociationTypes.SelectMany(v => v.RelationType.WorkspaceNames))
            .Union(this.MethodTypes.SelectMany(v => v.WorkspaceNames))
            .ToArray();

    internal void InitializeDirectSubtypes()
    {
        this.directSubtypes = new HashSet<Composite>(this.MetaPopulation.Composites.Where(v => v.DirectSupertypes.Contains(this)));
    }

    internal void InitializeSubclasses()
    {
        var subclasses = new HashSet<Class>();
        foreach (var subType in this.subtypes.OfType<IClass>())
        {
            subclasses.Add((Class)subType);
        }

        this.subclasses = new HashSet<Class>(subclasses);
    }

    internal void InitializeSubtypes()
    {
        var subtypes = new HashSet<Composite>();
        this.InitializeSubtypesRecursively(this, subtypes);
        this.subtypes = new HashSet<Composite>(subtypes);
    }

    internal void InitializeExclusiveSubclass() => this.exclusiveClass = this.subclasses.Count == 1 ? this.subclasses.First() : null;

    private void InitializeSubtypesRecursively(ObjectType type, ISet<Composite> subtypes)
    {
        foreach (var directSubtype in this.directSubtypes)
        {
            if (!Equals(directSubtype, type))
            {
                subtypes.Add(directSubtype);
                if (directSubtype is IInterface)
                {
                    ((Interface)directSubtype).InitializeSubtypesRecursively(this, subtypes);
                }
            }
        }
    }
}
