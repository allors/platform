// <copyright file="LessThan.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Data;

using Allors.Database.Meta;

public class LessThan : IRolePredicate
{
    public LessThan(IRoleType roleType = null) => this.RoleType = roleType;

    public object Value { get; set; }

    public IRoleType Path { get; set; }

    public string Parameter { get; set; }

    public IRoleType RoleType { get; set; }

    bool IPredicate.ShouldTreeShake(IArguments arguments) => ((IPredicate)this).HasMissingArguments(arguments);

    bool IPredicate.HasMissingArguments(IArguments arguments) => this.Parameter != null && arguments?.HasArgument(this.Parameter) != true;

    void IPredicate.Build(ITransaction transaction, IArguments arguments, Database.ICompositePredicate compositePredicate)
    {
        if (this.Path != null)
        {
            compositePredicate.AddLessThan(this.RoleType, this.Path);
        }
        else
        {
            var value = this.Parameter != null ? arguments.ResolveUnit(this.RoleType.ObjectType.Tag, this.Parameter) : this.Value;
            compositePredicate.AddLessThan(this.RoleType, value);
        }
    }

    public void Accept(IVisitor visitor) => visitor.VisitLessThan(this);
}
