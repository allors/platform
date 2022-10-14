// <copyright file="ITrace.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Adapters.Sql.Tracing;

using System.Text;
using Allors.Database.Adapters.Tracing;
using Allors.Database.Meta;

public sealed class SqlGetCompositesAssociationEvent : Event
{
    public SqlGetCompositesAssociationEvent(ITransaction transaction) : base(transaction)
    {
    }

    public Reference Role { get; set; }

    public IAssociationType AssociationType { get; set; }

    protected override void ToString(StringBuilder builder) => _ = builder
        .Append('[')
        .Append(this.Role.ObjectId)
        .Append(" : ")
        .Append(this.AssociationType.Name)
        .Append("] ");
}
