namespace Api
{
  public class AppConfig
  {
    public bool DebugMode { get; set; } = false;
    public string SerilogSink { get; set; }

    public int WebPort { get; set; } = 8080;
    public bool UseHTTPS { get; set; } = false;
    public string CertificatePath { get; set; }
    public string CertificateSecret { get; set; }

    public string MongoConnectionstring { get; set; } = "mongodb://database:27017";
    public string MongoDatabase { get; set; } = "chatbot";
    public string RedisConnectionstring { get; set; } = "auth:6379";

    public string UploadsDir { get; set; } = "./uploads";
    public string ThumbnailsDir { get; set; } = "./uploads/thumbnails";
    public int ThumbnailSize { get; set; } = 100;

    public string Email { get; set; } = "admin@email.com";
    public string Name { get; set; } = "Admin user";
    public string Password { get; set; } = "secret";
  }
}
