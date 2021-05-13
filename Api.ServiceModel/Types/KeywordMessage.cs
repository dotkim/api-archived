namespace Api.ServiceModel.Types
{
  public class KeywordMessage
  {
    public string Message { get; set; }

    // This is 0 by default so we dont need to set it when inserting a new Keyword.
    // It will only be one message and the count will naturally be 0.
    public int Count { get; set; } = 0;
  }
}
