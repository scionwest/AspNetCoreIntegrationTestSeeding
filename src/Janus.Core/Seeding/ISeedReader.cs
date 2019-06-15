﻿namespace Janus.Seeding
{
    /// <summary>
    /// Provides options for reading data generated by an instance of <see cref="IEntitySeeder"/>
    /// </summary>
    public interface ISeedReader
    {
        /// <summary>
        /// Provides all of the entities generated by an instance of <see cref="IEntitySeeder{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The entity type used to instantiate the seeded data being returned.</typeparam>
        /// <returns>Returns an array of entities generated by a seeder.</returns>
        TEntity[] GetDataForEntity<TEntity>();
    }
}
