// <copyright file="Data.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Repository
{
    using Attributes;
    using static Workspaces;

    #region Allors
    [Id("04B46AEE-10E9-493B-9F12-809460E341C7")]
    #endregion
    [Workspace(X, Y)]
    public class WorkspaceXYObject2 : Object
    {
        #region inherited

        public DelegatedAccess AccessDelegation { get; set; }
        public Revocation[] Revocations { get; set; }
        

        public SecurityToken[] SecurityTokens { get; set; }

        public void OnPostBuild() { }

        public void OnInit()
        {
        }

        public void OnPostDerive() { }

        #endregion
    }
}
