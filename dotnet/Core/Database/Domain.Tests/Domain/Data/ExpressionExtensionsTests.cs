// <copyright file="FilterTests.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>
//   Defines the ApplicationTests type.
// </summary>

namespace Allors.Database.Domain.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Allors.Database.Data;
    using Allors.Database.Meta;
    using Xunit;

    public class ExpressionExtensionsTests : DomainTest, IClassFixture<Fixture>
    {
        public ExpressionExtensionsTests(Fixture fixture) : base(fixture) { }

        [Fact]
        public void InterfaceAssociation()
        {
            Expression<Func<IMetaUser, IRelationEndType>> expression = v => v.Logins;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.User.Logins, path.RelationEndType);
            Assert.Empty(path.Nodes);
        }

        [Fact]
        public void ClassAssociation()
        {
            Expression<Func<IMetaPerson, IRelationEndType>> expression = v => v.OrganizationWhereEmployee;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.Person.OrganizationWhereEmployee, path.RelationEndType);
            Assert.Empty(path.Nodes);
        }

        [Fact]
        public void ClassAssociationClassRole()
        {
            Expression<Func<IMetaPerson, IRelationEndType>> expression = v => v.OrganizationWhereEmployee.Organization.Information;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.Person.OrganizationWhereEmployee, path.RelationEndType);

            var next = path.Nodes.First();

            Assert.Equal(this.M.Organization.Information, next.RelationEndType);
            Assert.Empty(next.Nodes);
        }

        [Fact]
        public void ClassRole()
        {
            Expression<Func<IMetaOrganization, IRelationEndType>> expression = v => v.Name;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.Organization.Name, path.RelationEndType);
            Assert.Empty(path.Nodes);
        }

        [Fact]
        public void ClassRoleOfType()
        {
            Expression<Func<IMetaUserGroup, IComposite>> expression = v => v.Members.User.AsPerson;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.UserGroup.Members, path.RelationEndType);
            Assert.Equal(this.M.Person, path.OfType);
            Assert.Empty(path.Nodes);
        }

        [Fact]
        public void ClassRoleClassRole()
        {
            Expression<Func<IMetaOrganization, IRelationEndType>> expression = v => v.Employees.Person.FirstName;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.Organization.Employees, path.RelationEndType);

            var next = path.Nodes.First();

            Assert.Equal(this.M.Person.FirstName, next.RelationEndType);
            Assert.Empty(next.Nodes);
        }


        [Fact]
        public void ClassRoleInterfaceAsClassRole()
        {
            Expression<Func<IMetaUserGroup, IRelationEndType>> expression = v => v.Members.User.AsPerson.FirstName;

            var path = expression.Node(this.M);

            Assert.Equal(this.M.UserGroup.Members, path.RelationEndType);

            var next = path.Nodes.First();

            Assert.Equal(this.M.Person.FirstName, next.RelationEndType);
            Assert.Empty(next.Nodes);
        }
    }
}
