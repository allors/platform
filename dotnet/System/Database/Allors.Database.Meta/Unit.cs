// <copyright file="Unit.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Defines the IObjectType type.</summary>

namespace Allors.Database.Meta
{
    using System;
    using System.Collections.Generic;

    public abstract class Unit : ObjectType, IUnit
    {
        private Type clrType;

        protected Unit(MetaPopulation metaPopulation, Guid id, string tag) : base(metaPopulation, id, tag) => metaPopulation.OnUnitCreated(this);

        /// <summary>
        /// Gets a value indicating whether this state is a binary.
        /// </summary>
        /// <value><c>true</c> if this state is a binary; otherwise, <c>false</c>.</value>
        public bool IsBinary => this.Tag == UnitTags.Binary;

        /// <summary>
        /// Gets a value indicating whether this state is a boolean.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state is a boolean; otherwise, <c>false</c>.
        /// </value>
        public bool IsBoolean => this.Tag == UnitTags.Boolean;

        /// <summary>
        /// Gets a value indicating whether this state is a date time.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state is a date time; otherwise, <c>false</c>.
        /// </value>
        public bool IsDateTime => this.Tag == UnitTags.DateTime;

        /// <summary>
        /// Gets a value indicating whether this state is a decimal.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this state is a decimal; otherwise, <c>false</c>.
        /// </value>
        public bool IsDecimal => this.Tag == UnitTags.Decimal;

        /// <summary>
        /// Gets a value indicating whether this state is a float.
        /// </summary>
        /// <value><c>true</c> if this state is a float; otherwise, <c>false</c>.</value>
        public bool IsFloat => this.Tag == UnitTags.Float;

        /// <summary>
        /// Gets a value indicating whether this state is an integer.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this state is an integer; otherwise, <c>false</c>.
        /// </value>
        public bool IsInteger => this.Tag == UnitTags.Integer;

        /// <summary>
        /// Gets a value indicating whether this state is a string.
        /// </summary>
        /// <value><c>true</c> if this state is a string; otherwise, <c>false</c>.</value>
        public bool IsString => this.Tag == UnitTags.String;

        /// <summary>
        /// Gets a value indicating whether this state is a unique.
        /// </summary>
        /// <value><c>true</c> if this state is a unique; otherwise, <c>false</c>.</value>
        public bool IsUnique => this.Id.Equals(UnitIds.Unique);

        public override Type ClrType => this.clrType;

        public void Bind() =>
            this.clrType = this.Tag switch
            {
                UnitTags.Binary => typeof(byte[]),
                UnitTags.Boolean => typeof(bool),
                UnitTags.DateTime => typeof(DateTime),
                UnitTags.Decimal => typeof(decimal),
                UnitTags.Float => typeof(double),
                UnitTags.Integer => typeof(int),
                UnitTags.String => typeof(string),
                UnitTags.Unique => typeof(Guid),
                _ => this.clrType
            };

        public override IEnumerable<string> WorkspaceNames => this.MetaPopulation.WorkspaceNames;
    }
}