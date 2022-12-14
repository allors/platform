// <copyright file="RemoteDatabase.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters.Json.SystemText
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Allors.Protocol.Json;
    using Allors.Protocol.Json.Api.Invoke;
    using Allors.Protocol.Json.Api.Pull;
    using Allors.Protocol.Json.Api.Push;
    using Allors.Protocol.Json.Api.Security;
    using Allors.Protocol.Json.Api.Sync;
    using Allors.Protocol.Json.SystemText;
    using Allors.Workspace.Meta;
    using Polly;

    [SuppressMessage("Design", "RCS1090:Add call to 'ConfigureAwait' (or vice versa).", Justification = "<Pending>")]
    public class Connection : Json.Connection
    {
        public Connection(Client client, string name, IMetaPopulation metaPopulation) : base(name, metaPopulation)
        {
            this.Client = client;
            this.UnitConvert = new UnitConvert();
        }

        public override IUnitConvert UnitConvert { get; }

        protected override string UserId => this.Client.UserId;

        public IAsyncPolicy Policy { get; set; } = Polly.Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        public Client Client { get; }

        public override async Task<PullResponse> Pull(object args, string name)
        {
            var uri = new Uri($"{name}/pull", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, args);
            response.EnsureSuccessStatusCode();
            return await this.Client.ReadAsAsync<PullResponse>(response);
        }

        public override async Task<PullResponse> Pull(PullRequest pullRequest)
        {
            var uri = new Uri("pull", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, pullRequest);
            response.EnsureSuccessStatusCode();
            return await this.Client.ReadAsAsync<PullResponse>(response);
        }

        public override async Task<SyncResponse> Sync(SyncRequest syncRequest)
        {
            var uri = new Uri("sync", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, syncRequest);
            response.EnsureSuccessStatusCode();

            return await this.Client.ReadAsAsync<SyncResponse>(response);
        }

        public override async Task<PushResponse> Push(PushRequest pushRequest)
        {
            var uri = new Uri("push", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, pushRequest);
            response.EnsureSuccessStatusCode();

            return await this.Client.ReadAsAsync<PushResponse>(response);
        }

        public override async Task<InvokeResponse> Invoke(InvokeRequest invokeRequest)
        {
            var uri = new Uri("invoke", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, invokeRequest);
            response.EnsureSuccessStatusCode();

            return await this.Client.ReadAsAsync<InvokeResponse>(response);
        }

        public override async Task<AccessResponse> Access(AccessRequest accessRequest)
        {
            var uri = new Uri("access", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, accessRequest);
            response.EnsureSuccessStatusCode();

            return await this.Client.ReadAsAsync<AccessResponse>(response);
        }

        public override async Task<PermissionResponse> Permission(PermissionRequest permissionRequest)
        {
            var uri = new Uri("permission", UriKind.Relative);
            var response = await this.Client.PostAsJsonAsync(uri, permissionRequest);
            response.EnsureSuccessStatusCode();

            return await this.Client.ReadAsAsync<PermissionResponse>(response);
        }
    }
}
