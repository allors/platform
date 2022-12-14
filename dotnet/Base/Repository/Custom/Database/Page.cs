// <copyright file="Page.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using System;
using Allors.Repository.Attributes;

#region Allors

[Id("0777C78C-CB50-4FDD-8386-5BCEC00B208C")]

#endregion

public class Page : UniquelyIdentifiable
{
    #region Allors

    [Id("9B2F32B4-DF88-41DA-AE4C-A7A8D4232C1C")]
    [Indexed]

    #endregion

    public string Name { get; set; }

    #region Allors

    [Id("E3117BBB-3B1E-465A-8DD0-CC5FE3A5A905")]
    [Indexed]

    #endregion

    [SingleAssociation]
    public Media Content { get; set; }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public Guid UniqueId { get; set; }

    public void OnPostBuild() { }

    public void OnInit() { }

    public void OnPostDerive(OnPostDeriveInput input) { }

    #endregion
}
