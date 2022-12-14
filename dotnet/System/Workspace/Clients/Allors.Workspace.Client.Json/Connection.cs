// <copyright file="RemoteWorkspace.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Allors.Protocol.Json;
    using Allors.Protocol.Json.Api.Invoke;
    using Allors.Protocol.Json.Api.Pull;
    using Allors.Protocol.Json.Api.Push;
    using Allors.Protocol.Json.Api.Security;
    using Allors.Protocol.Json.Api.Sync;
    using Allors.Workspace.Meta;
    using Allors.Workspace.Protocol.Json;
    using Allors.Workspace.Request;
    using Allors.Workspace.Response;
    using Allors.Shared.Ranges;
    using PullRequest = Allors.Workspace.Request.PullRequest;

    public abstract class Connection : Adapters.Connection
    {
        private readonly Dictionary<IClass, Dictionary<IOperandType, long>> executePermissionByOperandTypeByClass;

        private readonly Dictionary<IClass, Dictionary<IOperandType, long>> readPermissionByOperandTypeByClass;
        private readonly Dictionary<long, Record> recordsById;
        private readonly Dictionary<IClass, Dictionary<IOperandType, long>> writePermissionByOperandTypeByClass;


        protected Connection(string name, IMetaPopulation metaPopulation)
            : base(name, metaPopulation)
        {
            this.recordsById = new Dictionary<long, Record>();

            this.GrantById = new Dictionary<long, Grant>();
            this.RevocationById = new Dictionary<long, Revocation>();
            this.Permissions = new HashSet<long>();

            this.readPermissionByOperandTypeByClass = new Dictionary<IClass, Dictionary<IOperandType, long>>();
            this.writePermissionByOperandTypeByClass = new Dictionary<IClass, Dictionary<IOperandType, long>>();
            this.executePermissionByOperandTypeByClass = new Dictionary<IClass, Dictionary<IOperandType, long>>();
        }

        internal Dictionary<long, Grant> GrantById { get; }

        internal Dictionary<long, Revocation> RevocationById { get; }

        internal ISet<long> Permissions { get; }

        public abstract IUnitConvert UnitConvert { get; }

        protected abstract string UserId { get; }

        internal SyncRequest OnPullResponse(PullResponse response) =>
            new SyncRequest
            {
                o = response.p
                    .Where(v =>
                    {
                        if (!this.recordsById.TryGetValue(v.i, out var record))
                        {
                            return true;
                        }

                        if (!record.Version.Equals(v.v))
                        {
                            return true;
                        }

                        if (!record.GrantIds.Equals(ValueRange<long>.Load(v.g)))
                        {
                            return true;
                        }

                        if (!record.RevocationIds.Equals(ValueRange<long>.Load(v.r)))
                        {
                            return true;
                        }

                        return false;
                    })
                    .Select(v => v.i).ToArray(),
            };

        internal AccessRequest OnSyncResponse(SyncResponse syncResponse)
        {
            var ctx = new ResponseContext(this);

            foreach (var syncResponseObject in syncResponse.o)
            {
                var databaseObjects = Record.FromResponse(this, ctx, syncResponseObject);
                this.recordsById[databaseObjects.Id] = databaseObjects;
            }

            if (ctx.MissingGrantIds.Count > 0 || ctx.MissingRevocationIds.Count > 0)
            {
                return new AccessRequest
                {
                    g = ctx.MissingGrantIds.Select(v => v).ToArray(),
                    r = ctx.MissingRevocationIds.Select(v => v).ToArray(),
                };
            }

            return null;
        }

        internal PermissionRequest AccessResponse(AccessResponse accessResponse)
        {
            var responseContext = new ResponseContext(this);

            HashSet<long> missingPermissionIds = null;
            if (accessResponse.g != null)
            {
                foreach (var syncResponseAccessControl in accessResponse.g)
                {
                    var id = syncResponseAccessControl.i;
                    var version = syncResponseAccessControl.v;
                    var permissionIds = ValueRange<long>.Load(syncResponseAccessControl.p);
                    this.GrantById[id] = new Grant { Version = version, PermissionIds = ValueRange<long>.Load(permissionIds) };

                    foreach (var permissionId in permissionIds)
                    {
                        if (this.Permissions.Contains(permissionId))
                        {
                            continue;
                        }

                        missingPermissionIds ??= new HashSet<long>();
                        missingPermissionIds.Add(permissionId);
                    }
                }
            }

            if (accessResponse.r != null)
            {
                foreach (var syncResponseRevocation in accessResponse.r)
                {
                    var id = syncResponseRevocation.i;
                    var version = syncResponseRevocation.v;
                    var permissionIds = ValueRange<long>.Load(syncResponseRevocation.p);
                    this.RevocationById[id] = new Revocation { Version = version, PermissionIds = ValueRange<long>.Load(permissionIds) };

                    foreach (var permissionId in permissionIds)
                    {
                        if (this.Permissions.Contains(permissionId))
                        {
                            continue;
                        }

                        missingPermissionIds ??= new HashSet<long>();
                        missingPermissionIds.Add(permissionId);
                    }
                }
            }

            return missingPermissionIds != null ? new PermissionRequest { p = missingPermissionIds.ToArray() } : null;
        }

        internal void PermissionResponse(PermissionResponse permissionResponse)
        {
            if (permissionResponse.p != null)
            {
                foreach (var syncResponsePermission in permissionResponse.p)
                {
                    var id = syncResponsePermission.i;
                    var @class = (IClass)this.MetaPopulation.FindByTag(syncResponsePermission.c);
                    var metaObject = this.MetaPopulation.FindByTag(syncResponsePermission.t);
                    var operandType = (IOperandType)(metaObject as IRelationType)?.RoleType ?? (IMethodType)metaObject;
                    var operation = (Operations)syncResponsePermission.o;

                    this.Permissions.Add(id);

                    switch (operation)
                    {
                        case Operations.Read:
                            if (!this.readPermissionByOperandTypeByClass.TryGetValue(@class, out var readPermissionByOperandType))
                            {
                                readPermissionByOperandType = new Dictionary<IOperandType, long>();
                                this.readPermissionByOperandTypeByClass[@class] = readPermissionByOperandType;
                            }

                            readPermissionByOperandType[operandType] = id;

                            break;

                        case Operations.Write:
                            if (!this.writePermissionByOperandTypeByClass.TryGetValue(@class, out var writePermissionByOperandType))
                            {
                                writePermissionByOperandType = new Dictionary<IOperandType, long>();
                                this.writePermissionByOperandTypeByClass[@class] = writePermissionByOperandType;
                            }

                            writePermissionByOperandType[operandType] = id;

                            break;

                        case Operations.Execute:
                            if (!this.executePermissionByOperandTypeByClass.TryGetValue(@class, out var executePermissionByOperandType))
                            {
                                executePermissionByOperandType = new Dictionary<IOperandType, long>();
                                this.executePermissionByOperandTypeByClass[@class] = executePermissionByOperandType;
                            }

                            executePermissionByOperandType[operandType] = id;

                            break;
                        case Operations.Create:
                            throw new NotSupportedException("Create not supported");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public override async Task<IInvokeResult> InvokeAsync(MethodRequest method, BatchOptions options = null) =>
            await this.InvokeAsync(new[] { method }, options);

        public override async Task<IInvokeResult> InvokeAsync(MethodRequest[] methods, BatchOptions options = null)
        {
            var invokeRequest = new InvokeRequest
            {
                l = methods.Select(v => new Invocation { i = v.Object.Id, v = ((Object)v.Object).Version, m = v.MethodType.Tag }).ToArray(),
                o = options != null
                    ? new InvokeOptions { c = options.ContinueOnError, i = options.Isolated }
                    : null,
            };

            var invokeResponse = await this.Invoke(invokeRequest);

            var workspace = new Workspace(this);
            return new InvokeResult(workspace, invokeResponse);
        }

        public override async Task<IPullResult> PullAsync(params PullRequest[] pulls)
        {
            foreach (var pull in pulls)
            {
                if (pull.ObjectId < 0 || pull.Object?.Id < 0)
                {
                    throw new ArgumentException("Id is not in the database");
                }
            }

            var pullRequest = new Allors.Protocol.Json.Api.Pull.PullRequest { l = pulls.Select(v => v.ToJson(this.UnitConvert)).ToArray() };
            var pullResponse = await this.Pull(pullRequest);

            var workspace = new Workspace(this);
            return await workspace.OnPull(pullResponse);
        }

        public override long GetPermission(IClass @class, IOperandType operandType, Operations operation)
        {
            switch (operation)
            {
                case Operations.Read:
                    if (this.readPermissionByOperandTypeByClass.TryGetValue(@class,
                            out var readPermissionByOperandType) &&
                        readPermissionByOperandType.TryGetValue(operandType, out var readPermission))
                    {
                        return readPermission;
                    }

                    return 0;

                case Operations.Write:
                    if (this.writePermissionByOperandTypeByClass.TryGetValue(@class,
                            out var writePermissionByOperandType) &&
                        writePermissionByOperandType.TryGetValue(operandType, out var writePermission))
                    {
                        return writePermission;
                    }

                    return 0;

                case Operations.Execute:
                    if (this.executePermissionByOperandTypeByClass.TryGetValue(@class,
                            out var executePermissionByOperandType) &&
                        executePermissionByOperandType.TryGetValue(operandType, out var executePermission))
                    {
                        return executePermission;
                    }

                    return 0;

                case Operations.Create:
                    throw new NotSupportedException("Create is not supported");

                default:
                    throw new ArgumentOutOfRangeException(nameof(operation));
            }
        }

        public override Adapters.Record GetRecord(long id)
        {
            this.recordsById.TryGetValue(id, out var databaseObjects);
            return databaseObjects;
        }

        public abstract Task<PullResponse> Pull(object args, string name);

        public abstract Task<PullResponse> Pull(Allors.Protocol.Json.Api.Pull.PullRequest pullRequest);

        public abstract Task<SyncResponse> Sync(SyncRequest syncRequest);

        public abstract Task<PushResponse> Push(PushRequest pushRequest);

        public abstract Task<InvokeResponse> Invoke(InvokeRequest invokeRequest);

        public abstract Task<AccessResponse> Access(AccessRequest accessRequest);

        public abstract Task<PermissionResponse> Permission(PermissionRequest permissionRequest);
    }
}
