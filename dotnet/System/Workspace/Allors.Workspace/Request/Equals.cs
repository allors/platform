// <copyright file="Equals.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Request
{
    using Meta;
    using Response;
    using Visitor;

    public class Equals : IPropertyPredicate
    {
        public Equals(IPropertyType propertyType = null) => this.PropertyType = propertyType;

        public IObject Object { get; set; }

        public object Value { get; set; }

        public RoleType Path { get; set; }

        public string Parameter { get; set; }

        /// <inheritdoc />
        public IPropertyType PropertyType { get; set; }

        public void Accept(IVisitor visitor) => visitor.VisitEquals(this);
    }
}
