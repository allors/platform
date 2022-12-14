// <copyright file="Subdependee.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("46a437d1-455b-4ddd-b83c-068938c352bd")]

#endregion

public class Subdependee : Object
{
    #region Allors

    [Id("194930f9-9c3f-458d-93ec-3d7bea4cd538")]

    #endregion

    public int Subcounter { get; set; }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }


    public void OnPostBuild() { }

    public void OnInit()
    {
    }

    public void OnPostDerive(OnPostDeriveInput input) { }

    #endregion
}
