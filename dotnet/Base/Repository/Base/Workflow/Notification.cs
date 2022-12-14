// <copyright file="Notification.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using System;
using Allors.Repository.Attributes;

#region Allors

[Id("73dcdc68-7571-4ed1-86db-77c914fe2f62")]

#endregion

public partial class Notification : Deletable, Object
{
    #region Allors

    [Id("9a226bec-31b9-413e-bec1-8dcdf36fa6fb")]
    [Indexed]

    #endregion

    public UniquelyIdentifiable Target { get; set; }

    #region Allors

    [Id("50b1be30-d6a9-49e8-84da-a47647e443f0")]

    #endregion

    [Required]
    public bool Confirmed { get; set; }

    #region Allors

    [Id("70292962-9e0e-4b57-a710-c8ac34f65b11")]
    [Size(1024)]

    #endregion

    [Required]
    public string Title { get; set; }

    #region Allors

    [Id("e83600fc-5411-4c72-9903-80a3741a9165")]
    [Size(-1)]

    #endregion

    public string Description { get; set; }

    #region Allors

    [Id("458a8223-9c0f-4475-93c0-82d5cc133f1b")]
    [Derived]
    [Indexed]

    #endregion

    [Required]
    public DateTime DateCreated { get; set; }

    #region Allors

    [Id("B445FC66-27AF-4D45-ADA8-4F1409EBBE72")]

    #endregion

    public void Confirm() { }

    #region inherited

    public DelegatedAccess AccessDelegation { get; set; }
    public Revocation[] Revocations { get; set; }


    public SecurityToken[] SecurityTokens { get; set; }

    public void OnPostBuild() { }

    public void OnInit() { }

    public void OnPostDerive(OnPostDeriveInput input) { }

    public void Delete() { }

    #endregion
}
