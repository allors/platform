// <copyright file="IInvokeResult.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Response
{
    public interface IInvokeResult : IResult
    {
        IRecord Output { get; }
    }
}
