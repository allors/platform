// <copyright file="ContainedIn.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Request
{
    using System.Collections.Generic;
    using Allors.Workspace.Meta;
    using Allors.Workspace.Response;
    using Allors.Workspace.Request.Visitor;

    public class ContainedIn : IPropertyPredicate
    {
        public ContainedIn(IRelationEndType relationEndType = null) => this.RelationEndType = relationEndType;

        public IExtent Extent { get; set; }

        public IEnumerable<IObject> Objects { get; set; }

        public string Parameter { get; set; }

        public IRelationEndType RelationEndType { get; set; }

        public void Accept(IVisitor visitor) => visitor.VisitContainedIn(this);
    }
}
