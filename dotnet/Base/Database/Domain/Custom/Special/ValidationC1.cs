// <copyright file="ValidationC1.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    public partial class ValidationC1
    {
        public void CustomOnPostDerive(ObjectOnPostDerive method)
        {
            var derivation = method.Input.Derivation;

            derivation.Validation.AssertIsUnique(derivation.ChangeSet, this, this.M.ValidationC1.UniqueId);
        }
    }
}
