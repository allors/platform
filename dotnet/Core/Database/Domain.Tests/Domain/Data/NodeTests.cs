// <copyright file="FilterTests.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>
//   Defines the ApplicationTests type.
// </summary>

namespace Allors.Database.Domain.Tests
{
    using System.Linq;
    using Allors.Database.Data;
    using Xunit;

    public class NodeTests : DomainTest, IClassFixture<Fixture>
    {
        public NodeTests(Fixture fixture) : base(fixture) { }

        [Fact]
        public void Self()
        {
            var m = this.M;

            var node = m.UserGroup.Members.Node();

            Assert.Equal(m.UserGroup.Members, node.RelationEndType);
            Assert.Empty(node.Nodes);
        }

        [Fact]
        public void Child()
        {
            var m = this.M;

            var node = m.UserGroup.Members.Node(v => v.User.UniqueId.Node());

            Assert.Equal(m.UserGroup.Members, node.RelationEndType);
            Assert.Single(node.Nodes);

            var child = node.Nodes.First();

            Assert.Equal(m.User.UniqueId, child.RelationEndType);
            Assert.Empty(child.Nodes);
        }

        [Fact]
        public void ChildrenArray()
        {
            var m = this.M;

            var node = m.UserGroup.Members.Node(v => new[]
            {
                v.User.UniqueId.Node(),
                v.User.SecurityTokens.Node(),
            });

            Assert.Equal(m.UserGroup.Members, node.RelationEndType);
            Assert.Equal(2, node.Nodes.Length);

            var uniqueIdChild = node.Nodes.First(v => v.RelationEndType.Equals(m.User.UniqueId));

            Assert.NotNull(uniqueIdChild);
            Assert.Empty(uniqueIdChild.Nodes);

            var securityTokens = node.Nodes.First(v => v.RelationEndType.Equals(m.User.SecurityTokens));

            Assert.NotNull(securityTokens);
            Assert.Empty(securityTokens.Nodes);

        }

        [Fact]
        public void ChildrenRest()
        {
            var m = this.M;

            var node = m.UserGroup.Members.Node(
                v => v.User.UniqueId.Node(),
                v => v.User.SecurityTokens.Node());

            Assert.Equal(m.UserGroup.Members, node.RelationEndType);
            Assert.Equal(2, node.Nodes.Length);

            var uniqueIdChild = node.Nodes.First(v => v.RelationEndType.Equals(m.User.UniqueId));

            Assert.NotNull(uniqueIdChild);
            Assert.Empty(uniqueIdChild.Nodes);

            var securityTokens = node.Nodes.First(v => v.RelationEndType.Equals(m.User.SecurityTokens));

            Assert.NotNull(securityTokens);
            Assert.Empty(securityTokens.Nodes);
        }
    }
}
