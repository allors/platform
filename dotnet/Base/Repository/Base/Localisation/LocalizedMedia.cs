// <copyright file="LocalizedMedia.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("2288E1F3-5DC5-458B-9F5E-076F133890C0")]

#endregion

public partial class LocalizedMedia : Localized, Deletable
{
    #region Allors

    [Id("B6AE19AE-76BF-4B84-9CBE-176217D94B9E")]
    [Indexed]

    #endregion

    [SingleAssociation]
    public Media Media { get; set; }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public Locale Locale { get; set; }

    public void OnPostBuild() { }

    public void OnInit()
    {
    }

    public void OnPostDerive(OnPostDeriveInput input) { }

    public void Delete() { }

    #endregion
}
