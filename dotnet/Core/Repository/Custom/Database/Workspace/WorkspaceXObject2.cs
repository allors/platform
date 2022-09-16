// <copyright file="Data.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Repository;

using Attributes;

#region Allors
[Id("48E065DD-B7C0-4B94-AC7A-656BCBE1B04A")]
#endregion
[Workspace(Workspaces.X)]
public class WorkspaceXObject2 : Object
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
