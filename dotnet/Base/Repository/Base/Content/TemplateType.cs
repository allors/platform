// <copyright file="TemplateType.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using System;
using Allors.Repository.Attributes;

#region Allors

[Id("BDABB545-3B39-4F91-9D01-A589A5DA670E")]

#endregion

public partial class TemplateType : Enumeration, Deletable
{
    #region inherited

    public Guid UniqueId { get; set; }


    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public string Name { get; set; }

    public LocalizedText[] LocalizedNames { get; set; }

    public bool IsActive { get; set; }


    public void OnPostBuild() { }

    public void OnInit()
    {
    }

    public void OnPostDerive(OnPostDeriveInput input) { }

    public void Delete() { }

    #endregion
}
