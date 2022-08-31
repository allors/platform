// <copyright file="DelegatedAccessObjectDelegateAccess.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Domain
{
    public abstract class DelegatedAccessObjectDelegateAccess
    {
        public SecurityToken[] SecurityTokens { get; set; }

        public Revocation[] Revocations { get; set; }
    }
}
