// <copyright file="Locale.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("45033ae6-85b5-4ced-87ce-02518e6c27fd")]

#endregion

public partial class Locale : Object
{
    #region Allors

    [Id("2a2c6f77-e6a2-4eab-bfe3-5d35a8abd7f7")]
    [Size(256)]

    #endregion

    public string Name { get; set; }

    #region Allors

    [Id("d8cac34a-9bb2-4190-bd2a-ec0b87e04cf5")]

    #endregion

    [Indexed]
    [Required]
    public Language Language { get; set; }

    #region Allors

    [Id("ea778b77-2929-4ab4-ad99-bf2f970401a9")]

    #endregion

    [Indexed]
    [Required]
    public Country Country { get; set; }

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
