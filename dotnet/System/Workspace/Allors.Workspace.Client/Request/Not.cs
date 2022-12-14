// <copyright file="Not.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Request
{
    using Allors.Workspace.Request.Visitor;

    public class Not : ICompositePredicate
    {
        public Not(IPredicate operand = null) => this.Operand = operand;

        public IPredicate Operand { get; set; }

        void IPredicateContainer.AddPredicate(IPredicate predicate) => this.Operand = predicate;

        public void Accept(IVisitor visitor) => visitor.VisitNot(this);
    }
}
