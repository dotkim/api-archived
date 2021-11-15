using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.ServiceInterface.Modules;
using Api.ServiceInterface.Storage;
using Api.ServiceModel;
using ServiceStack;
using ServiceStack.Logging;

namespace Api.ServiceInterface
{
  [Authenticate]
  [RequiredRole("Admin")]
  public class VideoService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(VideoService));
    private VideoModule _module = new VideoModule();

    public async Task<GetVideoRandomResponse> GetAsync(GetVideoRandom request)
    {
      var query = await _module.GetRandom(request.GuildId, request.Filter);
      if (query is null) throw new FileNotFoundException("There are no video files for this guild.");

      return new GetVideoRandomResponse { FileInfo = query };
    }

    public async Task<object> PostAsync(PostVideo request)
    {
      List<string> files = FileHandler.Process(base.Request.Files, "video");
      if (files.Count <= 0) throw new ArgumentNullException("Files");

      // Create an int to see if we ignored any files while processing.
      // If a file is ignored it is most likely a wrong format for this endpoint.
      int ignoredFiles = 0;
      if (base.Request.Files.Length != files.Count)
        ignoredFiles = base.Request.Files.Length - files.Count;

      // Create a dict to check for insert or update errors.
      // This is used for possibly returning another code to the user
      // if one or more files wasn't upserted into the database.
      var checkList = new Dictionary<string, bool>();

      foreach (string file in files)
      {
        string ext = file.Split(".").Last();

        var video = _module.GetTypeConstraint();
        video.Name = file;
        video.GuildId = request.GuildId;
        video.Extension = ext;
        video.Tags = new List<string> { "tagme" };

        bool check = await _module.Exists(video.Name, request.GuildId);

        bool query;
        // A file should be updated when it already exists.
        // If it doesn't its inserted.
        if (check)
        {
          query = await _module.Update(video);
        }
        else
        {
          query = await _module.Insert(video);
        }

        // Adds the file and query result.
        checkList.Add(file, query);
      }

      if (ignoredFiles > 0) _Log.InfoFormat("Ignored {int} file(s) because of filetype.", ignoredFiles);
      foreach (string key in checkList.Keys)
      {
        if (!checkList[key]) _Log.InfoFormat("{key} was not inserted into the database.", key);
      }

      return HttpResult.Redirect("/");
    }
  }
}
