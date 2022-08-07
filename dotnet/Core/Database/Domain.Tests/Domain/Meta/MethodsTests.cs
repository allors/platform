// <copyright file="MethodsTests.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>
//   Defines the PersonTests type.
// </summary>

namespace Allors.Database.Domain.Tests
{
    using Xunit;

    public class MethodsTests : DomainTest, IClassFixture<Fixture>
    {
        public MethodsTests(Fixture fixture) : base(fixture) { }

        [Fact]
        public void ClassMethod()
        {
            var c1 = this.Transaction.Build<C1>();

            var classMethod = c1.ClassMethod();

            Assert.Equal("C1CustomC1Core", classMethod.Value);
        }

        [Fact]
        public void InterfaceMethod()
        {
            var c1 = this.Transaction.Build<C1>();

            var interfaceMethod = c1.InterfaceMethod();

            Assert.Equal("I1CustomI1CoreC1CustomC1Core", interfaceMethod.Value);
        }

        [Fact]
        public void SuperinterfaceMethod()
        {
            var c1 = this.Transaction.Build<C1>();

            var interfaceMethod = c1.SuperinterfaceMethod();

            Assert.Equal("S1CustomS1CoreI1CustomI1CoreC1CustomC1Core", interfaceMethod.Value);
        }

        [Fact]
        public void MethodWithResults()
        {
            var c1 = this.Transaction.Build<C1>();

            var method = c1.Sum(
                m =>
                {
                    m.A = 1;
                    m.B = 2;
                });

            Assert.Equal(3, method.Result);
        }

        [Fact]
        public void CallMethodTwice()
        {
            var c1 = this.Transaction.Build<C1>();

            var classMethod = c1.ClassMethod();

            var exceptionThrown = false;
            try
            {
                classMethod.Execute();
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);
        }
    }
}
