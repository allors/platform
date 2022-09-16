// <copyright file="Like.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Data;

using Meta;

public class Like : IRolePredicate
{
    public Like(IRoleType roleType = null) => this.RoleType = roleType;

    public string Value { get; set; }

    public string Parameter { get; set; }

    public IRoleType RoleType { get; set; }

    bool IPredicate.ShouldTreeShake(IArguments arguments) => ((IPredicate)this).HasMissingArguments(arguments);

    bool IPredicate.HasMissingArguments(IArguments arguments) => this.Parameter != null && arguments?.HasArgument(this.Parameter) != true;

    void IPredicate.Build(ITransaction transaction, IArguments arguments, Database.ICompositePredicate compositePredicate)
    {
        var value = this.Parameter != null ? (string)arguments.ResolveUnit(this.RoleType.ObjectType.Tag, this.Parameter) : this.Value;
        compositePredicate.AddLike(this.RoleType, value);
    }

    public void Accept(IVisitor visitor) => visitor.VisitLike(this);
}
