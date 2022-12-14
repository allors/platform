// <copyright file="PrintDocument.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("6161594B-8ACF-4DFA-AE6D-A9BC96040714")]

#endregion

public partial class PrintDocument : Deletable, Object
{
    #region Allors

    [Id("4C5C2727-908C-4FB2-9EB5-DA31837422FC")]
    [Indexed]

    #endregion

    [SingleAssociation]
    public Media Media { get; set; }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public void OnPostBuild() { }

    public void OnInit()
    {
    }

    public void OnPostDerive(OnPostDeriveInput input) { }

    public void Delete() { }

    #endregion
}
