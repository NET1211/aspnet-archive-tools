﻿// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************

namespace GraphQL.AspNet.Tests.Internal.Templating.ObjectTypeTests
{
    using GraphQL.AspNet.Attributes;

    public class TwoMethodsWithSameName
    {
        [GraphField]
        public string Method1()
        {
            return string.Empty;
        }

        [GraphField]
        public string Method1(int param1)
        {
            return string.Empty;
        }
    }
}