// <copyright file="BuilderTest.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain.Tests
{
    using Xunit;

    public class BuilderTest : DomainTest, IClassFixture<Fixture>
    {
        public BuilderTest(Fixture fixture) : base(fixture) { }

        [Fact]
        public void BaseOnPostBuild()
        {
            var person = this.Transaction.Build<Person>();

            Assert.True(person.ExistUniqueId);
        }
    }
}
