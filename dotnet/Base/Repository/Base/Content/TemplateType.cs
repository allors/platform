// <copyright file="TemplateType.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the Extent type.</summary>

namespace Allors.Repository;

using System;
using Attributes;

#region Allors
[Id("BDABB545-3B39-4F91-9D01-A589A5DA670E")]
#endregion
public class TemplateType : Enumeration, Deletable
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

    public void OnPostDerive() { }

    public void Delete() { }
    #endregion
}
