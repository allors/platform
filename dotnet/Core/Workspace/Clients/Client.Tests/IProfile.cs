// <copyright file="Test.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Tests.Workspace
{
    using System.Threading.Tasks;
    using Allors.Workspace;
    using Xunit;

    public interface IProfile : IAsyncLifetime
    {
        IConnection CreateExclusiveWorkspaceConnection();

        IConnection CreateWorkspaceConnection();

        IConnection Connection { get; }

        Task Login(string userName);
    }
}
