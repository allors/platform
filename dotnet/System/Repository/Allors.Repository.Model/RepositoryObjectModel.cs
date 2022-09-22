﻿namespace Generate.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using Allors.Repository;

public abstract class RepositoryObjectModel
{
    protected RepositoryObjectModel(RepositoryModel repositoryModel) => this.RepositoryModel = repositoryModel;

    public RepositoryModel RepositoryModel { get; }

    protected abstract RepositoryObject RepositoryObject { get; }

    public Dictionary<string, Attribute> AttributeByName => this.RepositoryObject.AttributeByName;

    public Dictionary<string, Attribute[]> AttributesByName => this.RepositoryObject.AttributesByName;

    public string Id => (string)((dynamic)this.AttributeByName.Get("Id"))?.Value;

    public override string ToString() => this.RepositoryObject.ToString();

    public Attribute[] ExtensionAttributes => this.AttributeByName.Values.Where(v => v.GetType().GetInterfaces().Any(v => "IExtensionAttribute" == v.Name)).ToArray();
}
