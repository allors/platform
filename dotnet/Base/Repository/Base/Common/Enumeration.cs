// <copyright file="Enumeration.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("b7bcc22f-03f0-46fd-b738-4e035921d445")]

#endregion

public partial interface Enumeration : UniquelyIdentifiable, Object
{
    #region Allors

    [Id("3d3ae4d0-bac6-4645-8a53-3e9f7f9af086")]

    #endregion

    [Indexed]
    [Required]
    [Size(256)]
    string Name { get; set; }

    #region Allors

    [Id("07e034f1-246a-4115-9662-4c798f31343f")]

    #endregion

    [SingleAssociation]
    [Indexed]
    LocalizedText[] LocalizedNames { get; set; }

    #region Allors

    [Id("f57bb62e-77a8-4519-81e6-539d54b71cb7")]

    #endregion

    [Indexed]
    bool IsActive { get; set; }
}
