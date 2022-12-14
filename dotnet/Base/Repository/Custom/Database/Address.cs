// <copyright file="Address.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using Allors.Repository.Attributes;

#region Allors

[Id("130aa2ff-4f14-4ad7-8a27-f80e8aebfa00")]

#endregion

[Plural("Addresses")]
public interface Address : Object
{
    #region Allors

    [Id("36e7d935-a9c7-484d-8551-9bdc5bdeab68")]
    [Indexed]

    #endregion

    Place Place { get; set; }
}
