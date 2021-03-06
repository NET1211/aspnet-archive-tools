// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************
namespace GraphQL.AspNet.Tests.Configuration.ConfigurationTestData
{
    using GraphQL.AspNet.Attributes;

    public class FanItem
    {
        [GraphField]
        public int Id { get; set; }

        [GraphField]
        public string Name { get; set; }

        [GraphField]
        public FanSpeed FanSpeed { get; set; }
    }
}