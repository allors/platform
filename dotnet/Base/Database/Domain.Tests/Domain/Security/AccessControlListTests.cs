// <copyright file="AccessControlListTests.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain.Tests
{
    using Allors.Database.Meta;
    using Xunit;

    public class AccessControlListTests : DomainTest, IClassFixture<Fixture>
    {
        public AccessControlListTests(Fixture fixture) : base(fixture) { }

        public override Config Config => new Config { SetupSecurity = true };

        [Fact]
        public void GivenAnAuthenticationPopulationWhenCreatingAnAccessListForGuestThenPermissionIsDenied()
        {
            this.Transaction.Derive();
            this.Transaction.Commit();

            var sessions = new[] { this.Transaction };
            foreach (var session in sessions)
            {
                session.Commit();

                var guest = new AutomatedAgents(this.Transaction).Guest;
                var acls = new DatabaseAccessControl(this.Security, guest);
                foreach (Object aco in (IObject[])session.Extent(this.M.Organization))
                {
                    // When
                    var accessList = acls[aco];

                    // Then
                    Assert.False(accessList.CanExecute(this.M.Organization.JustDoIt));
                }

                session.Rollback();
            }
        }

        private Permission FindPermission(RoleType roleType, Operations operation)
        {
            var objectType = (Class)roleType.AssociationType.ObjectType;
            return new Permissions(this.Transaction).Get(objectType, roleType, operation);
        }
    }
}
