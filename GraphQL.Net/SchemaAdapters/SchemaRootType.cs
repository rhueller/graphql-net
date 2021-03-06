﻿using System.Collections.Generic;
using System.Linq;
using GraphQL.Parser;
using GraphQL.Parser.CS;

namespace GraphQL.Net.SchemaAdapters
{
    class SchemaRootType : SchemaQueryTypeCS<Info>
    {
        public SchemaRootType(Schema schema, GraphQLType baseQueryType)
        {
            Fields = baseQueryType.Fields
                .Select(f => new SchemaField(this, f, schema))
                .ToDictionary(f => f.FieldName, f => f as ISchemaField<Info>);
        }

        public override IReadOnlyDictionary<string, ISchemaField<Info>> Fields { get; }
        public override string TypeName => "SchemaRoot";
    }
}
