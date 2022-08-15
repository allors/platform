// <copyright file="OrderLineVersion.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Repository
{
    using System;

    using Attributes;

    #region
    [Id("BA589BE8-049B-4107-9E20-FBFEC19477C4")]
    #endregion
    public partial class OrderLineVersion : Version
    {
        #region Allors
        [Id("0B9340C2-CE9B-48C7-A476-6D73B8829944")]
        #endregion
        public decimal Amount { get; set; }

        #region inherited

        public Revocation[] Revocations { get; set; }
        public Guid SecurityFingerPrint { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        public Guid DerivationId { get; set; }

        public DateTime DerivationTimeStamp { get; set; }

        public void OnPostBuild()
        {
        }

        public void OnInit()
        {
        }

        public void OnPostDerive()
        {
        }

        #endregion
    }
}
