// <copyright file="UserPasswordReset.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>


namespace Allors.Repository;

using Attributes;


#region Allors
[Id("2e5cd966-d85d-4ad8-ba2a-48fd0c2894dd")]
#endregion
public interface UserPasswordReset
{
    #region Allors
    [Id("1a03a20b-9cfe-4052-807b-2780ef81cffb")]
    [Size(256)]
    #endregion
    
    string InExistingUserPassword { get; set; }

    #region Allors
    [Id("DCE0EA9D-105B-4E46-A22E-9B02C28DA8DB")]
    [Size(256)]
    #endregion
    
    string InUserPassword { get; set; }
}
