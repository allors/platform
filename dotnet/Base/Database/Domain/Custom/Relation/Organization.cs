// <copyright file="Organization.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the Person type.</summary>

namespace Allors.Database.Domain
{
    public partial class Organization
    {
        public void CustomToggleCanWrite(OrganizationToggleCanWrite method)
        {
            if (this.ExistRevocations)
            {
                this.RemoveRevocations();
            }
            else
            {
                var toggleRevocation = new Revocations(this.strategy.Transaction).ToggleRevocation;
                this.AddRevocation(toggleRevocation);
            }

            this.Address = this.MainAddress;
        }

        public void CustomJustDoIt(OrganizationJustDoIt method) => this.JustDidIt = true;

        public override string ToString() => this.Name;
    }
}
