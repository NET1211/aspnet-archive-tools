// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************

namespace GraphQL.AspNet.Tests.ValidationRuless.RuleCheckTestData
{
    using GraphQL.AspNet.Attributes;

    [GraphType("VerticalMover")]
    public interface IVerticalMover
    {
        int Id { get; }
    }
}