// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************

namespace GraphQL.AspNet.Tests.Internal.Templating.GraphTypeNameTestData
{
    using GraphQL.AspNet.Attributes;

    [GraphType("GenericClassWithAttribute")]
    public class GenericClassWithAttribute<T, TK>
    {
        public T Prop1 { get; set; }

        public TK Prop2 { get; set; }
    }
}