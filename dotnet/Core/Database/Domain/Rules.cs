// <copyright file="ObjectsBase.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    using Allors.Database.Domain.Derivations.Rules;
    using Allors.Database.Meta;

    public static class Rules
    {
        public static Rule[] Create(M m) =>
            new Rule[]
            {
                // Core
                new UserNormalizedUserNameRule(m),
                new UserNormalizedUserEmailRule(m),
                new UserInUserPasswordRule(m),
                new GrantEffectiveUsersRule(m),
                new GrantEffectivePermissionsRule(m),
                new SecurityTokenFingerprintRule(m),
                
                // Custom
                new PersonFullNameRule(m),
                new PersonCustomFullNameRule(m),
                new PersonGreetingRule(m),
                new PersonOwningRule(m),
                new OrganizationJustDidItRule(m),
                new OrganizationPostDeriveRule(m),

                // Validation
                new RoleOne2OneRule(m),
                new RoleOne2ManyRule(m),
                new RoleMany2OneRule(m),
                new RoleMany2ManyRule(m),

                // RoleTypeHierarchy
                new C1ChangedRoleRule(m),
                new I12ChangedRoleRule(m),
                new I1ChangedRoleRule(m),
                new S12ChangedRoleRule(m),
            };
    }
}
