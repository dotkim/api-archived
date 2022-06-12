using System;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Collection("messages")]
  public class Message : Entity, ICreatedOn, IModifiedOn
  {
    public ulong UploaderId { get; set; }
    public string Text { get; set; }

    // This is 0 by default so we dont need to set it when inserting a new Keyword.
    // It will only be one message and the count will naturally be 0.
    public int Count { get; set; } = 0;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Task IncrementCount()
    {
      return DB.Update<Message>()
        .MatchID(this.ID)
        .Modify(x => x.Count, Count + 1)
        .ExecuteAsync();
    }

    public Task Save()
    {
      return this.SaveAsync();
    }
  }
}
