// <copyright file="TestPopulation.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Database.Server.Controllers
{
    using System;
    using Allors.Database.Domain;

    public class TestPopulation
    {
        private readonly ITransaction transaction;
        private readonly string population;

        public TestPopulation(ITransaction transaction, string population)
        {
            this.transaction = transaction;
            this.population = population;
        }

        public void Apply()
        {
            if ("full".Equals(this.population))
            {
                this.Full();
            }

            this.transaction.Derive();
        }

        private void Full()
        {
            #region Plurals
            var people = new People(this.transaction);
            var c1s = new C1s(this.transaction);
            var c2s = new C2s(this.transaction);
            #endregion

            #region Builders
            Person BuildPerson(string userName, string firstName, string lastName)
            {
                void Builder(Person v)
                {
                    v.UserName = userName;
                    v.FirstName = firstName;
                    v.LastName = lastName;
                }

                return people.Create(Builder);
            }

            C1 BuildC1(string name, int order) => c1s.Create(v =>
            {
                v.Name = name;
                v.Order = order;
            });

            C2 BuildC2(string name, int order) => c2s.Create(v =>
            {
                v.Name = name;
                v.Order = order;
            });

            #endregion

            BuildPerson("noacl", "no", "acl");

            var noperm = BuildPerson("noperm", "no", "perm");
            var emptyRole = this.transaction.Build<Role>(v => v.Name = "Empty");
            var defaultSecurityToken = new SecurityTokens(this.transaction).DefaultSecurityToken;

            var acl = this.transaction.Build<Grant>(v =>
            {
                v.Role = emptyRole;
                v.AddSubject(noperm);
                v.AddSecurityToken(defaultSecurityToken);
            });

            var c1A = BuildC1("c1A",4);
            var c1B = BuildC1("c1B",3);
            var c1C = BuildC1("c1C", 8);
            var c1D = BuildC1("c1D", 7);
            var c2A = BuildC2("c2A", 5);
            var c2B = BuildC2("c2B", 6);
            var c2C = BuildC2("c2C", 2);
            var c2D = BuildC2("c2D", 1);

            // class
            c1B.C1AllorsString = "???bra";
            c1C.C1AllorsString = "???bracadabra";
            c1D.C1AllorsString = "???bracadabra";

            c2B.C2AllorsString = "???bra";
            c2C.C2AllorsString = "???bracadabra";
            c2D.C2AllorsString = "???bracadabra";
            // exclusive interface
            c1B.I1AllorsString = "???bra";
            c1C.I1AllorsString = "???bracadabra";
            c1D.I1AllorsString = "???bracadabra";

            // shared interface
            c1B.I12AllorsString = "???bra";
            c1C.I12AllorsString = "???bracadabra";
            c1D.I12AllorsString = "???bracadabra";
            c2B.I12AllorsString = "???bra";
            c2C.I12AllorsString = "???bracadabra";
            c2D.I12AllorsString = "???bracadabra";

            // Boolean
            c1B.C1AllorsBoolean = true;
            c1C.C1AllorsBoolean = false;
            c1D.C1AllorsBoolean = false;

            c1B.I1AllorsBoolean = true;
            c1C.I1AllorsBoolean = false;
            c1D.I1AllorsBoolean = false;

            c1B.I12AllorsBoolean = true;
            c1C.I12AllorsBoolean = false;
            c1D.I12AllorsBoolean = false;
            c2B.I12AllorsBoolean = true;
            c2C.I12AllorsBoolean = false;
            c2D.I12AllorsBoolean = false;

            // DateTime
            c1B.C1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 4, DateTimeKind.Utc);
            c1C.C1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);
            c1D.C1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);

            c1B.I1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 4, DateTimeKind.Utc);
            c1C.I1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);
            c1D.I1AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);

            c1B.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 4, DateTimeKind.Utc);
            c1C.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);
            c1D.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);
            c2B.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 4, DateTimeKind.Utc);
            c2C.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);
            c2D.I12AllorsDateTime = new DateTime(2000, 1, 1, 0, 0, 5, DateTimeKind.Utc);

            // Decimal
            c1B.C1AllorsDecimal = 1.1M;
            c1C.C1AllorsDecimal = 2.2M;
            c1D.C1AllorsDecimal = 2.2M;

            c1B.I1AllorsDecimal = 1.1M;
            c1C.I1AllorsDecimal = 2.2M;
            c1D.I1AllorsDecimal = 2.2M;

            c1B.I12AllorsDecimal = 1.1M;
            c1C.I12AllorsDecimal = 2.2M;
            c1D.I12AllorsDecimal = 2.2M;
            c2B.I12AllorsDecimal = 1.1M;
            c2C.I12AllorsDecimal = 2.2M;
            c2D.I12AllorsDecimal = 2.2M;

            // Float
            c1B.C1AllorsDouble = 1.1d;
            c1C.C1AllorsDouble = 2.2d;
            c1D.C1AllorsDouble = 2.2d;

            c1B.I1AllorsDouble = 1.1d;
            c1C.I1AllorsDouble = 2.2d;
            c1D.I1AllorsDouble = 2.2d;

            c1B.I12AllorsDouble = 1.1d;
            c1C.I12AllorsDouble = 2.2d;
            c1D.I12AllorsDouble = 2.2d;
            c2B.I12AllorsDouble = 1.1d;
            c2C.I12AllorsDouble = 2.2d;
            c2D.I12AllorsDouble = 2.2d;

            // Integer
            c1B.C1AllorsInteger = 1;
            c1C.C1AllorsInteger = 2;
            c1D.C1AllorsInteger = 2;

            c1B.I1AllorsInteger = 1;
            c1C.I1AllorsInteger = 2;
            c1D.I1AllorsInteger = 2;

            c1B.I12AllorsInteger = 1;
            c1C.I12AllorsInteger = 2;
            c1D.I12AllorsInteger = 2;
            c2B.I12AllorsInteger = 1;
            c2C.I12AllorsInteger = 2;
            c2D.I12AllorsInteger = 2;

            // Unique
            c1B.C1AllorsUnique = new Guid("8B3C4978-72D3-40BA-B302-114EB331FE04");
            c1C.C1AllorsUnique = new Guid("0FD4EC2C-08DB-46B9-B71A-10152EBE4569");
            c1D.C1AllorsUnique = new Guid("AF01C994-379A-449A-8C4D-8D3B7207EC91");

            c1B.I1AllorsUnique = new Guid("7F7BF8EF-DDF2-47E6-B33F-627BE7DEAD6D");
            c1C.I1AllorsUnique = new Guid("08BFF7DE-51B9-4A53-BFBA-1212F23A342D");
            c1D.I1AllorsUnique = new Guid("59A349CB-E197-43B7-A17E-90630A87873C");

            c1B.I12AllorsUnique = new Guid("FF240B61-52A1-42E3-8FB5-A2CFB243E4E8");
            c1C.I12AllorsUnique = new Guid("8E3C8B4D-C945-4814-9FED-F68E632ECF0A");
            c1D.I12AllorsUnique = new Guid("F0C49BFF-4B3A-48E8-A354-6201E8E20A00");
            c2B.I12AllorsUnique = new Guid("0D2FEA7B-549C-466D-91E8-92427309F83A");
            c2C.I12AllorsUnique = new Guid("3E94AA78-2101-42E9-87AC-87563B792DED");
            c2D.I12AllorsUnique = new Guid("5E12BE66-D083-450E-9AF3-C37A354701F8");

            // One2One
            c1B.C1C1One2One = c1B;
            c1C.C1C1One2One = c1C;
            c1D.C1C1One2One = c1D;

            c1B.C1C2One2One = c2B;
            c1C.C1C2One2One = c2C;
            c1D.C1C2One2One = c2D;

            c1B.I1I2One2One = c2B;
            c1C.I1I2One2One = c2C;
            c1D.I1I2One2One = c2D;

            c1B.I12C2One2One = c2B;
            c1C.I12C2One2One = c2C;
            c1D.I12C2One2One = c2D;
            c2A.I12C2One2One = c2A;

            c1B.C1I12One2One = c1B;
            c1C.C1I12One2One = c2B;
            c1D.C1I12One2One = c2C;

            // One2Many
            c1B.AddC1C1One2Many(c1B);
            c1C.AddC1C1One2Many(c1C);
            c1C.AddC1C1One2Many(c1D);

            c1B.AddC1C2One2Many(c2B);
            c1C.AddC1C2One2Many(c2C);
            c1C.AddC1C2One2Many(c2D);

            c1B.AddI1I2One2Many(c2B);
            c1C.AddI1I2One2Many(c2C);
            c1C.AddI1I2One2Many(c2D);

            c1B.AddC1I12One2Many(c1B);
            c1C.AddC1I12One2Many(c2C);
            c1C.AddC1I12One2Many(c2D);

            // Many2One
            c1B.C1C1Many2One = c1B;
            c1C.C1C1Many2One = c1C;
            c1D.C1C1Many2One = c1C;

            c1B.C1C2Many2One = c2B;
            c1C.C1C2Many2One = c2C;
            c1D.C1C2Many2One = c2C;

            c1B.I1I2Many2One = c2B;
            c1C.I1I2Many2One = c2C;
            c1D.I1I2Many2One = c2C;

            c1B.I12C2Many2One = c2B;
            c2C.I12C2Many2One = c2C;
            c2D.I12C2Many2One = c2C;

            c1B.C1I12Many2One = c1B;
            c1C.C1I12Many2One = c2C;
            c1D.C1I12Many2One = c2C;

            // Many2Many
            c1B.AddC1C1Many2Many(c1B);
            c1C.AddC1C1Many2Many(c1B);
            c1D.AddC1C1Many2Many(c1B);
            c1C.AddC1C1Many2Many(c1C);
            c1D.AddC1C1Many2Many(c1C);
            c1D.AddC1C1Many2Many(c1D);

            c1B.AddC1C2Many2Many(c2B);
            c1C.AddC1C2Many2Many(c2B);
            c1D.AddC1C2Many2Many(c2B);
            c1C.AddC1C2Many2Many(c2C);
            c1D.AddC1C2Many2Many(c2C);
            c1D.AddC1C2Many2Many(c2D);

            c1B.AddI1I2Many2Many(c2B);
            c1C.AddI1I2Many2Many(c2B);
            c1C.AddI1I2Many2Many(c2C);
            c1D.AddI1I2Many2Many(c2B);
            c1D.AddI1I2Many2Many(c2C);
            c1D.AddI1I2Many2Many(c2D);

            c1B.AddI12C2Many2Many(c2B);
            c1C.AddI12C2Many2Many(c2B);
            c1C.AddI12C2Many2Many(c2C);
            c1D.AddI12C2Many2Many(c2B);
            c1D.AddI12C2Many2Many(c2C);
            c1D.AddI12C2Many2Many(c2D);
            c2A.AddI12C2Many2Many(c2A);
            c2A.AddI12C2Many2Many(c2B);
            c2A.AddI12C2Many2Many(c2C);
            c2A.AddI12C2Many2Many(c2D);

            c1B.AddC1I12Many2Many(c1B);
            c1B.AddC1I12Many2Many(c2B);
            c1C.AddC1I12Many2Many(c2B);
            c1C.AddC1I12Many2Many(c2C);
            c1D.AddC1I12Many2Many(c2B);
            c1D.AddC1I12Many2Many(c2C);
            c1D.AddC1I12Many2Many(c2D);
        }
    }
}
