// <copyright file="IBarcodeGenerator.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Configuration
{
    using Meta;
    using Services;

    public class ObjectBuilderService : IObjectBuilderService
    {
        public ITransaction Transaction { get; }

        public ObjectBuilderService(ITransaction transaction) => this.Transaction = transaction;

        public IObject Build(IClass @class) => this.Transaction.Build(@class);
    }
}
