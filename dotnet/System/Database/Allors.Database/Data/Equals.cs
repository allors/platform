// <copyright file="Equals.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Data;

using Allors.Database.Meta;

public class Equals : IPropertyPredicate
{
    public Equals(IRelationEndType relationEndType = null) => this.RelationEndType = relationEndType;

    public IObject Object { get; set; }

    public object Value { get; set; }

    public IRoleType Path { get; set; }

    public string Parameter { get; set; }

    /// <inheritdoc />
    public IRelationEndType RelationEndType { get; set; }

    bool IPredicate.ShouldTreeShake(IArguments arguments) => ((IPredicate)this).HasMissingArguments(arguments);

    bool IPredicate.HasMissingArguments(IArguments arguments) => this.Parameter != null && arguments?.HasArgument(this.Parameter) != true;

    /// <inheritdoc />
    void IPredicate.Build(ITransaction transaction, IArguments arguments, Database.ICompositePredicate compositePredicate)
    {
        switch (this.RelationEndType)
        {
            case null:
            {
                var equals = this.Parameter != null ? transaction.GetObject(arguments.ResolveObject(this.Parameter)) : this.Object;
                if (equals != null)
                {
                    compositePredicate.AddEquals(this.Object);
                }

                break;
            }

            case IRoleType roleType when roleType.ObjectType.IsUnit:
            {
                var equals = this.Path ??
                             (this.Parameter != null ? arguments.ResolveUnit(roleType.ObjectType.Tag, this.Parameter) : this.Value);
                if (equals != null)
                {
                    compositePredicate.AddEquals(roleType, equals);
                }

                break;
            }

            case IRoleType roleType:
            {
                var equals = this.Parameter != null ? transaction.GetObject(arguments.ResolveObject(this.Parameter)) : this.Object;
                if (equals != null)
                {
                    compositePredicate.AddEquals(roleType, equals);
                }

                break;
            }
            default:
            {
                var associationType = (IAssociationType)this.RelationEndType;
                var equals = this.Parameter != null ? transaction.GetObject(arguments.ResolveObject(this.Parameter)) : this.Object;
                if (equals != null)
                {
                    compositePredicate.AddEquals(associationType, equals);
                }

                break;
            }
        }
    }

    public void Accept(IVisitor visitor) => visitor.VisitEquals(this);
}
