// <copyright file="Contains.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Data;

using Allors.Database.Meta;

public class Contains : IPropertyPredicate
{
    public Contains(IRelationEndType relationEndType = null) => this.RelationEndType = relationEndType;

    public IObject Object { get; set; }

    public string Parameter { get; set; }

    public IRelationEndType RelationEndType { get; set; }

    bool IPredicate.ShouldTreeShake(IArguments arguments) => ((IPredicate)this).HasMissingArguments(arguments);

    bool IPredicate.HasMissingArguments(IArguments arguments) => this.Parameter != null && arguments?.HasArgument(this.Parameter) != true;

    void IPredicate.Build(ITransaction transaction, IArguments arguments, Database.ICompositePredicate compositePredicate)
    {
        var containedObject = this.Parameter != null ? transaction.GetObject(arguments.ResolveObject(this.Parameter)) : this.Object;

        if (this.RelationEndType is IRoleType roleType)
        {
            compositePredicate.AddContains(roleType, containedObject);
        }
        else
        {
            var associationType = (IAssociationType)this.RelationEndType;
            compositePredicate.AddContains(associationType, containedObject);
        }
    }

    public void Accept(IVisitor visitor) => visitor.VisitContains(this);
}
