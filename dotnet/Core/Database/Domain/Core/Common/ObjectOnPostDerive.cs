// <copyright file="ObjectOnPostDerive.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    using Allors.Database.Derivations;

    public partial record OnPostDeriveInput
    {
        public IDerivation Derivation { get; set; }
    }
}
