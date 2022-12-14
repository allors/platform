// <copyright file="Pull.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Request
{
    using System;
    using System.Collections.Generic;
    using Allors.Workspace.Meta;
    using Allors.Workspace.Response;
    using Allors.Workspace.Request.Visitor;

    public class PullRequest : IRequest, IVisitable
    {
        public Guid? ExtentRef { get; set; }

        public IExtent Extent { get; set; }

        public IObjectType ObjectType { get; set; }

        public IObject Object { get; set; }

        public long? ObjectId { get; set; }

        public Result[] Results { get; set; }

        public IDictionary<string, object> Arguments { get; set; }

        public void Accept(IVisitor visitor) => visitor.VisitPull(this);
    }
}
