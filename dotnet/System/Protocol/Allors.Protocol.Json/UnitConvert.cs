// <copyright file="Convert.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Protocol.Json
{
    public interface IUnitConvert
    {
        object ToJson(object value);

        object UnitFromJson(string tag, object value);

        long? LongFromJson(object v);

        long[] LongArrayFromJson(object v);

        string StringFromJson(object v);
    }
}
