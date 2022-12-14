// <copyright file="Security.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    using Allors.Database.Meta;

    public partial class Security
    {
        public void GrantOperations(IObjectType objectType, params Operations[] operations) => this.Grant(Roles.OperationsId, objectType, operations);

        public void GrantOperations(IObjectType objectType, IOperandType operandType, params Operations[] operations) => this.Grant(Roles.OperationsId, objectType, operandType, operations);

        public void GrantProcurement(IObjectType objectType, params Operations[] operations) => this.Grant(Roles.ProcurementId, objectType, operations);

        public void GrantProcurement(IObjectType objectType, IOperandType operandType, params Operations[] operations) => this.Grant(Roles.ProcurementId, objectType, operandType, operations);

        public void GrantSales(IObjectType objectType, params Operations[] operations) => this.Grant(Roles.SalesId, objectType, operations);

        public void GrantSales(IObjectType objectType, IOperandType operandType, params Operations[] operations) => this.Grant(Roles.SalesId, objectType, operandType, operations);

        private void CustomOnPostSetup()
        {
            // Default access policy
            var security = new Security(this.transaction);

            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            foreach (var @class in this.transaction.Database.MetaPopulation.Classes)
            {
                security.GrantAdministrator(@class, full);
                security.GrantCreator(@class, full);
                security.GrantGuest(@class, Operations.Read);
            }
        }

        private void CustomOnPreSetup()
        {
        }
    }
}
