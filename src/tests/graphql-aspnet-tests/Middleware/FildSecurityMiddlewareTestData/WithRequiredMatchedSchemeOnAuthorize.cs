﻿// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************

namespace GraphQL.AspNet.Tests.Middleware.FildSecurityMiddlewareTestData
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize(AuthenticationSchemes = "testScheme")]
    public class WithRequiredMatchedSchemeOnAuthorize
    {
    }
}