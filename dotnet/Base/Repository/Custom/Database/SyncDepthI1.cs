// <copyright file="SyncDepthI1.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("FBC46730-B636-4333-989C-53D5F76A32A0")]

#endregion

public interface SyncDepthI1 : Object, DerivationCounted
{
    #region Allors

    [Id("BC3991AE-475D-4CA2-A8E1-6DF5CCC65CE0")]
    [Indexed]

    #endregion

    [SingleAssociation]
    [Derived]
    SyncDepth2 SyncDepth2 { get; set; }

    #region Allors

    [Id("DCD1D766-99F4-4DD4-A8F8-F1BEBBB2BBB5")]

    #endregion

    [Required]
    int Value { get; set; }
}
