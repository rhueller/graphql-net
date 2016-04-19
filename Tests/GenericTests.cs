﻿using System.Collections.Generic;
using GraphQL.Net;
using NUnit.Framework;

namespace Tests
{
    public static class GenericTests
    {
        public static void LookupSingleEntity<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, name } }");
            Test.DeepEquals(results, "{ user: { id: 1, name: 'Joe User' } }");
        }

        public static void AliasOneField<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { idAlias : id, name } }");
            Test.DeepEquals(results, "{ user: { idAlias: 1, name: 'Joe User' } }");
        }

        public static void NestedEntity<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, account { id, name } } }");
            Test.DeepEquals(results, "{ user: { id: 1, account: { id: 1, name: 'My Test Account' } } }");
        }

        public static void NoUserQueryReturnsNull<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:0) { id, account { id, name } } }");
            Test.DeepEquals(results, "{ user: null }");
        }

        public static void CustomFieldSubQuery<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, accountPaid } }");
            Test.DeepEquals(results, "{ user: { id: 1, accountPaid: true } }");
        }

        public static void CustomFieldSubQueryUsingContext<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, total } }");
            Test.DeepEquals(results, "{ user: { id: 1, total: 2 } }");
        }

        public static void List<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ users { id, name } }");
            Test.DeepEquals(results, "{ users: [{ id: 1, name: 'Joe User'}, { id: 2, name: 'Late Paying User' }] }");
        }

        public static void ListTypeIsList<TContext>(GraphQL<TContext> gql)
        {
            var users = gql.ExecuteQuery("{ users { id, name } }")["users"];
            Assert.AreEqual(users.GetType(), typeof(List<IDictionary<string, object>>));
        }

        public static void NestedEntityList<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ account(id:1) { id, users { id, name } } }");
            Test.DeepEquals(results, "{ account: { id: 1, users: [{ id: 1, name: 'Joe User' }] } }");
        }

        public static void PostField<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, abc } }");
            Test.DeepEquals(results, "{ user: { id: 1, abc: 'easy as 123' } }");
        }

        public static void PostFieldSubQuery<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { sub { id } } }");
            Test.DeepEquals(results, "{ user: { sub: { id: 1 } } }");
        }

        public static void TypeName<TContext>(GraphQL<TContext> gql)
        {
            var results = gql.ExecuteQuery("{ user(id:1) { id, __typename } }");
            Test.DeepEquals(results, "{ user: { id: 1, __typename: 'User' } }");
        }

        public static void DateTimeFilter<TConext>(GraphQL<TConext> gql)
        {
            var results =  gql.ExecuteQuery("{ accountPaidBy(paid: \"2016-02-01T00:00:00\") { id } }");
            Test.DeepEquals(results, "{ accountPaidBy: { id: 1 } }");
        }

        public static void EnumerableSubField<TContext>(GraphQL<TContext> gql)
        {
            var results =  gql.ExecuteQuery("{ account(id:1) { activeUsers { id, name } } }");
            Test.DeepEquals(results, "{ account: { activeUsers: [{ id: 1, name: 'Joe User' }] } }");

            var results2 =  gql.ExecuteQuery("{ account(id:2) { activeUsers { id, name } } }");
            Test.DeepEquals(results2, "{ account: { activeUsers: [] } }");
        }
    }
}
