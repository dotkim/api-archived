using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Api.ServiceInterface.Common;
using ServiceStack;
using Microsoft.Extensions.Configuration;

namespace Api.ServiceInterface.Storage
{
  public static class FileHandler
  {
    private static AppConfig _config;

    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L129
    private static Stream Resize(Image img, int newWidth, int newHeight)
    {
      if (newWidth != img.Width || newHeight != img.Height)
      {
        var ratioX = (double)newWidth / img.Width;
        var ratioY = (double)newHeight / img.Height;
        var ratio = Math.Max(ratioX, ratioY);
        var width = (int)(img.Width * ratio);
        var height = (int)(img.Height * ratio);

        var newImage = new Bitmap(width, height);
        Graphics.FromImage(newImage).DrawImage(img, 0, 0, width, height);
        img = newImage;

        if (img.Width != newWidth || img.Height != newHeight)
        {
          var startX = (Math.Max(img.Width, newWidth) - Math.Min(img.Width, newWidth)) / 2;
          var startY = (Math.Max(img.Height, newHeight) - Math.Min(img.Height, newHeight)) / 2;
          img = Crop(img, newWidth, newHeight, startX, startY);
        }
      }

      var ms = new MemoryStream();
      img.Save(ms, ImageFormat.Png);
      ms.Position = 0;
      return ms;
    }

    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L157
    private static Image Crop(Image Image, int newWidth, int newHeight, int startX = 0, int startY = 0)
    {
      if (Image.Height < newHeight)
        newHeight = Image.Height;

      if (Image.Width < newWidth)
        newWidth = Image.Width;

      using (var bmp = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb))
      {
        bmp.SetResolution(72, 72);
        using (var g = Graphics.FromImage(bmp))
        {
          g.SmoothingMode = SmoothingMode.AntiAlias;
          g.InterpolationMode = InterpolationMode.HighQualityBicubic;
          g.PixelOffsetMode = PixelOffsetMode.HighQuality;
          g.DrawImage(Image, new Rectangle(0, 0, newWidth, newHeight), startX, startY, newWidth, newHeight, GraphicsUnit.Pixel);

          var ms = new MemoryStream();
          bmp.Save(ms, ImageFormat.Png);
          Image.Dispose();
          var outimage = Image.FromStream(ms);
          return outimage;
        }
      }
    }

    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L63
    public static List<string> Process(ServiceStack.Web.IHttpFile[] files, string type)
    {
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      _config = builder.Build().Get<AppConfig>();

      var data = new List<string>();

      foreach (var uploadedFile in files
       .Where(uploadedFile => uploadedFile.ContentLength > 0))
      {
        if (!MimeTypes.GetMimeType(uploadedFile.FileName).StartsWith(type)) continue;

        using (var ms = new System.IO.MemoryStream())
        {
          uploadedFile.WriteTo(ms);
          data.Add(Write(uploadedFile.FileName, ms, type));
        }
      }

      return data;
    }

    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L75
    private static string Write(string name, Stream ms, string type)
    {
      string uploadDir = AssertDir(_config.UploadsDir);
      string thumbnailsDir = AssertDir(_config.ThumbnailsDir);
      int thumbnailSize = _config.ThumbnailSize;

      ms.Position = 0;
      string hash = Hashing.GetMd5Hash(ms.ReadFully());
      string fileName = hash + "." + name.Split(".").Last();

      ms.Position = 0;
      if (type == "image")
      {
        using (var img = Image.FromStream(ms))
        {
          img.Save(uploadDir.CombineWith(fileName));
          var stream = Resize(img, thumbnailSize, thumbnailSize);
          File.WriteAllBytes(thumbnailsDir.CombineWith(fileName), stream.ReadFully());
        }
      }
      else
      {
        // Does not create a thumbnail for videos and audio files.
        using (FileStream fs = new FileStream(uploadDir.CombineWith(fileName), FileMode.Create))
        {
          ms.CopyTo(fs);
          fs.Flush();
        }
      }

      return fileName;
    }

    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L206
    private static string AssertDir(string dirPath)
    {
      if (!Directory.Exists(dirPath))
        Directory.CreateDirectory(dirPath);
      return dirPath;
    }
  }
}
