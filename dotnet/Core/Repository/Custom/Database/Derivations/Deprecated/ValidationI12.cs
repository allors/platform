// <copyright file="ValidationI12.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using System;
using Attributes;

#region Allors
[Id("d61872ee-3778-47e8-8931-003f3f48cbc5")]
#endregion
public interface ValidationI12 : Object
{
    #region Allors
    [Id("0b89b096-a69a-495c-acfe-b24a9b27e320")]
    #endregion

    Guid UniqueId { get; set; }
}
