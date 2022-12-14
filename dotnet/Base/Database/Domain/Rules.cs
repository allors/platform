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

                // Base
                new MediaRule(m),
                new TransitionalDeniedPermissionRule(m),
                new NotificationListRule(m),

                // Custom
                new DataRule(m),
                new OrganizationEmployementRule(m),
                new PersonAddressRule(m),
                new PersonFullNameRule(m),
                new PersonGreetingRule(m),
            };
    }
}
