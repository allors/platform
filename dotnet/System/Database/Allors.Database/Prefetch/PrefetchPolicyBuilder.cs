// <copyright file="PrefetchPolicyBuilder.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the ObjectIdInteger type.</summary>

namespace Allors.Database;

using System.Collections.Generic;
using Allors.Database.Meta;

public sealed class PrefetchPolicyBuilder
{
    private bool allowCompilation = true;
    private List<PrefetchRule> rules;

    public PrefetchPolicyBuilder() => this.rules = new List<PrefetchRule>();

    public PrefetchPolicyBuilder WithRule(IRelationEndType relationEndType)
    {
        var rule = new PrefetchRule(relationEndType, null);
        this.rules.Add(rule);
        return this;
    }

    public PrefetchPolicyBuilder WithRule(IRelationEndType relationEndType, PrefetchPolicy prefetch)
    {
        var rule = new PrefetchRule(relationEndType, prefetch);
        this.rules.Add(rule);
        return this;
    }

    public PrefetchPolicyBuilder WithAllowCompilation(bool allowCompilation)
    {
        this.allowCompilation = allowCompilation;
        return this;
    }

    public PrefetchPolicy Build()
    {
        try
        {
            return new PrefetchPolicy(this.rules.ToArray()) { AllowCompilation = this.allowCompilation };
        }
        finally
        {
            this.rules = null;
        }
    }
}
