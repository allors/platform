// <copyright file="To.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("7eb25112-4b81-4e8d-9f75-90950c40c65f")]

#endregion

public class To : Object
{
    #region Allors

    [Id("4be564ac-77bc-48b8-b945-7d39f2ea9903")]
    [Size(256)]

    #endregion

    public string Name { get; set; }

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
