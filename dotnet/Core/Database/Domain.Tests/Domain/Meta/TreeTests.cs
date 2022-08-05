// <copyright file="TreeTests.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>
//   Defines the PersonTests type.
// </summary>

namespace Allors.Database.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Database.Data;
    using Xunit;

    public class TreeTests : DomainTest, IClassFixture<Fixture>
    {
        public TreeTests(Fixture fixture) : base(fixture) { }

        public IPrefetchPolicyCache PrefetchPolicyCache => this.Transaction.Database.Services.Get<IPrefetchPolicyCache>();

        [Fact]
        public void ResolveObject()
        {
            var c2A = new C2Builder(this.Transaction).WithC2AllorsString("c2A");
            var c2B = new C2Builder(this.Transaction).WithC2AllorsString("c2B");
            var c2C = new C2Builder(this.Transaction).WithC2AllorsString("c2C");

            var c1A = new C1Builder(this.Transaction).WithC1AllorsString("c1A").WithC1C2One2Many(c2A);

            var c1B = new C1Builder(this.Transaction).WithC1AllorsString("c1B")
                    .WithC1C2One2Many(c2B)
                    .WithC1C2One2Many(c2C)
                    ;

            this.Transaction.Derive();

            var tree = new[] { new Node(this.M.C1.C1C2One2Manies) };
            {
                var resolved = new HashSet<IObject>();
                tree.Resolve(c1A, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Single(resolved);
                Assert.Contains(c2A, resolved);
            }

            {
                var resolved = new HashSet<IObject>();
                tree.Resolve(c1B, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(2, resolved.Count);
                Assert.Contains(c2B, resolved);
                Assert.Contains(c2C, resolved);
            }
        }

        [Fact]
        public void ResolveObjectMultipleSubtree()
        {
            var c1A = new C1Builder(this.Transaction).WithC1AllorsString("c1A");
            var c1B = new C1Builder(this.Transaction).WithC1AllorsString("c1B");
            var c1C = new C1Builder(this.Transaction).WithC1AllorsString("c1C");
            var c1D = new C1Builder(this.Transaction).WithC1AllorsString("c1D");
            var c1E = new C1Builder(this.Transaction).WithC1AllorsString("c1E");

            var c2A = new C2Builder(this.Transaction).WithC2AllorsString("c2A");
            var c2B = new C2Builder(this.Transaction).WithC2AllorsString("c2B");
            var c2C = new C2Builder(this.Transaction).WithC2AllorsString("c2C");
            var c2D = new C2Builder(this.Transaction).WithC2AllorsString("c2D");

            c1A.AddC1I12One2Many(c1C);
            c1B.AddC1I12One2Many(c1E);
            c1B.AddC1I12One2Many(c2A);
            c1B.AddC1I12One2Many(c2B);

            c1C.AddC1C1One2Many(c1D);
            c2A.AddC2C2One2Many(c2C);
            c2A.AddC2C2One2Many(c2D);

            this.Transaction.Derive();

            this.Transaction.Commit();

            var tree = new[]
            {
                new Node(this.M.C1.C1I12One2Manies)
                    .Add(this.M.C1.C1C1One2Manies),
                new Node(this.M.C1.C1I12One2Manies)
                    .Add(this.M.C2.C2C2One2Manies),
            };

            var prefetchPolicy = new PrefetchPolicyBuilder().WithNodes(tree, this.M);

            {
                var resolved = new HashSet<IObject>();

                this.Transaction.Prefetch(prefetchPolicy, c1A);

                tree.Resolve(c1A, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(2, resolved.Count);
                Assert.Contains(c1C, resolved);
                Assert.Contains(c1D, resolved);
            }

            {
                var resolved = new HashSet<IObject>();

                this.Transaction.Prefetch(prefetchPolicy, c1B);
                tree.Resolve(c1B, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(5, resolved.Count);
                Assert.Contains(c1E, resolved);
                Assert.Contains(c2A, resolved);
                Assert.Contains(c2B, resolved);
                Assert.Contains(c2C, resolved);
                Assert.Contains(c2D, resolved);
            }
        }

        [Fact]
        public void ResolveCollection()
        {
            var c2A = new C2Builder(this.Transaction).WithC2AllorsString("c2A");
            var c2B = new C2Builder(this.Transaction).WithC2AllorsString("c2B");
            var c2C = new C2Builder(this.Transaction).WithC2AllorsString("c2C");

            var c1A = new C1Builder(this.Transaction).WithC1AllorsString("c1A").WithC1C2One2Many(c2A);

            var c1B = new C1Builder(this.Transaction).WithC1AllorsString("c1B")
                    .WithC1C2One2Many(c2B)
                    .WithC1C2One2Many(c2C)
                    ;

            this.Transaction.Derive();

            var tree = new[] { new Node(this.M.C1.C1C2One2Manies) };
            {
                var resolved = new HashSet<IObject>();
                tree.Resolve(new[] { c1A }, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Single(resolved);
                Assert.Contains(c2A, resolved);
            }

            {
                var resolved = new HashSet<IObject>();
                tree.Resolve(new[] { c1B }, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(2, resolved.Count);
                Assert.Contains(c2B, resolved);
                Assert.Contains(c2C, resolved);
            }
        }

        [Fact]
        public void ResolveCollectionMultipleSubtree()
        {
            var c1A = new C1Builder(this.Transaction).WithC1AllorsString("c1A");
            var c1B = new C1Builder(this.Transaction).WithC1AllorsString("c1B");
            var c1C = new C1Builder(this.Transaction).WithC1AllorsString("c1C");
            var c1D = new C1Builder(this.Transaction).WithC1AllorsString("c1D");
            var c1E = new C1Builder(this.Transaction).WithC1AllorsString("c1E");

            var c2A = new C2Builder(this.Transaction).WithC2AllorsString("c2A");
            var c2B = new C2Builder(this.Transaction).WithC2AllorsString("c2B");
            var c2C = new C2Builder(this.Transaction).WithC2AllorsString("c2C");
            var c2D = new C2Builder(this.Transaction).WithC2AllorsString("c2D");

            c1A.AddC1I12One2Many(c1C);
            c1B.AddC1I12One2Many(c1E);
            c1B.AddC1I12One2Many(c2A);
            c1B.AddC1I12One2Many(c2B);

            c1C.AddC1C1One2Many(c1D);
            c2A.AddC2C2One2Many(c2C);
            c2A.AddC2C2One2Many(c2D);

            this.Transaction.Derive();

            this.Transaction.Commit();

            var tree = new[]
            {
                new Node(this.M.C1.C1I12One2Manies)
                    .Add(this.M.C1.C1C1One2Manies),
                new Node(this.M.C1.C1I12One2Manies)
                    .Add(this.M.C2.C2C2One2Manies),
            };

            var prefetchPolicy = new PrefetchPolicyBuilder().WithNodes(tree, this.M);

            {
                var resolved = new HashSet<IObject>();

                this.Transaction.Prefetch(prefetchPolicy, c1A);

                tree.Resolve(new[] { c1A }, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(2, resolved.Count);
                Assert.Contains(c1C, resolved);
                Assert.Contains(c1D, resolved);
            }

            {
                var resolved = new HashSet<IObject>();

                this.Transaction.Prefetch(prefetchPolicy, c1B);
                tree.Resolve(new[] { c1B }, this.AclsMock.Object, v => resolved.Add(v), this.PrefetchPolicyCache, this.Transaction);

                Assert.Equal(5, resolved.Count);
                Assert.Contains(c1E, resolved);
                Assert.Contains(c2A, resolved);
                Assert.Contains(c2B, resolved);
                Assert.Contains(c2C, resolved);
                Assert.Contains(c2D, resolved);
            }
        }

        [Fact]
        public void Legal()
        {
            new Node(this.M.C1.C1C1Many2Manies).Add(this.M.C1.C1C2Many2Manies);

            new Node(this.M.C1.C1C1Many2Manies).Add(this.M.I12.I12C2Many2Manies);

            new Node(this.M.C1.C1I12Many2Manies).Add(this.M.C1.I12C2Many2Manies);
        }

        [Fact]
        public void Illegal()
        {
            {
                var exceptionThrown = false;

                try
                {
                    new Node(this.M.C1.C1C1Many2Manies).Add(this.M.C2.C2C1Many2Manies);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.True(exceptionThrown);
            }
        }

        [Fact]
        public void UnitTreeNodesDontHaveTreeNodes()
        {
            var treeNode = new Node(this.M.C1.C1AllorsString);

            Assert.Empty(treeNode.Nodes);
        }

        [Fact]
        public void Prefetch()
        {
            var tree = Array.Empty<Node>();
            new PrefetchPolicyBuilder().WithNodes(tree, this.M);

            tree = new[] { new Node(this.M.C1.C1AllorsBinary) };
            new PrefetchPolicyBuilder().WithNodes(tree, this.M);

            tree = new[] { new Node(this.M.C1.C1C1Many2Manies) };
            new PrefetchPolicyBuilder().WithNodes(tree, this.M);
        }
    }
}
