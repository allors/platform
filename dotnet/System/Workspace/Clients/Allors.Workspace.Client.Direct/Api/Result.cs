// <copyright file="LocalPullResult.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters.Direct;

using System.Collections.Generic;
using System.Linq;
using Allors.Workspace.Response;
using IDerivationError = Allors.Database.Derivations.IDerivationError;

public abstract class Result : IInvokeResult
{
    private readonly List<Object> accessErrorStrategies;
    private readonly List<long> databaseMissingIds;
    private readonly List<long> versionErrors;
    private List<IDerivationError> derivationErrors;

    protected Result(Workspace workspace)
    {
        this.Workspace = workspace;
        this.accessErrorStrategies = new List<Object>();
        this.databaseMissingIds = new List<long>();
        this.versionErrors = new List<long>();
    }

    protected Workspace Workspace { get; }

    public string ErrorMessage { get; set; }

    public IRecord Output { get; }

    public IEnumerable<IObject> VersionErrors => this.versionErrors?.Select(v => this.Workspace.GetObject(v));

    public IEnumerable<IObject> AccessErrors => this.accessErrorStrategies;

    public IEnumerable<IObject> MissingErrors => this.databaseMissingIds?.Select(this.Workspace.GetObject);

    public IEnumerable<Response.IDerivationError> DerivationErrors => this.derivationErrors
        ?.Select<IDerivationError, Response.IDerivationError>(v =>
            new DerivationError(this.Workspace, v)).ToArray();

    public bool HasErrors => !string.IsNullOrWhiteSpace(this.ErrorMessage) ||
                             this.accessErrorStrategies?.Count > 0 ||
                             this.databaseMissingIds?.Count > 0 ||
                             this.versionErrors?.Count > 0 ||
                             this.derivationErrors?.Count > 0;

    internal void AddDerivationErrors(IDerivationError[] errors) =>
        (this.derivationErrors ??= new List<IDerivationError>()).AddRange(errors);

    internal void AddMissingId(long id) => this.databaseMissingIds.Add(id);

    internal void AddAccessError(Object @object) => this.accessErrorStrategies.Add(@object);

    internal void AddVersionError(long id) => this.versionErrors.Add(id);
}
