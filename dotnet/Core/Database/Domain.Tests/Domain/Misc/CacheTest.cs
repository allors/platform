// <copyright file="CacheTest.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain.Tests
{
    using Xunit;

    public class CacheTest : DomainTest, IClassFixture<Fixture>
    {
        public CacheTest(Fixture fixture) : base(fixture) { }

        [Fact]
        public void Default()
        {
            var existingOrganization = BuildOrganization("existing organization");

            this.Transaction.Derive();
            this.Transaction.Commit();

            foreach (var session in new[] { this.Transaction })
            {
                session.Commit();

                var cachedOrganization = new Organizations(session).Cache[existingOrganization.UniqueId];
                Assert.Equal(existingOrganization.UniqueId, cachedOrganization.UniqueId);
                Assert.Same(session, cachedOrganization.Strategy.Transaction);

                var newOrganization = this.BuildOrganization("new organization");
                cachedOrganization = new Organizations(session).Cache[newOrganization.UniqueId];
                Assert.Equal(newOrganization.UniqueId, cachedOrganization.UniqueId);
                Assert.Same(session, cachedOrganization.Strategy.Transaction);

                session.Rollback();
            }
        }
    }
}
