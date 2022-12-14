// <copyright file="First.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("1937b42e-954b-4ef9-bc63-5b8ae7903e9d")]

#endregion

public class First : Object, DerivationCounted
{
    #region Allors

    [Id("24886999-11f0-408f-b094-14b36ac4129b")]
    [SingleAssociation]
    [Indexed]

    #endregion

    public Second Second { get; set; }

    #region Allors

    [Id("b0274351-3403-4384-afb6-2cb49cd03893")]

    #endregion

    public bool CreateCycle { get; set; }

    #region Allors

    [Id("f2b61dd5-d30c-445a-ae7a-af1c0cc8e278")]

    #endregion

    public bool IsDerived { get; set; }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public int DerivationCount { get; set; }

    public void OnPostBuild() { }

    public void OnInit()
    {
    }

    public void OnPostDerive(OnPostDeriveInput input) { }

    #endregion
}
