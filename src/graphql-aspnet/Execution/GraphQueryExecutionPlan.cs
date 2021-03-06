// *************************************************************
// project:  graphql-aspnet
// --
// repo: https://github.com/graphql-aspnet
// docs: https://graphql-aspnet.github.io
// --
// License:  MIT
// *************************************************************

namespace GraphQL.AspNet.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using GraphQL.AspNet.Common;
    using GraphQL.AspNet.Interfaces.Execution;
    using GraphQL.AspNet.Interfaces.TypeSystem;
    using GraphQL.AspNet.Internal.Interfaces;

    /// <summary>
    /// A query plan detailing the field data requested by an end user and the order in which
    /// the fields should be resolved. Also acts as a collection bin for any messages generated by
    /// server code during the execution.
    /// </summary>
    /// <typeparam name="TSchema">The type of the graphql schema to this plan exists for.</typeparam>
    [Serializable]
    [DebuggerDisplay("Operations = {Operations.Count}")]
    public class GraphQueryExecutionPlan<TSchema> : IGraphQueryPlan
         where TSchema : class, ISchema
    {
        private readonly Dictionary<string, IGraphFieldExecutableOperation> _operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphQueryExecutionPlan{TSchema}" /> class.
        /// </summary>
        public GraphQueryExecutionPlan()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.Messages = new GraphMessageCollection();
            _operations = new Dictionary<string, IGraphFieldExecutableOperation>();
        }

        /// <summary>
        /// Adds a parsed executable operation to the plan's operation collection.
        /// </summary>
        /// <param name="operation">The completed and validated operation to add.</param>
        public void AddOperation(IGraphFieldExecutableOperation operation)
        {
            Validation.ThrowIfNull(operation, nameof(operation));
            var name = operation.OperationName?.Trim() ?? string.Empty;

            this.Messages.AddRange(operation.Messages);
            _operations.Add(name, operation);
        }

        /// <summary>
        /// Retrieves the operation from those contained in this plan. If this plan contains only one operation
        /// that singular operation will be returned and the operationName is ignored. Otherwise, if the operation
        /// is not found it will be returned.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns>IFieldExecutionContextCollection.</returns>
        public IGraphFieldExecutableOperation RetrieveOperation(string operationName = null)
        {
            if (this.Operations.Count == 1)
                return this.Operations.Values.First();

            if (string.IsNullOrWhiteSpace(operationName))
                return null;

            operationName = operationName?.Trim() ?? string.Empty;
            if (this.Operations.ContainsKey(operationName))
                return this.Operations[operationName];

            return null;
        }

        /// <summary>
        /// Gets or sets the unique identifier assigned to this instance when it was created.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; protected set; }

        /// <summary>
        /// Gets the messages generated, if any, during the execution of the plan.
        /// </summary>
        /// <value>The messages.</value>
        public IGraphMessageCollection Messages { get; }

        /// <summary>
        /// Gets the collection of field contexts that need to be executed to fulfill an operation of a given name.
        /// </summary>
        /// <value>The operations.</value>
        public IReadOnlyDictionary<string, IGraphFieldExecutableOperation> Operations => _operations;

        /// <summary>
        /// Gets a value indicating whether this plan is in a valid and potentially executable state.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid => !this.Messages.Severity.IsCritical();

        /// <summary>
        /// Gets or sets a value representing a total estimate dcomplexity of the fields requested, their expected return values and nested dependencies there in. This value
        /// is used as a measure for determining executability of a query and to disallow unreasonable queries from overwhelming the server.
        /// </summary>
        /// <value>The total estimated complexity of this query plan.</value>
        public float EstimatedComplexity { get; set; }

        /// <summary>
        /// Gets or sets the maximum depth of nested nodes the query text acheives in any operation or fragment.
        /// </summary>
        /// <value>The maximum depth.</value>
        public int MaxDepth { get; set; }

        /// <summary>
        /// Gets the <see cref="Type" /> of the schema that this plan was created for.
        /// </summary>
        /// <value>The name of the schema.</value>
        public Type SchemaType => typeof(TSchema);
    }
}