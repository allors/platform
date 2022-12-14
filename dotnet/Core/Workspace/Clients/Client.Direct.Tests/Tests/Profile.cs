// <copyright file="Profile.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Tests.Workspace.Direct
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Allors.Database;
    using Allors.Database.Adapters.Memory;
    using Allors.Database.Configuration;
    using Allors.Database.Domain;
    using Allors.Workspace;
    using Allors.Workspace.Adapters.Direct;
    using Allors.Workspace.Meta;
    using Allors.Workspace.Meta.Static;

    public class Profile : IProfile
    {
        private User user;
        private readonly M metaPopulation;

        public Database Database { get; }

        IConnection IProfile.Connection => this.Connection;

        public Connection Connection { get; private set; }

        public M M => (M)this.Connection.MetaPopulation;

        public Profile(Fixture fixture)
        {

            this.metaPopulation = new MetaBuilder().Build();

            this.Database = new Database(
                new DefaultDatabaseServices(fixture.Engine),
                new Configuration
                {
                    ObjectFactory = new ObjectFactory(fixture.M, typeof(Person)),
                });

            this.Database.Init();

            this.Connection = new Connection(this.Database, "Default", this.metaPopulation);

            var config = new Config();
            new Setup(this.Database, config).Apply();

            using var transaction = this.Database.CreateTransaction();

            var administrator = transaction.Build<Person>(v => v.UserName = "administrator");
            new UserGroups(transaction).Administrators.AddMember(administrator);
            transaction.Services.Get<IUserService>().User = administrator;

            new TestPopulation(transaction).Apply();
            transaction.Derive();
            transaction.Commit();
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync() => Task.CompletedTask;

        public IConnection CreateExclusiveWorkspaceConnection() => new Connection(this.Database, "Default", this.metaPopulation) { UserId = this.user.Id };

        public IConnection CreateWorkspaceConnection() => new Connection(this.Database, "Default", this.metaPopulation) { UserId = this.user.Id };

        public Task Login(string userName)
        {
            using var transaction = this.Database.CreateTransaction();
            this.user = new Users(transaction).Extent().ToArray().First(v => v.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            transaction.Services.Get<IUserService>().User = this.user;

            this.Connection.UserId = this.user.Id;

            return Task.CompletedTask;
        }
    }
}
