// <copyright file="Profile.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Tests.Workspace.Json
{
    using System;
    using System.Threading.Tasks;
    using Allors.Workspace;
    using Allors.Workspace.Adapters;
    using Allors.Workspace.Adapters.Json.Newtonsoft.WebClient;
    using Allors.Workspace.Meta;
    using RestSharp;
    using RestSharp.Serializers.NewtonsoftJson;
    using Xunit;
    using Configuration = Allors.Workspace.Adapters.Json.Configuration;
    using DatabaseConnection = Allors.Workspace.Adapters.Json.Newtonsoft.WebClient.DatabaseConnection;

    public class Profile : IProfile
    {
        public const string Url = "http://localhost:5000/allors/";

        public const string SetupUrl = "Test/Setup?population=full";
        public const string LoginUrl = "TestAuthentication/Token";

        private readonly Configuration configuration;

        private Client client;

        IWorkspaceConnection IProfile.Workspace => this.Workspace;

        public DatabaseConnection DatabaseConnection { get; private set; }

        public IWorkspaceConnection Workspace { get; private set; }

        public M M => this.Workspace.Services.Get<M>();

        public Profile()
        {
            var metaPopulation = new MetaBuilder().Build();
            var objectFactory = new ReflectionObjectFactory(metaPopulation, typeof(Allors.Workspace.Domain.Person));
            this.configuration = new Configuration("Default", metaPopulation, objectFactory);
        }

        public async Task InitializeAsync()
        {
            var request = new RestRequest($"{Url}{SetupUrl}", RestSharp.Method.GET, DataFormat.Json);
            var restClient = this.CreateRestClient();
            var response = await restClient.ExecuteAsync(request);
            Assert.True(response.IsSuccessful);

            this.client = new Client(this.CreateRestClient);
            this.DatabaseConnection = new DatabaseConnection(this.configuration, () => new WorkspaceServices(), this.client);
            this.Workspace = this.DatabaseConnection.CreateWorkspaceConnection();

            await this.Login("administrator");
        }

        public Task DisposeAsync() => Task.CompletedTask;

        public IWorkspaceConnection CreateExclusiveWorkspace()
        {
            var database = new DatabaseConnection(this.configuration, () => new WorkspaceServices(), this.client);
            return database.CreateWorkspaceConnection();
        }

        public IWorkspaceConnection CreateWorkspace() => this.DatabaseConnection.CreateWorkspaceConnection();

        public async Task Login(string user)
        {
            var uri = new Uri(LoginUrl, UriKind.Relative);
            var response = await this.client.Login(uri, user, null);
            Assert.True(response);
        }

        private IRestClient CreateRestClient() => new RestClient(Url).UseNewtonsoftJson();
    }
}