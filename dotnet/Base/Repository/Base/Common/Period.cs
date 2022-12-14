// <copyright file="Period.cs" company="Allors bv">
// Copyright (c) Allors bv. All rights reserved.
// </copyright>

namespace Allors.Repository;

using System;
using Allors.Repository.Attributes;

#region Allors

[Id("80adbbfd-952e-46f3-a744-78e0ce42bc80")]

#endregion

public partial interface Period : Object
{
    #region Allors

    [Id("5aeb31c7-03d4-4314-bbb2-fca5704b1eab")]

    #endregion

    [Required]
    DateTime FromDate { get; set; }

    #region Allors

    [Id("d7576ce2-da27-487a-86aa-b0912f745bc0")]

    #endregion

    DateTime ThroughDate { get; set; }
}
