using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Entities;

using Api.ServiceModel.Interfaces;

namespace Api.ServiceModel.Storage
{
  public class Database<T> where T : ISchema
  {
    /// <summary>
    /// Get document from the database.
    /// </summary>
    /// <param name="name">Name of the document.</param>
    /// <param name="gid">GuildId from Discord.</param>
    /// <returns>An instance of the document entity.</returns>
    public async static Task<T> Get(string name, ulong gid)
    {
      var res = await DB.Find<T>()
        .Match(a => a.Name == name)
        .Match(a => a.GuildId == gid)
        .ExecuteFirstAsync();
      return res;
    }

    /// <summary>
    /// This method provides a list of documents from the mongodb instance.
    /// It uses the provided page and filter parameters to create a skip variable.
    /// This variable will skip n of the documents returned from the db query,
    /// and get the configured return size of the query to return the "next page".
    /// </summary>
    /// <param name="page">What page of documents to return</param>
    /// <param name="filter">A bit switch for returning a filtered list.</param>
    /// <returns>A list of T</returns>
    public async static Task<List<T>> GetPage(int page, bool filter)
    {
      // TODO: Implement filtering.
      int returnSize = 24;  // The amount of images that should be returned. TODO: Get this from config.
      int skip = 0;         // The amount of images to skip, this is used for getting pages.
      // If the page is anything other than 0 we multiply it with the returnSize.
      // This ensures we skip the correct amount of images pr. page.
      if (page != 0) skip = returnSize * page;

      return await DB.Find<T>()
        .Sort(a => a.ModifiedOn, Order.Descending)
        .Skip(skip).Limit(returnSize)
        .ProjectExcluding(a => new { a.ID })
        .ExecuteAsync();
    }

    /// <summary>
    /// Get a random document from the database.
    /// This method uses a RNG, it counts all the documents of the guild to get a "max".
    /// The random number is then passed to the "skip" method for fetching a document.
    /// </summary>
    /// <param name="gid">GuildId from Discord.</param>
    /// <returns>An instance of the document entity.</returns>
    public async static Task<T> GetRandom(ulong gid, string filter)
    {
      var count = await DB.CountAsync<T>(a => a.GuildId == gid && a.Tags.Contains(filter));

      Random r = new Random();
      int skip = r.Next(0, (int)count);

      return await DB.Find<T>()
        .Match(a => a.GuildId == gid)
        .Match(a => a.Tags.Contains(filter))
        .Skip(skip).Limit(-1)
        .ExecuteSingleAsync();
    }

    /// <summary>
    /// A method for checking if an document exists in the database.
    /// </summary>
    /// <param name="name">Name of the document.</param>
    /// <param name="gid">GuildId from Discord</param>
    /// <returns>boolean</returns>
    public async static Task<bool> Exists(T type)
    {
      long count = await DB.CountAsync<T>(a => a.Name == type.Name && a.GuildId == type.GuildId);
      if (count > 0) return true;
      else return false;
    }

    /// <summary>
    /// Insert a document into the database.
    /// </summary>
    /// <param name="type">An instance of the type to insert</param>
    /// <returns>boolean</returns>
    public async static Task<bool> Insert(T type)
    {
      await DB.SaveAsync(type);
      bool res = await Database<T>.Exists(type);
      return res;
    }

    /// <summary>
    /// Update a document in the database.
    /// </summary>
    /// <param name="type">An instance of the type to update.</param>
    /// <returns>boolean</returns>
    public async static Task<bool> Update(T type)
    {
      var res = await DB.Update<T>()
        .Match(a => a.Name == type.Name)
        .Match(a => a.GuildId == type.GuildId)
        .Modify(x => x.CurrentDate(a => a.ModifiedOn))
        .ExecuteAsync();

      return res.IsAcknowledged;
    }

    /// <summary>
    /// Change the Guild id of a document, used when a file is connected to the wrong guild.
    /// </summary>
    /// <param name="name">the name of the file</param>
    /// <param name="current">the guild the file is connected to</param>
    /// <param name="change">the guild you want to change to</param>
    /// <returns></returns>
    public async static Task<bool> ChangeGuild(string name, ulong current, ulong change)
    {
      var res = await DB.Update<T>()
        .Match(a => a.Name == name)
        .Match(a => a.GuildId == current)
        .Modify(x => x.GuildId, change)
        .ExecuteAsync();

      return res.IsAcknowledged;
    }

    /// <summary>
    /// Get all the keywords for a specific Guild.
    /// </summary>
    /// <param name="gid">The guild to get for.</param>
    /// <returns>A list of strings of all the keywords</returns>
    public async static Task<List<T>> GetAllNames(ulong gid)
    {
      var res = await DB.Find<T>()
        .Match(a => a.GuildId == gid)
        .ExecuteAsync();
      return res;
    }
  }
}
