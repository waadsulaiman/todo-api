﻿using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;

namespace Todo.Api.GraphQL.Resolvers
{
    /// <summary>
    /// A resolver for <see cref="Collection"/> navigation properties.
    /// </summary>
    public class CollectionResolvers
    {
        #region GetUser

        /// <summary>
        /// Gets the <see cref="User"/> that owns this collection.
        /// </summary>
        /// <param name="collection">Represents a <see cref="Collection"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A <see cref="User"/>.
        /// </returns>
        public User? GetUser([Parent] Collection collection, [ScopedService] DatabaseContext context)
        {
            return context.Users.FirstOrDefault(options => options.Id == collection.UserId);
        }

        #endregion GetUser

        #region GetAssignments

        /// <summary>
        /// Gets a list of <see cref="Assignment"/>'s contained within this <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Represents a <see cref="Collection"/>.</param>
        /// <param name="context">DbContext for querying and mutating data.</param>
        /// <returns>
        /// A list of <see cref="Assignment"/>'s.
        /// </returns>
        public IQueryable<Assignment> GetAssignments([Parent] Collection collection, [ScopedService] DatabaseContext context)
        {
            return context.Assignments.Where(options => options.CollectionId == collection.Id).AsQueryable();
        }

        #endregion GetAssignments
    }
}