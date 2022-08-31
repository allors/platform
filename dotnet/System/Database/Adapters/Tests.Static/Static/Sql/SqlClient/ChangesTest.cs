// <copyright file="ChangesTest.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Adapters.Sql.SqlClient
{
    using Xunit;

    public class ChangesTest : Adapters.ChangesTest, IClassFixture<Fixture<ChangesTest>>
    {
        private readonly Profile profile;

        public ChangesTest() => this.profile = new Profile(this.GetType().Name);

        protected override IProfile Profile => this.profile;

        public override void Dispose() => this.profile.Dispose();
    }
}
